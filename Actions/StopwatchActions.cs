using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace it.Actions
{
    public sealed class StopwatchActions : IAction
    {
        private string lastClipboard;

        private Stopwatch stopwatch = new Stopwatch();

        public StopwatchActions()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StopwatchActions);
        }

        public bool Matches(string clipboardText)
        {
            return clipboardText.IndexOf(nameof(stopwatch), StringComparison.Ordinal) >= 0;
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult("Stopwatch");
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            // moved this from every case statement to up here. Not sure what to do if the clipboard
            // text matches. I don't think this code is needed at all.
            if (!string.Equals(clipboardText, lastClipboard, StringComparison.Ordinal))
            {
                lastClipboard = clipboardText;
            }

            switch (clipboardText)
            {
                case "start stopwatch": //start
                    {
                        if (stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchAlreadyRunning();
                        }
                        else
                        {
                            stopwatch.Start();
                            actionResult.Description = FillStopwatchStarted();
                        }

                        break;
                    }
                case "reset stopwatch": //reset
                    {
                        if (stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchAlreadyRunning();
                        }
                        else
                        {
                            stopwatch.Restart();
                            actionResult.Description = FillStopwatchReset();

                            lastClipboard = clipboardText;
                            stopwatch.Reset();
                            stopwatch = new Stopwatch();
                            stopwatch.Start();
                            TimeSpan ts = stopwatch.Elapsed;
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    {
                                        actionResult.Title = "Stopwatch reset to";
                                        actionResult.Description =
                                            $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                        break;
                                    }
                                case 1043: // dutch
                                    {
                                        actionResult.Title = "Stopwatch gereset naar";
                                        actionResult.Description =
                                            $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                        break;
                                    }
                            }
                        }

                        break;
                    }
                case "pause stopwatch": //  pause
                    {
                        if (!stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchNotRunning();
                        }
                        else
                        {
                            stopwatch.Stop();
                            actionResult.Description = FillStopwatchPause();

                            lastClipboard = clipboardText;
                            TimeSpan ts = stopwatch.Elapsed;
                            stopwatch.Stop();

                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    {
                                        actionResult.Title = "Stopwatch paused on";
                                        actionResult.Description =
                                            $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                        break;
                                    }
                                case 1043: // dutch
                                    {
                                        actionResult.Title = "Stopwatch gepauzeerd op";
                                        actionResult.Description =
                                            $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                        break;
                                    }
                            }
                        }

                        break;
                    }
                case "resume stopwatch":
                    {
                        if (stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchAlreadyRunning();
                        }
                        else
                        {
                            stopwatch.Start();
                            actionResult.Description = FillStopwatchResume();

                            lastClipboard = clipboardText;
                            TimeSpan ts = stopwatch.Elapsed;
                            stopwatch.Start();
                            switch (currentCulture.LCID)
                            {
                                case 1033: // english-us
                                    {
                                        actionResult.Title = "Stopwatch resumed from";
                                        actionResult.Description =
                                            $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                        break;
                                    }
                                case 1043: // dutch
                                    {
                                        actionResult.Title = "Stopwatch gepauzeerd op";
                                        actionResult.Description =
                                            $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                        break;
                                    }
                            }
                        }
                    }
                    break;

                case "stop stopwatch": //stop
                    {
                        {
                            if (!stopwatch.IsRunning)
                            {
                                actionResult.Description = FillStopwatchNotRunning();
                            }
                            else
                            {
                                stopwatch.Stop();
                                actionResult.Description = FillStopwatchStop();
                            }
                        }
                        lastClipboard = null;
                        stopwatch.Stop();
                        TimeSpan ts = stopwatch.Elapsed;
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "Elapsed time";
                                    actionResult.Description =
                                        $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "Elapsed time";
                                    actionResult.Description =
                                        $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                    break;
                                }
                        }
                        break;
                    }
            }

            return actionResult;
        }

        private static string FillStopwatchAlreadyRunning(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = "Stopwatch already running!";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = "Stopwatch al actief!";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }

        private static string FillStopwatchNotRunning(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;
            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = "Stopwatch is not running!";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = "Stopwatch wordt niet uitgevoerd!";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }

        private static string FillStopwatchStarted(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = "Stopwatch started";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = "Stopwatch gestart";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }

        private string FillStopwatchPause(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = $"Stopwatch paused on: {GetElaspedTime()}";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = $"Stopwatch gepauzeerd op: {GetElaspedTime()}";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }

        private string FillStopwatchReset(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = $"Stopwatch reset to: {GetElaspedTime()}";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = $"Stopwatch resetten naar: {GetElaspedTime()}";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }

        private string FillStopwatchResume(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = $"Stopwatch resumed on: {GetElaspedTime()}";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = $"Stopwatch hervat op: {GetElaspedTime()}";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }

        private string FillStopwatchStop(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description = $"Stopwatch stopped on: {GetElaspedTime()}";
                        break;
                    }
                case 1043: // dutch
                    {
                        description = $"Stopwatch gestopt op: {GetElaspedTime()}";
                        break;
                    }
            }

            return description;
        }

        private string GetElaspedTime(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        description =
                           $"{stopwatch.Elapsed.Hours} hours, {stopwatch.Elapsed.Hours} minutes,  {stopwatch.Elapsed.Hours} seconds";
                        break;
                    }
                case 1043: // dutch
                    {
                        description =
                           $"{stopwatch.Elapsed.Hours} uur, {stopwatch.Elapsed.Hours} minuten,  {stopwatch.Elapsed.Hours}secondes";
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("Unexpected Case");
                    }
            }

            return description;
        }
    }
}
