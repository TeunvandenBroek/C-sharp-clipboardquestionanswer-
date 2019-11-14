using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace it
{
    public class StopwatchActions : IAction
    {
        private readonly Form1 form1;

        public StopwatchActions(Form1 form1)
        {
            this.form1 = form1;
        }
        private Stopwatch myStopwatch;

        private string lastClipboard;

        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }


        public bool TryExecute(string clipboardText)
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
                                lastClipboard = clipboard;
                                myStopwatch.Reset();
                                myStopwatch = new Stopwatch();
                                myStopwatch.Start();
                                TimeSpan ts = myStopwatch.Elapsed;
                                ShowNotification("Stopwatch gereset naar", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                            }
                            break;
                        }
                    case "pause stopwatch": //  pause
                        {
                            if (clipboard != lastClipboard)
                            {
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

            return false;
        }
    }
}