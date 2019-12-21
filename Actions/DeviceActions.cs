namespace it.Actions
{
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    internal sealed class DeviceActions : IAction, IDisposable
    {
        private Process afsluiten;

        private readonly string[] commands = { "sluit", "opnieuw opstarten", "reboot", "slaapstand", "sleep", "taakbeheer",
            "task mananger", "notepad", "kladblok", "leeg prullebak", "prullebak", "empty recycle bin", "empty bin",
            "empty recycling bin", "vergrendel", "lock", nameof(afsluiten), "shut down", "ram", "windows versie", "windows version",
            "mac-adres", "mac", "mac address", "computer naam", "computer name", "cpu", "wifi check", "heb ik internet?", "count words", "ip", "runtime"};

        private readonly SmartPerformanceCounter cpuCounter = new SmartPerformanceCounter(
            () => new PerformanceCounter("Processor", "% Processor Time", "_Total"), TimeSpan.FromMinutes(1));


        private readonly SafeFileHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private bool isCountingWords;

        private Process kladblok;

        private readonly SmartPerformanceCounter ramCounter =
            new SmartPerformanceCounter(() => new PerformanceCounter("Memory", "Available MBytes"),
                TimeSpan.FromMinutes(1));

        private Process reboot;

        private Process taskmananger;

        private Process vergrendel;

        private enum Recycle : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001,
            SHRB_NOPROGRESSUI = 0x00000002,
            SHRB_NOSOUND = 0x00000004,
        }

        public void Dispose()
        {
            handle?.Dispose();
            afsluiten?.Dispose();
            reboot?.Dispose();
            taskmananger?.Dispose();
            vergrendel?.Dispose();
            kladblok?.Dispose();
            cpuCounter?.Dispose();
            ramCounter.Dispose();
        }

        public bool Equals(DeviceActions other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            throw new NotImplementedException();
        }

        public bool Matches(string clipboardText = null)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                string command = commands[i];
                if (command.Equals(clipboardText, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                } 
            }

            return isCountingWords;
        }


        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]

        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Recycle dwFlags);

        ActionResult IAction.TryExecute(string clipboardText)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            ActionResult actionResult = new ActionResult(clipboardText);

            switch (clipboardText.ToLower(CultureInfo.InvariantCulture))
            {
                case "runtime":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    using PerformanceCounter uptime = new PerformanceCounter("System", "System Up Time");
                                    uptime.NextValue();
                                    TimeSpan.FromSeconds(uptime.NextValue());
                                    actionResult.Title = "Runtime";
                                    actionResult.Description =
                                        uptime.NextValue().ToString() + "So long on";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    using PerformanceCounter uptime = new PerformanceCounter("System", "System Up Time");
                                    uptime.NextValue();
                                    TimeSpan.FromSeconds(uptime.NextValue());
                                    actionResult.Title = "Runtime";
                                    actionResult.Description =
                                        uptime.NextValue().ToString() +
                                        "Zo lang aan";
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }
                        return actionResult;
                    }
                case "sluit":
                    {
                        Environment.Exit(0);
                        return actionResult;
                    }

                case "opnieuw opstarten":
                case nameof(reboot):
                    {
                        reboot?.Dispose();
                        reboot = Process.Start("shutdown", "/r /t 0");
                        return actionResult;
                    }

                case "slaapstand":
                case "sleep":
                    {
                        _ = Application.SetSuspendState(PowerState.Hibernate, true, true);
                        return actionResult;
                    }

                case "taakbeheer":
                case "task mananger":
                    {
                        taskmananger?.Dispose();
                        taskmananger = Process.Start("taskmgr.exe");
                        return actionResult;
                    }

                case "notepad":
                case "kladblok":
                    {
                        kladblok = Process.Start("notepad.exe");
                        return actionResult;
                    }

                case "leeg prullebak":
                case "prullebak":
                case "empty recycle bin":
                case "empty bin":
                case "empty recycling bin":
                    {
                        _ = SHEmptyRecycleBin(IntPtr.Zero, null, Recycle.SHRB_NOCONFIRMATION);
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Description = "Recycling bin emptied successfully";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Description = "Prullebak succesvol leeg gemaakt";
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }

                        return actionResult;
                    }
                case "vergrendel":
                case "lock":
                    {
                        vergrendel?.Dispose();
                        vergrendel = Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                        break;
                    }
                case "afsluiten":
                case "shut down":
                    {
                        afsluiten?.Dispose();
                        afsluiten = Process.Start("shutdown", "/s /t 0");
                        break;
                    }
                //om je momentele ram geheugen te laten zien (To display your momentary RAM memory)
                case "ram":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    using PerformanceCounter pc = ramCounter.Value;
                                    actionResult.Title = "RAM Memory";
                                    actionResult.Description =
                                        pc.NextValue().ToString(CultureInfo.InvariantCulture) + " MB of RAM in your system";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    using PerformanceCounter pc = ramCounter.Value;
                                    actionResult.Title = "Ram geheugen";
                                    actionResult.Description =
                                        pc.NextValue().ToString(CultureInfo.InvariantCulture) +
                                        " MB ram-geheugen over in je systeem";
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }
                        return actionResult;
                    }
                case "windows versie":
                case "windows version":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "Your Windows version";
                                    actionResult.Description = $"Windows Version {Environment.OSVersion.Version}";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "Je windows versie";
                                    actionResult.Description = $"Windows Version {Environment.OSVersion.Version}";
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }

                        return actionResult;
                    }
                case "mac-adres":
                case "mac":
                case "mac address":
                    {
                        string sMacAddress = string.Empty;
                        NetworkInterface[] array = NetworkInterface.GetAllNetworkInterfaces();
                        for (int i = 0; i < array.Length; i++)
                        {
                            NetworkInterface adapter = array[i]; 
                            if (string.IsNullOrEmpty(sMacAddress))
                            {
                                _ = adapter.GetPhysicalAddress().ToString();
                                return actionResult;
                            }
                        }

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "Your MAC Address";
                                    actionResult.Description = sMacAddress;
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "Je mac adres";
                                    actionResult.Description = sMacAddress;
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }

                        return actionResult;
                    }
                case "computer naam":
                case "computer name":
                    {
                        string dnsName = Dns.GetHostName();
                        Clipboard.SetText(dnsName);

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "Your computer name is";
                                    actionResult.Description = dnsName;
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "je computer naam is";
                                    actionResult.Description = dnsName;
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }

                        return actionResult;
                    }

                case "cpu":
                    {
                        // komt nu overeen met lezen van taakbeheer (Now matches read Task Manager)
                        float secondValue = cpuCounter.Value.NextValue();
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "Processor consumption";
                                    actionResult.Description = secondValue.ToString("###", CultureInfo.InvariantCulture) + "%";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "Processor verbruik";
                                    actionResult.Description = secondValue.ToString("###", CultureInfo.InvariantCulture) + "%";
                                    break;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }

                        return actionResult;
                    }

                case "wifi check":
                case "heb ik internet?":
                    {
                        try
                        {
                            using (WebClient client = new WebClient())
                            using (System.IO.Stream stream = client.OpenRead("http://www.google.com"))
                            {
                                switch (currentCulture.LCID)
                                {
                                    case 1033: // english-us
                                        {
                                            actionResult.Description = "You have Internet";
                                            break;
                                        }
                                    case 1043: // dutch
                                        {
                                            actionResult.Description = "Je hebt internet";
                                            break;
                                        }
                                    default:
                                        {
                                            return  actionResult;
                                        }
                                }
                            }

                            return actionResult;
                        }
                        catch (Exception)
                        {
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    {
                                        actionResult.Description = "You do not have Internet";
                                        break;
                                    }
                                case 1043: // dutch
                                    {
                                        actionResult.Description = "Je hebt geen internet";
                                        break;
                                    }

                                default:
                                    break;
                            }
                            return actionResult;
                        }
                    }
                case "count words":
                case "tel woorden":
                    {
                        if (!isCountingWords)
                        {
                            actionResult.Title = null;
                            actionResult.Description = null;
                            isCountingWords = true;
                        }

                        return actionResult;
                    }
                case "ip":
                    {
                        string externalIpAddress = null;
                        using (WebClient webClient = new WebClient())
                        {
                            string externalIp = webClient.DownloadString("http://icanhazip.com");
                            if (!string.IsNullOrEmpty(externalIp))
                            {
                                IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                                for (int i = 0; i < iPHostEntry.AddressList.Length; i++)
                                { 
                                    IPAddress ipAddress = iPHostEntry.AddressList[i];
                                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                                    {
                                        externalIpAddress = externalIp;
                                    }
                                }
                            }
                        }

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "IPAddress Address";
                                    actionResult.Description = "Your public IP Address = " + externalIpAddress;
                                    return actionResult;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "Ip adres";
                                    actionResult.Description = "Je public ip adres = " + externalIpAddress;
                                    return actionResult;
                                }
                            default:
                                {
                                    return actionResult;
                                }
                        }
                    }
                default:
                    {
                        if (isCountingWords)
                        {
                            string[] words = clipboardText.Split(new char[] { ' ' });
                            int numberOfWords = words.Length;
                            isCountingWords = false;
                            actionResult.Title = "Number of words are: ";
                            actionResult.Description = numberOfWords.ToString();
                        }
                        break;
                    }
            }
            return actionResult;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DeviceActions);
        }
    }
}