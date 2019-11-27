namespace it.Actions
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.Win32.SafeHandles;

    internal sealed class DeviceActions : IAction, IDisposable
    {
        private Process afsluiten;

        private readonly string[] commands = { "sluit", "opnieuw opstarten", "reboot", "slaapstand", "sleep", "taakbeheer",
            "task mananger", "notepad", "kladblok", "leeg prullebak", "prullebak", "empty recycle bin", "empty bin",
            "empty recycling bin", "vergrendel", "lock", nameof(afsluiten), "shut down", "ram", "windows versie", "windows version",
            "mac-adres", "mac", "mac address", "computer naam", "computer name", "cpu", "wifi check", "heb ik internet?", "count words", "ip", };

        private readonly SmartPerformanceCounter cpuCounter = new SmartPerformanceCounter(
            () => new PerformanceCounter("Processor", "% Processor Time", "_Total"), TimeSpan.FromMinutes(1));

        private bool disposed;

        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
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
            this.handle?.Dispose();
            this.afsluiten?.Dispose();
            this.reboot?.Dispose();
            this.taskmananger?.Dispose();
            this.vergrendel?.Dispose();
            this.kladblok?.Dispose();
            this.cpuCounter?.Dispose();
            this.ramCounter.Dispose();
        }

        public bool Matches(string clipboardText)
        {
            foreach (string command in this.commands)
            {
                if (command.Equals(clipboardText.ToLower(), StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }


        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Recycle dwFlags);


        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.handle.Dispose();
                if (this.ramCounter.IsValueCreated)
                {
                    this.ramCounter.Value.Dispose();
                }

                if (this.cpuCounter.IsValueCreated)
                {
                    this.cpuCounter.Value.Dispose();
                }

                this.taskmananger?.Dispose();
                this.afsluiten?.Dispose();
                this.vergrendel?.Dispose();
                this.reboot?.Dispose();
                this.kladblok?.Dispose();
            }

            this.disposed = true;
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var actionResult = new ActionResult(clipboardText);

            switch (clipboardText.ToLower())
            {
                case "sluit":
                    {
                        Environment.Exit(0);
                        return actionResult;
                    }

                case "opnieuw opstarten":
                case nameof(this.reboot):
                    {
                        this.reboot?.Dispose();
                        this.reboot = Process.Start("shutdown", "/r /t 0");
                        return actionResult;
                    }

                case "slaapstand":
                case "sleep":
                    {
                        Application.SetSuspendState(PowerState.Hibernate, true, true);
                        return actionResult;
                    }

                case "taakbeheer":
                case "task mananger":
                    {
                        this.taskmananger?.Dispose();
                        this.taskmananger = Process.Start("taskmgr.exe");
                        return actionResult;
                    }

                case "notepad":
                case "kladblok":
                    {
                        this.kladblok = Process.Start("notepad.exe");
                        return actionResult;
                    }

                case "leeg prullebak":
                case "prullebak":
                case "empty recycle bin":
                case "empty bin":
                case "empty recycling bin":
                    {
                        SHEmptyRecycleBin(IntPtr.Zero, null, Recycle.SHRB_NOCONFIRMATION);
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Description = "Recycling bin emptied successfully";
                                break;
                            case 1043: // dutch
                                actionResult.Description = "Prullebak succesvol leeg gemaakt";
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }

                        return actionResult;
                    }
                case "vergrendel":
                case "lock":
                    {
                        this.vergrendel?.Dispose();
                        this.vergrendel = Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                        break;
                    }
                case "afsluiten":
                case "shut down":
                    {
                        this.afsluiten?.Dispose();
                        this.afsluiten = Process.Start("shutdown", "/s /t 0");
                        break;
                    }
                //om je momentele ram geheugen te laten zien (To display your momentary RAM memory)
                case "ram":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                using (var pc = this.ramCounter.Value)
                                {
                                    actionResult.Title = "RAM Memory";
                                    actionResult.Description =
                                        pc.NextValue().ToString(CultureInfo.InvariantCulture) + " MB of RAM in your system";
                                }

                                break;
                            case 1043: // dutch
                                using (var pc = this.ramCounter.Value)
                                {
                                    actionResult.Title = "Ram geheugen";
                                    actionResult.Description =
                                        pc.NextValue().ToString(CultureInfo.InvariantCulture) +
                                        " MB ram-geheugen over in je systeem";
                                }

                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }

                        return actionResult;
                    }
                case "windows versie":
                case "windows version":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Your Windows version";
                                actionResult.Description = $"Windows Version {Environment.OSVersion.Version}";
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Je windows versie";
                                actionResult.Description = $"Windows Version {Environment.OSVersion.Version}";
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }

                        return actionResult;
                    }
                case "mac-adres":
                case "mac":
                case "mac address":
                    {
                        var sMacAddress = string.Empty;
                        foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
                        {
                            if (string.IsNullOrEmpty(sMacAddress))
                            {
                                adapter.GetPhysicalAddress().ToString();
                                return actionResult;
                            }
                        }

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Your MAC Address";
                                actionResult.Description = sMacAddress;
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Je mac adres";
                                actionResult.Description = sMacAddress;
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }

                        return actionResult;
                    }
                case "computer naam":
                case "computer name":
                    {
                        var dnsName = Dns.GetHostName();
                        Clipboard.SetText(dnsName);

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Your MAC Address";
                                actionResult.Description = dnsName;
                                break;
                            case 1043: // dutch
                                actionResult.Title = "je computer naam is";
                                actionResult.Description = dnsName;
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }

                        return actionResult;
                    }
                case "cpu":
                    {
                        // komt nu overeen met lezen van taakbeheer (Now matches read Task Manager)
                        var secondValue = this.cpuCounter.Value.NextValue();
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Processor consumption";
                                actionResult.Description = secondValue.ToString("###", CultureInfo.InvariantCulture) + "%";
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Processor verbruik";
                                actionResult.Description = secondValue.ToString("###", CultureInfo.InvariantCulture) + "%";
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }

                        return actionResult;
                    }
                case "wifi check":
                case "heb ik internet?":
                    {
                        try
                        {
                            using (var client = new WebClient())
                            using (var stream = client.OpenRead("http://www.google.com"))
                            {
                                switch (currentCulture.LCID)
                                {
                                    case 1033: // english-us
                                        actionResult.Description = "You have Internet";
                                        break;
                                    case 1043: // dutch
                                        actionResult.Description = "Je hebt internet";
                                        break;
                                    default:
                                        actionResult.IsProcessed = false;
                                        return actionResult;
                                }
                            }

                            return actionResult;
                        }
                        catch (Exception)
                        {
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    actionResult.Description = "You do not have Internet";
                                    break;
                                case 1043: // dutch
                                    actionResult.Description = "Je hebt geen internet";
                                    break;
                                default:
                                    actionResult.IsProcessed = false;
                                    return actionResult;
                            }
                        }

                        return actionResult;
                    }
                case "count words":
                    {
                        if (!this.isCountingWords)
                        {
                            actionResult.Title = null;
                            actionResult.Description = null;
                            this.isCountingWords = true;
                        }

                        return actionResult;
                    }
                case "ip":
                    {
                        string externalIpAddress = null;
                        using (var webClient = new WebClient())
                        {
                            var externalIp = webClient.DownloadString("http://icanhazip.com");
                            if (!string.IsNullOrEmpty(externalIp))
                            {
                                var iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                                foreach (var ipAddress in iPHostEntry.AddressList)
                                {
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
                                actionResult.Title = "IPAddress Address";
                                actionResult.Description = "Your public IP Address = " + externalIpAddress;
                                return actionResult;
                            case 1043: // dutch
                                actionResult.Title = "Ip adres";
                                actionResult.Description = "Je public ip adres = " + externalIpAddress;
                                return actionResult;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }
                    }

                default:
                    {
                        actionResult.IsProcessed = false;
                        return actionResult;
                    }
            }

            if (this.isCountingWords)
            {
                var words = clipboardText.Split(new char[] { ' ' });
                var numberOfWords = words.Length;
                this.isCountingWords = false;
                actionResult.Title = "Number of words are: ";
                actionResult.Description = numberOfWords.ToString();
            }
            else
            {
                actionResult.IsProcessed = false;
            }

            return actionResult;
        }
    }
}