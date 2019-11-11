namespace it
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Recycle dwFlags);

        private readonly List<Question> questionList = Questions.LoadQuestions();

        private DateTime? PrevDate { get; set; }

        public IPInterfaceProperties IPInterfaceProperties { get; set; }

        public IntPtr ClipboardViewerNext { get; set; }

        private void SetIPInterfaceProperties(IPInterfaceProperties value)
        {
            IPInterfaceProperties = value;
        }

        [SecurityCritical]
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern bool RtlGetVersion(ref Osversioninfoex versionInfo);

        public Form1()
        {
            InitializeComponent();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        private void Form1_Load(object sender, EventArgs e)
        {
            ClipboardViewerNext = SetClipboardViewer(Handle);
        }

        private readonly Regex mathRegex = new Regex(@"^(?<lhs>\d+(?:[,.]{1}\d)*)(([ ]*(?<operator>[+\-\:x\%\*/])[ ]*(?<rhs>\d+(?:[,.]{1}\d)*)+)+)");

        private readonly Dictionary<string, Func<double, double, double>> binaryOperators = new Dictionary<string, Func<double, double, double>>
            {
               { "+", (a, b) => a + b},
               { "x", (a, b) => a * b },
               { "*", (a, b) => a * b },
               { "-", (a, b) => a - b },
               { "/", (a, b) => b == 0 ? double.NaN : a / b },
               { ":", (a, b) => b == 0 ? double.NaN : a / b },
               { "%", (a, b) => a / b * 100 },
            };

        private void GetAnswer(string clipboardText)
        {
            if (TryDeviceActions(clipboardText) || TryTimeZonesActions(clipboardText) || TryComputeTimeSpan(clipboardText)
                || ConvertUnits(clipboardText) || TryDoMaths(clipboardText) || TryRandomActions(clipboardText) || TryDoStopWatch(clipboardText) || TryDoCountdown(clipboardText))

            {
                Clipboard.Clear();
                return;
            }
            if (clipboardText.Length > 2)
            {
                foreach (Question question in questionList)
                {
                    if (question.Text.Contains(clipboardText))
                    {
                        ShowNotification(question.Text, question.Answer);
                        Clipboard.Clear();
                        return;
                    }
                }
            }
        }

        private Stopwatch myStopwatch;

        private string lastClipboard;

        private bool TryDoStopWatch(string clipboardText)
        {
            TryStopwatch(clipboardText);

            return false;
        }

        private void TryStopwatch(string clipboardText)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }
            if (Clipboard.ContainsText())
            {
                string clipboard = Clipboard.GetText();
                switch (clipboard)
                {
                    case "start stopwatch": //start
                        {
                            if (myStopwatch?.IsRunning == true)
                            {
                                ShowNotification("Stopwatch", "Stopwatch already running");
                            }
                            if (clipboard != lastClipboard)
                            {
                                InteractiveTimer.Enabled = false;
                                lastClipboard = clipboard;
                                myStopwatch = new Stopwatch();
                                myStopwatch.Start();
                            }
                            break;
                        }
                    case "reset stopwatch": //reset
                        {
                            if (clipboard != lastClipboard)
                            {
                                InteractiveTimer.Enabled = false;
                                lastClipboard = clipboard;
                                myStopwatch.Reset();
                                myStopwatch = new Stopwatch();
                                myStopwatch.Start();
                                TimeSpan ts = myStopwatch.Elapsed;
                                ShowNotification("Stopwatch gereset naar", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                            }
                            break;
                        }
                    case "pauzeer stopwatch": //  pause
                        {
                            if (clipboard != lastClipboard)
                            {
                                InteractiveTimer.Enabled = false;
                                lastClipboard = clipboard;
                                TimeSpan ts = myStopwatch.Elapsed;
                                ShowNotification("Stopwatch gepauzeerd op", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                                myStopwatch.Stop();
                            }
                            break;
                        }
                    case "resume stopwatch":
                        {
                            if (clipboard != lastClipboard)
                            {
                                InteractiveTimer.Enabled = true;
                                lastClipboard = clipboard;
                                TimeSpan ts = myStopwatch.Elapsed;
                                ShowNotification("Stopwatch hervat vanaf", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                                myStopwatch.Start();
                            }
                            break;
                        }
                    case "stop stopwatch": //stop
                        {
                            if (lastClipboard is object)
                            {
                                lastClipboard = null;
                                myStopwatch.Stop();
                                TimeSpan ts = myStopwatch.Elapsed;
                                ShowNotification("Elapsed time", $"{ts.Hours} uur, {ts.Minutes} minuten, {ts.Seconds}secondes");
                            }
                            break;
                        }
                }
            }
        }

        private bool TryDoCountdown(string clipboardText)
        {
            if (clipboardText.StartsWith("timer") && TimeSpan.TryParse(clipboardText.Replace("timer ", ""), out TimeSpan ts))
            {
                async Task p()
                {
                    await Task.Delay(ts).ConfigureAwait(false);
                    ShowNotification("Countdown timer", "time is over");
                }
                Func<Task> function = p;
                Task.Run(function);
                return true;
            }
            return false;
        }

        private bool TryDoMaths(string clipboardText)
        {
            Match match = mathRegex.Match(clipboardText.Replace(',', '.'));
            if (!match.Success)
            {
                return false;
            }
            string[] operators = (from Capture capture in match.Groups["operator"].Captures
                                  select capture.Value).ToArray();
            double lhs = double.Parse(match.Groups["lhs"].Value, CultureInfo.InvariantCulture);
            double[] rhss = (from Capture capture in match.Groups["rhs"].Captures
                             select double.Parse(capture.Value, CultureInfo.InvariantCulture)).ToArray();
            double answer = lhs;
            int i = 0;
            for (int i2 = 0; i2 < rhss.Length; i2++)
            {
                answer = binaryOperators[operators[i++]](answer, rhss[i2]);
            }
            ShowNotification(clipboardText, answer.ToString(CultureInfo.CurrentCulture));
            Clipboard.SetText(answer.ToString(CultureInfo.CurrentCulture));
            return true;
        }

        private readonly string[] DateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy"
        };

        private readonly Random _random = new Random();

        private bool TryRandomActions(string clipboardText)
        {
            switch (clipboardText)
            {
                case "kop of munt":
                    {
                        if (_random.NextDouble() < 0.5)
                        {
                            ShowNotification("Kop of munt?", "Kop");
                        }
                        else
                        {
                            ShowNotification("Kop of munt?", "Munt");
                        }
                        return true;
                    }
                case "random password":
                    {
                        const int minLength = 8;
                        const int maxLength = 12;
                        const string charAvailable = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789-";
                        StringBuilder password = new StringBuilder();
                        int passwordLength = _random.Next(minLength, maxLength + 1);
                        while (passwordLength-- > 0)
                        {
                            password.Append(charAvailable[_random.Next(charAvailable.Length)]);
                            ShowNotification("Random password", password.ToString());
                        }
                        return true;
                    }
            }
            return false;
        }

        private bool TryComputeTimeSpan(string clipboardText)
        {
            if (DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate))
            {
                if (PrevDate is object)
                {
                    TimeSpan? difference = newDate - PrevDate;
                    if (difference is object)
                    {
                        ShowNotification("Days between:", difference.Value.Days.ToString(CultureInfo.InvariantCulture));
                    }
                    PrevDate = null;
                }
                else
                {
                    PrevDate = newDate;
                }
                return true;
            }
            PrevDate = null;
            return false;
        }

        private bool TryDeviceActions(string clipboardText)
        {
            switch (clipboardText)
            {
                case "sluit":
                    {
                        Close();
                        return true;
                    }
                case "opnieuw opstarten":
                case "reboot":
                    {
                        _reboot = Process.Start("shutdown", "/r /t 0");
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
                        _vergrendel = Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                        return true;
                    }
                case "afsluiten":
                    {
                        _afsluiten = Process.Start("shutdown", "/s /t 0");
                        return true;
                    }
                //om je momentele ram geheugen te laten zien
                case "ram":
                    {
                        ShowNotification("Ram geheugen", ramCounter.Value.NextValue().ToString(CultureInfo.InvariantCulture) + " MB ram-geheugen over in je systeem");
                        return true;
                    }
                case "windows versie":
                    {
                        Osversioninfoex osVersionInfo = default;
                        if (!RtlGetVersion(ref osVersionInfo))
                        {
                            ShowNotification("Je windows versie", $"Windows Version {osVersionInfo.MajorVersion}..{osVersionInfo.BuildNumber}");
                        }
                        return true;
                    }
                case "mac-adres":
                case "mac":
                    {
                        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                        string sMacAddress = string.Empty;
                        foreach (NetworkInterface adapter in nics)
                        {
                            if (string.IsNullOrEmpty(sMacAddress))
                            {
                                SetIPInterfaceProperties(adapter.GetIPProperties());
                                sMacAddress = adapter.GetPhysicalAddress().ToString();
                            }
                        }
                        ShowNotification("Je mac adres", sMacAddress);
                        return true;
                    }
                case "computer naam":
                    {
                        string dnsName = Dns.GetHostName();
                        ShowNotification("je computer naam is", dnsName);
                        Clipboard.SetText(dnsName);
                        return true;
                    }
                case "cpu":
                    {
                        // komt nu overeen met lezen van taakbeheer
                        float secondValue = cpuCounter.Value.NextValue();
                        ShowNotification("Processor verbruik", secondValue.ToString("###", CultureInfo.InvariantCulture) + "%");
                        return true;
                    }
                case "ip":
                    {
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
                                        ShowNotification("Ip adres", "Je public ip adres = " + externalIp);
                                    }
                                }
                            }
                        }
                        return true;
                    }
            }
            return false;
        }

        private string country;

        private bool TryTimeZonesActions(string clipboardText)
        {
            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> result = TryKeypair();
            if (result.Key == default)
                return false;

            switch (result.Value)
            {
                case Countries.UtcOffset.UtcMinusTwelve:
                    {
                        ShowNotification("Dateline Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusEleven:
                    {
                        ShowNotification("Samoa Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusTen:
                    {
                        ShowNotification("Hawaiian Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusNine:
                    {
                        ShowNotification("Alaskan Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusEight:
                    {
                        ShowNotification("Pacific Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusSeven:
                    {
                        ShowNotification("US Mountain Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusSix:
                    {
                        ShowNotification("Central America Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusFive:
                    {
                        ShowNotification("SA Pacific Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusFour:
                    {
                        ShowNotification("Atlantic Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusThreepoinfive:
                    {
                        ShowNotification("Newfoundland Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusThree:
                    {
                        ShowNotification("E. South America Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusTwo:
                    {
                        ShowNotification("Mid-Atlantic Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcMinusOne:
                    {
                        ShowNotification("Cape Verde Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcZero:
                    {
                        ShowNotification("GMT Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusOne:
                    {
                        ShowNotification(country, DateTime.Now.ToString("HH:mm", CultureInfo.CurrentCulture));
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusTwo:
                    {
                        ShowNotification("Jordan Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusThree:
                    {
                        ShowNotification("Arabic Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusThreepoinfive:
                    {
                        ShowNotification("Iran Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusFour:
                    {
                        ShowNotification("Mauritius Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusFourpointfive:
                    {
                        ShowNotification("Afghanistan Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusFive:
                    {
                        ShowNotification("Ekaterinburg Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusSix:
                    {
                        ShowNotification("N. Central Asia Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusSeven:
                    {
                        ShowNotification("SE Asia Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusEight:
                    {
                        ShowNotification("China Standard Time ");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusNine:
                    {
                        ShowNotification("Korea Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusTen:
                    {
                        ShowNotification("E. Australia Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusEleven:
                    {
                        ShowNotification("Central Pacific Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusTwelve:
                    {
                        ShowNotification("New Zealand Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusThirteen:
                    {
                        ShowNotification("Tonga Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusFivepointfive:
                    {
                        ShowNotification("India Standard Time");
                        return true;
                    }
                case Countries.UtcOffset.UtcPlusFivepointThreeQuarters:
                    {
                        ShowNotification("Nepal Standard Time");
                        return true;
                    }
            }
            return false;
        }

        private KeyValuePair<string, Countries.UtcOffset> TryKeypair()
        {
            bool predicate(KeyValuePair<string, Countries.UtcOffset> x) => x.Key.Contains(country);
            return Countries.UtcOffsetByCountry.FirstOrDefault(predicate);
        }

        private void ShowNotification(string timeZoneName)
        {
            string countrycopy = Clipboard.GetText();
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            notifyIcon1.Icon = SystemIcons.Exclamation;
            notifyIcon1.BalloonTipTitle = countrycopy;
            notifyIcon1.BalloonTipText = dateTime.ToString("HH:mm", CultureInfo.CurrentCulture);
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void ShowNotification(string question, string answer)
        {
            notifyIcon1.Icon = SystemIcons.Exclamation;
            notifyIcon1.BalloonTipTitle = question;
            notifyIcon1.BalloonTipText = answer;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon1.ShowBalloonTip(1000);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const int WM_DRAWCLIPBOARD = 0x308;
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    {
                        string text = Clipboard.GetText(TextDataFormat.UnicodeText);
                        if (!string.IsNullOrEmpty(text))
                        {
                            GetAnswer(text);
                        }
                        break;
                    }
            }
        }

        private readonly SmartPerformanceCounter ramCounter = new SmartPerformanceCounter(() => new PerformanceCounter("Memory", "Available MBytes"), TimeSpan.FromMinutes(1));

        private readonly SmartPerformanceCounter cpuCounter = new SmartPerformanceCounter(() => new PerformanceCounter("Processor", "% Processor Time", "_Total"), TimeSpan.FromMinutes(1));

        private readonly Regex unitRegex = new Regex("(?<number>^[0-9]+([.,][0-9]{1,3})?)(\\s*)(?<from>[a-z]+[2-3]?) to (?<to>[a-z]+[2-3]?)");

        private bool ConvertUnits(string clipboardText)
        {
            Match matches = unitRegex.Match(clipboardText);
            if (!matches.Success)
            {
                return false;
            }
            double number = double.Parse(matches.Groups["number"].Value);
            string from = matches.Groups["from"].Value;
            string to = matches.Groups["to"].Value;
            return BerekenEenheden(clipboardText, number, from, to);
        }

        private bool BerekenEenheden(string clipboardText, double number, string from, string to)
        {
            Eenheden(out double meter, out double gram, out double liter, out double oppervlakte);
            switch (from)
            {
                // lengte eenheden
                case "mm":
                case "millimeter":
                    meter = number / 1000;
                    break;
                case "cm":
                case "centimer":
                    meter = number / 100;
                    break;
                case "dm":
                case "decimeter":
                    meter = number / 10;
                    break;
                case "m":
                case "meter":
                    meter = number;
                    break;
                case "dam":
                case "decameter":
                    meter = number * 1;
                    break;
                case "hm":
                case "hectometer":
                    meter = number * 100;
                    break;
                case "km":
                case "kilometer":
                    meter = number * 1000;
                    break;
                case "feet":
                case "ft":
                    meter = number * 0.3048;
                    break;
                case "inch":
                    meter = number * 0.0254;
                    break;
                case "mile":
                case "miles":
                    meter = number / 0.00062137;
                    break;
                case "yard":
                case "yd":
                    meter = number * 0.9144;
                    break;
                // gewicht eenheden
                case "mg":
                case "milligram":
                    gram = number / 1000;
                    break;
                case "cg":
                case "centigram":
                    gram = number / 100;
                    break;
                case "dg":
                case "decigram":
                    gram = number / 10;
                    break;
                case "gr":
                case "gram":
                    gram = number;
                    break;
                case "dag":
                case "decagram":
                    gram = number * 10;
                    break;
                case "hg":
                case "hectogram":
                    gram = number * 100;
                    break;
                case "kg":
                case "kilogram":
                    gram = number * 1000;
                    break;
                case "ml":
                case "milliliter":
                    liter = number / 1000;
                    break;
                case "cl":
                case "centiliter":
                    liter = number / 100;
                    break;
                case "dl":
                case "deciliter":
                    liter = number / 10;
                    break;
                case "l":
                case "liter":
                    liter = number;
                    break;
                case "dal":
                case "decaliter":
                    liter = number * 10;
                    break;
                case "hl":
                case "hectoliter":
                    liter = number * 100;
                    break;
                case "kl":
                case "kiloliter":
                    liter = number * 1000;
                    break;
                // oppervlakte eenheden
                case "mm2":
                    oppervlakte = number / 1000000;
                    break;
                case "cm2":
                    oppervlakte = number / 10000;
                    break;
                case "dm2":
                    oppervlakte = number / 100;
                    break;
                case "m2":
                    oppervlakte = number;
                    break;
                case "dam2":
                    oppervlakte = number * 100;
                    break;
                case "hm2":
                    oppervlakte = number * 10000;
                    break;
                case "km2":
                    oppervlakte = number * 1000000;
                    break;
                default:
                    return false;
            }
            // oppervlakte eenheden
            double result;
            switch (to) // naar
            {
                // lengte eenheden
                case "mm":
                case "millimeter":
                    result = meter * 1000;
                    break;
                case "cm":
                case "centimer":
                    result = meter * 100;
                    break;
                case "dm":
                case "decimeter":
                    result = meter * 10;
                    break;
                case "m":
                case "meter":
                    result = meter;
                    break;
                case "dam":
                case "decameter":
                    result = meter / 1;
                    break;
                case "hm":
                case "hectometer":
                    result = meter / 100;
                    break;
                case "km":
                case "kilometer":
                    result = meter / 1000;
                    break;
                case "feet":
                case "ft":
                    result = meter / 0.3048;
                    break;
                case "inch":
                    result = meter / 0.0254;
                    break;
                case "mile":
                case "miles":
                    result = meter * 0.00062137;
                    break;
                case "yard":
                case "yd":
                    result = meter * 0.9144;
                    break;
                // gewicht eenheden
                case "mg":
                case "milligram":
                    result = gram * 1000;
                    break;
                case "cg":
                case "centigram":
                    result = gram * 100;
                    break;
                case "dg":
                case "decigram":
                    result = gram * 10;
                    break;
                case "gr":
                case "gram":
                    result = gram;
                    break;
                case "dag":
                case "decagram":
                    result = gram / 10;
                    break;
                case "hg":
                case "hectogram":
                    result = gram / 100;
                    break;
                case "kg":
                case "kilogram":
                    result = gram / 1000;
                    break;
                // inhoud
                case "ml":
                case "milliliter":
                    result = liter * 1000;
                    break;
                case "cl":
                case "centiliter":
                    result = liter * 100;
                    break;
                case "dl":
                case "deciliter":
                    result = liter * 10;
                    break;
                case "l":
                case "liter":
                    result = liter;
                    break;
                case "dal":
                case "decaliter":
                    result = liter / 10;
                    break;
                case "hl":
                case "hectoliter":
                    result = liter / 100;
                    break;
                case "kl":
                case "kiloliter":
                    result = liter / 1000;
                    break;
                // oppervlakte eenheden
                case "mm2":
                    result = oppervlakte * 1000000;
                    break;
                case "cm2":
                    result = oppervlakte * 10000;
                    break;
                case "dm2":
                    result = oppervlakte * 100;
                    break;
                case "m2":
                    result = oppervlakte;
                    break;
                case "dam2":
                    result = oppervlakte / 100;
                    break;
                case "hm2":
                    result = oppervlakte / 10000;
                    break;
                case "km2":
                    result = oppervlakte / 1000000;
                    break;
                default:
                    return false;
            }

            Clipboard.SetText(result.ToString(CultureInfo.CurrentCulture));
            ShowNotification(clipboardText, result.ToString() + to);
            return true;
        }

        private static void Eenheden(out double meter, out double gram, out double liter, out double oppervlakte)
        {
            meter = 0;
            gram = 0;
            liter = 0;
            oppervlakte = 0;
        }

        private Process _vergrendel;

        private Process _afsluiten;

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _vergrendel?.Dispose();
            _afsluiten?.Dispose();
            if (ramCounter.IsValueCreated)
            {
                ramCounter.Value.Dispose();
            }
            if (cpuCounter.IsValueCreated)
            {
                cpuCounter.Value.Dispose();
            }
        }

        public new void Dispose()
        {
            _afsluiten?.Dispose();
            _vergrendel?.Dispose();
            _reboot?.Dispose();
        }

        private Process _reboot;
    }
}
