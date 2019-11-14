using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using static System.Diagnostics.Process;

namespace it
{
    public class DeviceActions : IAction, IDisposable
    {
        private readonly SmartPerformanceCounter cpuCounter = new SmartPerformanceCounter(
            () => new PerformanceCounter("Processor", "% Processor Time", "_Total"), TimeSpan.FromMinutes(1));

        private readonly Form1 form1;

        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        private readonly SmartPerformanceCounter ramCounter =
            new SmartPerformanceCounter(() => new PerformanceCounter("Memory", "Available MBytes"),
                TimeSpan.FromMinutes(1));

        private Process _afsluiten;

        private Process _reboot;

        private Process _vergrendel;

        private bool disposed;
        private bool isCountingWords;

        public DeviceActions(Form1 form1)
        {
            this.form1 = form1;
        }


        public bool TryExecute(string clipboardText)
        {
            switch (clipboardText)
            {
                case "sluit":
                {
                    form1.Close();
                    return true;
                }
                case "opnieuw opstarten":
                case "reboot":
                {
                    _reboot = Start("shutdown", "/r /t 0");
                    return true;
                }
                case "slaapstand":
                {
                    Application.SetSuspendState(PowerState.Hibernate, true, true);
                    return true;
                }
                case "leeg prullebak":
                case "prullebak":
                {
                    SHEmptyRecycleBin(IntPtr.Zero, null, Recycle.SHRB_NOCONFIRMATION);
                    ShowNotification(clipboardText, "Prullebak succesvol leeg gemaakt");
                    return true;
                }
                case "vergrendel":
                {
                    _vergrendel = Start($@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                    return true;
                }
                case "afsluiten":
                {
                    _afsluiten = Start("shutdown", "/s /t 0");
                    return true;
                }
                //om je momentele ram geheugen te laten zien
                case "ram":
                {
                    if (ramCounter != null)
                        ShowNotification("Ram geheugen",
                            ramCounter.Value.NextValue().ToString(CultureInfo.InvariantCulture) +
                            " MB ram-geheugen over in je systeem");
                    return true;
                }
                case "windows versie":
                {
                    Form1.Osversioninfoex osVersionInfo = default;
                    if (!RtlGetVersion(ref osVersionInfo))
                        ShowNotification("Je windows versie",
                            $"Windows Version {osVersionInfo.MajorVersion}..{osVersionInfo.BuildNumber}");
                    return true;
                }
                case "mac-adres":
                case "mac":
                {
                    var sMacAddress = string.Empty;
                    foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
                        if (string.IsNullOrEmpty(sMacAddress))
                        {
                            sMacAddress = adapter.GetPhysicalAddress().ToString();
                            break;
                        }

                    ShowNotification("Je mac adres", sMacAddress);
                    return true;
                }
                case "computer naam":
                {
                    var dnsName = Dns.GetHostName();
                    ShowNotification("je computer naam is", dnsName);
                    Clipboard.SetText(dnsName);
                    return true;
                }
                case "cpu":
                {
                    if (cpuCounter == null) return true;
                    var secondValue = cpuCounter.Value.NextValue();
                    ShowNotification("Processor verbruik",
                        secondValue.ToString("###", CultureInfo.InvariantCulture) + "%");

                    return true;
                }
                case "wifi check":
                case "heb ik internet?":
                {
                    var client = new WebClient();
                    Stream stream;
                    try
                    {
                        stream = client.OpenRead("http://www.google.com");
                        stream.Dispose();
                        client.Dispose();
                        ShowNotification(clipboardText, "Je hebt internet");
                        return true;
                    }
                    catch
                    {
                        ShowNotification(clipboardText, "Je hebt geen internet");
                        stream = client.OpenRead("http://www.google.com");
                        stream.Dispose();
                        client.Dispose();
                        return false;
                    }
                }
                case "count words":
                case "tel woorden":
                {
                    ShowNotification(clipboardText,"Kopieer woorden om het aantal woorden te weten.");
                    if (!isCountingWords)
                    {
                        isCountingWords = true;
                        return false;
                    }
                }
                    return true;
                case "ip":
                {
                    using (var webClient = new WebClient())
                    {
                        var externalIp = webClient.DownloadString("http://icanhazip.com");
                        if (!string.IsNullOrEmpty(externalIp))
                        {
                            ShowNotification("Ip adres", "Je public ip adres = " + externalIp);
                            return true;
                        }

                        return false;
                    }
                }
            }

            if (!isCountingWords) return false;
            if (clipboardText != null)
            {
                var words = clipboardText.Split(new char[] { ' ' });
                var numberOfWords = words.Length;
                ShowNotification("Totaal aantal woorden zijn: ", numberOfWords.ToString());
            }

            isCountingWords = false;
            return true;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Recycle dwFlags);

        [SecurityCritical]
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern bool RtlGetVersion(ref Form1.Osversioninfoex versionInfo);

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                handle.Dispose();
                if (ramCounter.IsValueCreated) ramCounter.Value.Dispose();
                if (cpuCounter.IsValueCreated) cpuCounter.Value.Dispose();

                _afsluiten?.Dispose();
                _vergrendel?.Dispose();
                _reboot?.Dispose();
            }

            disposed = true;
        }

        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }

        private enum Recycle : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001,
            SHRB_NOPROGRESSUI = 0x00000002,
            SHRB_NOSOUND = 0x00000004
        }
    }
}