using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace it
{
    public partial class Form1 : Form
    {
        private readonly ConvertActions convertActions;

        private readonly CountdownActions countdownActions;
        private readonly DeviceActions deviceActions;

        private readonly MathActions mathActions;

        private readonly List<Question> questionList = Questions.LoadQuestions();

        private readonly RandomActions randomActions;

        private readonly StopwatchActions stopwatchActions;

        private readonly TimespanActions timespanActions;

        private readonly TimezoneActions timezoneActions;

        private IntPtr clipboardViewerNext;


        public Form1()
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
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        private void Form1_Load(object sender, EventArgs e)
        {
            clipboardViewerNext = SetClipboardViewer(Handle);
        }

        private void GetAnswer(string clipboardText)
        {
            if (deviceActions.TryExecute(clipboardText) || timezoneActions.TryExecute(clipboardText) ||
                timespanActions.TryExecute(clipboardText)
                || convertActions.TryExecute(clipboardText) || randomActions.TryExecute(clipboardText) ||
                stopwatchActions.TryExecute(clipboardText) || countdownActions.TryExecute(clipboardText))
            {
                Clipboard.Clear();
                return;
            }

            if (mathActions.TryExecute(clipboardText)) return;
            if (clipboardText.Length <= 2) return;
            foreach (var question in questionList)
                if (question.Text.Contains(clipboardText))
                {
                    ShowNotification(question.Text, question.Answer);
                    Clipboard.Clear();
                    return;
                }
        }

        public void ShowNotification(string timeZoneName)
        {
            var countrycopy = Clipboard.GetText();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
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
                    var text = Clipboard.GetText(TextDataFormat.UnicodeText);
                    if (!string.IsNullOrEmpty(text)) GetAnswer(text);
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