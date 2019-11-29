namespace it
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class frmMain : Form
    {
        private readonly DeviceActions deviceActions;

        private readonly TimezoneActions timezoneActions;

        private readonly TimespanActions timespanActions;

        private readonly MathActions mathActions;

        private readonly StopwatchActions stopwatchActions;

        private readonly RandomActions randomActions;

        private readonly CountdownActions countdownActions;

        private readonly ConvertActions convertActions;

        private readonly TryWifiPass trywifiPass;

        private readonly TryCalcBmi tryCalcBmi;


        private readonly List<Question> questionList = Questions.LoadQuestions();

        private IntPtr clipboardViewerNext;


        public frmMain()
        {
            InitializeComponent();

            deviceActions = new DeviceActions(this);
            timezoneActions = new TimezoneActions(this);
            timespanActions = new TimespanActions(this);
            mathActions = new MathActions(this);
            stopwatchActions = new StopwatchActions(this);
            randomActions = new RandomActions(this);
            countdownActions = new CountdownActions(this);
            convertActions = new ConvertActions(this);
            trywifiPass = new TryWifiPass(this);
            tryCalcBmi = new TryCalcBmi(this);
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        private void Form1_Load(object sender, EventArgs e)
        {
            clipboardViewerNext = SetClipboardViewer(Handle);
        }
        private void GetAnswer(string clipboardText)
        {
            if (deviceActions.TryExecute(clipboardText) || timezoneActions.TryExecute(clipboardText) || timespanActions.TryExecute(clipboardText)
            || convertActions.TryExecute(clipboardText) || randomActions.TryExecute(clipboardText) || stopwatchActions.TryExecute(clipboardText) || countdownActions.TryExecute(clipboardText)
            || trywifiPass.TryExecute(clipboardText) || tryCalcBmi.TryExecute(clipboardText))
            {
                Clipboard.Clear();
                return;
            }
            if (mathActions.TryExecute(clipboardText))
            {
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

        public void ShowNotification(string timeZoneName)
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

        public void ShowNotification(string question, string answer)
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            deviceActions.Dispose();
            Clipboard.Clear();
            Clipboard.ContainsData(null);
        }

        public void Dispose()
        {
            deviceActions?.Dispose();
        }
    }
}