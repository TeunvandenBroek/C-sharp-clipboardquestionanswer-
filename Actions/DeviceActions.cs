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
    internal class DeviceActions : IAction
    {
        #region DLLImports

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Recycle dwFlags);

        //[SecurityCritical]
        //[DllImport("ntdll.dll", SetLastError = true)]
        //internal static extern bool RtlGetVersion(ref Form1.Osversioninfoex versionInfo);

        #endregion DLLImports

        private readonly SmartPerformanceCounter ramCounter = new SmartPerformanceCounter(() => new PerformanceCounter("Memory", "Available MBytes"), TimeSpan.FromMinutes(1));
        private readonly SmartPerformanceCounter cpuCounter = new SmartPerformanceCounter(() => new PerformanceCounter("Processor", "% Processor Time", "_Total"), TimeSpan.FromMinutes(1));
        private bool isCountingWords = false;

        ActionResult IAction.TryExecute(string clipboardText)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            ActionResult actionResult = new ActionResult(title: clipboardText);

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
                        break;
                    }
                case "slaapstand":
                case "sleep":
                    {
                        Application.SetSuspendState(PowerState.Hibernate, true, true);
                        break;
                    }
                case "taakbeheer":
                case "task mananger":
                    {
                        _taskmananger = Process.Start("taskmgr.exe");
                        break;
                    }
                case "notepad":
                case "kladblok":
                    {
                        Process.Start("notepad.exe");
                        break;
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
                        }
                        break;
                    }
                case "vergrendel":
                case "lock":
                    {
                        _vergrendel = Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                        break;
                    }
                case "afsluiten":
                case "shut down":
                    {
                        _afsluiten = Process.Start("shutdown", "/s /t 0");
                        break;
                    }
                //om je momentele ram geheugen te laten zien (To display your momentary RAM memory)
                case "ram":
                    {
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                using (PerformanceCounter pc = ramCounter.Value)
                                {
                                    actionResult.Title = "RAM Memory";
                                    actionResult.Description = pc.NextValue().ToString(CultureInfo.InvariantCulture) + " MB of RAM in your system";
                                }
                                break;
                            case 1043: // dutch
                                using (PerformanceCounter pc = ramCounter.Value)
                                {
                                    actionResult.Title = "Ram geheugen";
                                    actionResult.Description = pc.NextValue().ToString(CultureInfo.InvariantCulture) + " MB ram-geheugen over in je systeem";
                                }
                                break;
                        }
                        break;
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
                        }
                        break;
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
                                actionResult.Title = "Your MAC Address";
                                actionResult.Description = sMacAddress;
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Je mac adres";
                                actionResult.Description = sMacAddress;
                                break;
                        }
                        break;
                    }
                case "computer naam":
                case "computer name":
                    {
                        string dnsName = Dns.GetHostName();
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
                        }
                        break;
                    }
                case "cpu":
                    {
                        // komt nu overeen met lezen van taakbeheer (Now matches read Task Manager)
                        float secondValue = cpuCounter.Value.NextValue();
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
                        }
                        break;
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
                                }
                            }
                            break;
                        }
                        catch
                        {
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    actionResult.Description = "You do not have Internet";
                                    break;
                                case 1043: // dutch
                                    actionResult.Description = "Je hebt geen internet";
                                    break;
                            }
                        }
                        break;
                    }
                case "count words":
                    {
                        if (!isCountingWords)
                        {
                            actionResult.Title = null;
                            actionResult.Description = null;
                            isCountingWords = true;
                        }
                        return actionResult;
                    }
                case "ip-adress":
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
                                actionResult.Title = "IPAddress Address";
                                actionResult.Description = "Your public IP Address = " + externalIpAddress;
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Ip adres";
                                actionResult.Description = "Je public ip adres = " + externalIpAddress;
                                break;
                        }
                        break;
                    }


            }

            if (isCountingWords)
            {
                string[] words = clipboardText.Split(' ');
                int numberOfWords = words.Length;
                isCountingWords = false;
                actionResult.Title = "Number of words are: ";
                actionResult.Description = numberOfWords.ToString();

            }
            else
            {
                actionResult.IsProcessed = false;
            }
            return actionResult;

        }

        private bool disposed = false;

        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


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
            }

            disposed = true;
        }

        private Process _afsluiten;

        private Process _vergrendel;

        private Process _reboot;

        private enum Recycle : uint
        {
            /// <summary>
            /// Defines the SHRB_NOCONFIRMATION
            /// </summary>
            SHRB_NOCONFIRMATION = 0x00000001,
            /// <summary>
            /// Defines the SHRB_NOPROGRESSUI
            /// </summary>
            SHRB_NOPROGRESSUI = 0x00000002,
            /// <summary>
            /// Defines the SHRB_NOSOUND
            /// </summary>
            SHRB_NOSOUND = 0x00000004
        }

        private Process _taskmananger;

    }
}
