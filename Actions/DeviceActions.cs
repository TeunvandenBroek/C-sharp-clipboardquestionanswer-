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

namespace it.Actions
{
    internal class DeviceActions : IAction, IDisposable
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Recycle dwFlags);

        //[SecurityCritical]
        //[DllImport("ntdll.dll", SetLastError = true)]
        //internal static extern bool RtlGetVersion(ref Form1.Osversioninfoex versionInfo);

        private readonly SmartPerformanceCounter ramCounter = new SmartPerformanceCounter(() => new PerformanceCounter("Memory", "Available MBytes"), TimeSpan.FromMinutes(1));

        private readonly SmartPerformanceCounter cpuCounter = new SmartPerformanceCounter(() => new PerformanceCounter("Processor", "% Processor Time", "_Total"), TimeSpan.FromMinutes(1));

        private bool isCountingWords = false;

        QuestionAnswer IAction.TryExecute(string clipboardText)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            switch (clipboardText)
            {
                case "sluit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                case "opnieuw opstarten":
                case "reboot":
                    {
                        _reboot = Process.Start("shutdown", "/r /t 0");
                        return new QuestionAnswer(isSuccessful: true);
                    }
                case "slaapstand":
                case "sleep":
                    {
                        Application.SetSuspendState(PowerState.Hibernate, true, true);
                        return new QuestionAnswer(isSuccessful: true);
                    }
                case "taakbeheer":
                case "task mananger":
                    {
                        _taskmananger = Process.Start("taskmgr.exe");
                        return new QuestionAnswer(isSuccessful: true);
                    }
                case "notepad":
                case "kladblok":
                    {
                        _notepad = Process.Start("notepad.exe");
                        return new QuestionAnswer(isSuccessful: true);
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
                                return new QuestionAnswer(clipboardText, "Recycling bin emptied successfully");

                            case 1043: // dutch
                                return new QuestionAnswer(clipboardText, "Prullebak succesvol leeg gemaakt");

                            default:
                                return null;
                        }
                    }
                case "vergrendel":
                case "lock":
                    {
                        _vergrendel = Process.Start($@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                        return new QuestionAnswer(isSuccessful: true);
                    }
                case "afsluiten":
                case "shut down":
                    {
                        _afsluiten = Process.Start("shutdown", "/s /t 0");
                        return new QuestionAnswer(isSuccessful: true);
                    }
                //om je momentele ram geheugen te laten zien (To display your momentary RAM memory)
                case "ram":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                return new QuestionAnswer("RAM Memory", ramCounter.Value.NextValue().ToString(CultureInfo.InvariantCulture) + " MB of RAM in your system");

                            case 1043: // dutch
                                return new QuestionAnswer("Ram geheugen", ramCounter.Value.NextValue().ToString(CultureInfo.InvariantCulture) + " MB ram-geheugen over in je systeem");

                            default:
                                return null;
                        }
                    }
                case "windows versie":
                case "windows version":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                return new QuestionAnswer("Your Windows version", $"Windows Version {Environment.OSVersion.Version}");

                            case 1043: // dutch
                                return new QuestionAnswer("Je windows versie", $"Windows Version {Environment.OSVersion.Version}");

                            default:
                                return null;
                        }
                    }
                case "mac-adres":
                case "mac":
                case "mac address":
                    {
                        string sMacAddress = string.Empty;
                        foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                        {
                            if (string.IsNullOrEmpty(sMacAddress))
                            {
                                sMacAddress = adapter.GetPhysicalAddress().ToString();
                                break;
                            }
                        }

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                return new QuestionAnswer("Your MAC Address", sMacAddress);

                            case 1043: // dutch
                                return new QuestionAnswer("Je mac adres", sMacAddress);

                            default:
                                return new QuestionAnswer(isSuccessful: true);
                        }
                    }
                case "computer naam":
                case "computer name":
                    {
                        string dnsName = Dns.GetHostName();
                        Clipboard.SetText(dnsName);

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                return new QuestionAnswer("Your MAC Address", dnsName);

                            case 1043: // dutch
                                return new QuestionAnswer("je computer naam is", dnsName);

                            default:
                                return new QuestionAnswer(isSuccessful: true);
                        }
                    }
                case "cpu":
                    {
                        // komt nu overeen met lezen van taakbeheer (Now matches read Task Manager)
                        float secondValue = cpuCounter.Value.NextValue();
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                return new QuestionAnswer("Processor consumption", secondValue.ToString("###", CultureInfo.InvariantCulture) + "%");

                            case 1043: // dutch
                                return new QuestionAnswer("Processor verbruik", secondValue.ToString("###", CultureInfo.InvariantCulture) + "%");

                            default:
                                return new QuestionAnswer(isSuccessful: true);
                        }
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
                                        return new QuestionAnswer(clipboardText, "You have Internet");

                                    case 1043: // dutch
                                        return new QuestionAnswer(clipboardText, "Je hebt internet");

                                    default:
                                        return new QuestionAnswer(isSuccessful: true);
                                }
                            }
                        }
                        catch
                        {
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    return new QuestionAnswer(clipboardText, "You do not have Internet");

                                case 1043: // dutch
                                    return new QuestionAnswer(clipboardText, "Je hebt geen internet");

                                default:
                                    return new QuestionAnswer(isSuccessful: true);
                            }
                        }
                    }
                case "count words":
                    {
                        if (!isCountingWords) isCountingWords = true;
                        return new QuestionAnswer(isSuccessful: true);
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
                                foreach (IPAddress ipAddress in iPHostEntry.AddressList)
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
                                return new QuestionAnswer("IPAddress Address", "Your public IP Address = " + externalIpAddress);

                            case 1043: // dutch
                                return new QuestionAnswer("Ip adres", "Je public ip adres = " + externalIpAddress);

                            default:
                                return new QuestionAnswer(isSuccessful: true);
                        }
                    }
            }

            // no command
            if (isCountingWords)
            {
                string[] words = clipboardText.Split(' ');
                int numberOfWords = words.Length;
                isCountingWords = false;
                return new QuestionAnswer("Number of words are: ", numberOfWords.ToString());
            }

            return new QuestionAnswer();
        }

        private bool disposed = false;

        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                handle.Dispose();
                if (ramCounter.IsValueCreated)
                {
                    ramCounter.Value.Dispose();
                }
                if (cpuCounter.IsValueCreated)
                {
                    cpuCounter.Value.Dispose();
                }
                _taskmananger?.Dispose();
                _afsluiten?.Dispose();
                _vergrendel?.Dispose();
                _reboot?.Dispose();
                _notepad?.Dispose();
            }

            disposed = true;
        }

        private Process _afsluiten;

        private Process _vergrendel;

        private Process _reboot;

        private enum Recycle : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001,
            SHRB_NOPROGRESSUI = 0x00000002,
            /// <summary>
            /// Defines the SHRB_NOSOUND
            /// </summary>
            SHRB_NOSOUND = 0x00000004
        }

        private Process _taskmananger;
        private Process _notepad;
    }
}