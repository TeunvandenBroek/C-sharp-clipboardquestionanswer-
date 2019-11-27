using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace it.Actions
{
    public class StopwatchActions : IAction
    {
        private string lastClipboard;
        private Stopwatch stopwatch = new Stopwatch();

        public StopwatchActions()
        {
            if (stopwatch.IsRunning) stopwatch.Stop();
        }


        public bool Matches(string clipboardText)
        {
            return clipboardText.IndexOf("stopwatch", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public ActionResult TryExecute(string clipboardText)
        {
            var actionResult = new ActionResult {Title = "Stopwatch"};
            var currentCulture = Thread.CurrentThread.CurrentCulture;

            // moved this from every case statement to up here. Not sure what to do if the clipboard text matches.
            // I don't think this code is needed at all.
            if (!string.Equals(clipboardText, lastClipboard, StringComparison.Ordinal)) lastClipboard = clipboardText;


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
                        actionResult.Description = FillStopwatchReset(stopwatch.Elapsed);

                        lastClipboard = clipboardText;
                        stopwatch.Reset();
                        stopwatch = new Stopwatch();
                        stopwatch.Start();
                        var ts = stopwatch.Elapsed;
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Stopwatch reset to";
                                actionResult.Description =
                                    $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Stopwatch gereset naar";
                                actionResult.Description =
                                    $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
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
                        actionResult.Description = FillStopwatchPause(stopwatch.Elapsed);

                        lastClipboard = clipboardText;
                        var ts = stopwatch.Elapsed;
                        stopwatch.Stop();

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Stopwatch paused on";
                                actionResult.Description =
                                    $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Stopwatch gepauzeerd op";
                                actionResult.Description =
                                    $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
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
                        actionResult.Description = FillStopwatchResume(stopwatch.Elapsed);

                        lastClipboard = clipboardText;
                        var ts = stopwatch.Elapsed;
                        stopwatch.Start();
                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                actionResult.Title = "Stopwatch resumed from";
                                actionResult.Description =
                                    $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                                break;
                            case 1043: // dutch
                                actionResult.Title = "Stopwatch gepauzeerd op";
                                actionResult.Description =
                                    $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                                break;
                            default:
                                actionResult.IsProcessed = false;
                                return actionResult;
                        }
                    }
                }
                {
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
                            actionResult.Description = FillStopwatchStop(stopwatch.Elapsed);
                        }
                    }
                    lastClipboard = null;
                    stopwatch.Stop();
                    var ts = stopwatch.Elapsed;
                    switch (currentCulture.LCID)
                    {
                        case 1033: // english-us
                            actionResult.Title = "Elapsed time";
                            actionResult.Description =
                                $"{ts.Hours} hours, {ts.Minutes} minutes,  {ts.Seconds} seconds";
                            break;
                        case 1043: // dutch
                            actionResult.Title = "Elapsed time";
                            actionResult.Description =
                                $"{ts.Hours} uur, {ts.Minutes} minuten,  {ts.Seconds}secondes";
                            break;
                        default:
                            actionResult.IsProcessed = false;
                            return actionResult;
                    }

                    break;
                }
            }

            return actionResult;
        }

        #region Stopwatch Fill Methods

        private string GetElaspedTime(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description =
                        $"{stopwatch.Elapsed.Hours} hours, {stopwatch.Elapsed.Hours} minutes,  {stopwatch.Elapsed.Hours} seconds";
                    break;
                case 1043: // dutch
                    description =
                        $"{stopwatch.Elapsed.Hours} uur, {stopwatch.Elapsed.Hours} minuten,  {stopwatch.Elapsed.Hours}secondes";
                    break;
            }

            return description;
        }

        private string FillStopwatchAlreadyRunning(CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = "Stopwatch already running!";
                    break;
                case 1043: // dutch
                    description = "Stopwatch al actief!";
                    break;
            }

            return description;
        }

        private string FillStopwatchNotRunning(CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = "Stopwatch is not running!";
                    break;
                case 1043: // dutch
                    description = "Stopwatch wordt niet uitgevoerd!";
                    break;
            }

            return description;
        }

        private string FillStopwatchStarted(CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = "Stopwatch started";
                    break;
                case 1043: // dutch
                    description = "Stopwatch gestart";
                    break;
            }

            return description;
        }

        private string FillStopwatchReset(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = $"Stopwatch reset to: {GetElaspedTime(timeSpan)}";
                    break;
                case 1043: // dutch
                    description = $"Stopwatch resetten naar: {GetElaspedTime(timeSpan)}";
                    break;
            }

            return description;
        }

        private string FillStopwatchPause(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = $"Stopwatch paused on: {GetElaspedTime(timeSpan)}";
                    break;
                case 1043: // dutch
                    description = $"Stopwatch gepauzeerd op: {GetElaspedTime(timeSpan)}";
                    break;
            }

            return description;
        }

        private string FillStopwatchResume(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = $"Stopwatch resumed on: {GetElaspedTime(timeSpan)}";
                    break;
                case 1043: // dutch
                    description = $"Stopwatch hervat op: {GetElaspedTime(timeSpan)}";
                    break;
            }

            return description;
        }

        private string FillStopwatchStop(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture == null) currentCulture = Thread.CurrentThread.CurrentCulture;
            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = $"Stopwatch stopped on: {GetElaspedTime(timeSpan)}";
                    break;
                case 1043: // dutch
                    description = $"Stopwatch gestopt op: {GetElaspedTime(timeSpan)}";
                    break;
            }

            return description;
        }

        #endregion Stopwatch Fill Methods
    }
}