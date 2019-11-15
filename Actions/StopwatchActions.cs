using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace it.Actions
{
    public class StopwatchActions : IAction
    {
        private Stopwatch myStopwatch;
        private string lastClipboard;

        public QuestionAnswer TryExecute(string clipboardText)
        {

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            string clipboard = Clipboard.GetText();
            switch (clipboard)
            {
                case "start stopwatch": //start
                    {
                        if (myStopwatch?.IsRunning == true)
                        {
                            return new QuestionAnswer("Stopwatch", "Stopwatch already running");
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
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    return new QuestionAnswer("Stopwatch reset to", $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds");
                                case 1043: // dutch
                                    return new QuestionAnswer("Stopwatch gereset naar", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                                default:
                                    return new QuestionAnswer();
                            }
                        }
                        break;
                    }
                case "pause stopwatch": //  pause
                    {
                        if (clipboard != lastClipboard)
                        {
                            lastClipboard = clipboard;
                            TimeSpan ts = myStopwatch.Elapsed;
                            myStopwatch.Stop();

                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    return new QuestionAnswer("Stopwatch paused on", $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds");
                                case 1043: // dutch
                                    return new QuestionAnswer("Stopwatch gepauzeerd op", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                                default:
                                    return new QuestionAnswer();
                            }
                        }
                        break;
                    }
                case "resume stopwatch":
                    {
                        if (clipboard != lastClipboard)
                        {
                            lastClipboard = clipboard;
                            TimeSpan ts = myStopwatch.Elapsed;
                            myStopwatch.Start();
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    return new QuestionAnswer("Stopwatch resumed from", $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds");
                                case 1043: // dutch
                                    return new QuestionAnswer("Stopwatch gepauzeerd op", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                                default:
                                    return new QuestionAnswer();
                            }
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
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    return new QuestionAnswer("Elapsed time", $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds");
                                case 1043: // dutch
                                    return new QuestionAnswer("Elapsed time", $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes");
                                default:
                                    return new QuestionAnswer();
                            }
                        }
                        else
                            return new QuestionAnswer(isProcessed: false);

                    }
            }


            return new QuestionAnswer(isProcessed: false);
        }
    }
}