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

            if (!Clipboard.ContainsText()) return false;
            var clipboard = Clipboard.GetText();
            switch (clipboard)
            {
                case "start stopwatch":
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

                    return false;
                }
                case "reset stopwatch":
                {
                    if (clipboard != lastClipboard)
                    {
                        lastClipboard = clipboard;
                        myStopwatch.Reset();
                        myStopwatch = new Stopwatch();
                        myStopwatch.Start();
                        TimeSpan ts = myStopwatch.Elapsed;
                        ShowNotification("Stopwatch gereset naar",
                            $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                    }

                    return false;
                }
                case "pause stopwatch":
                {
                    if (clipboard != lastClipboard)
                    {
                        lastClipboard = clipboard;
                        TimeSpan ts = myStopwatch.Elapsed;
                        ShowNotification("Stopwatch gepauzeerd op",
                            $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                        myStopwatch.Stop();
                    }

                    return false;
                }
                case "resume stopwatch":
                {
                    if (clipboard != lastClipboard)
                    {
                        lastClipboard = clipboard;
                        TimeSpan ts = myStopwatch.Elapsed;
                        ShowNotification("Stopwatch hervat vanaf",
                            $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                        myStopwatch.Start();
                    }

                    return false;
                }
                case "stop stopwatch":
                {
                    if (lastClipboard is object)
                    {
                        lastClipboard = null;
                        myStopwatch.Stop();
                        TimeSpan ts = myStopwatch.Elapsed;
                        ShowNotification("Elapsed time", $"{ts.Hours} uur, {ts.Minutes} minuten, {ts.Seconds}secondes");
                    }

                    return false;
                }
                default:
                    return false;
            }
        }
    }
}