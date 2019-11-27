namespace it.Actions
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;

    public class StopwatchActions : IAction
    {
        private string lastClipboard;
        private Stopwatch stopwatch = new Stopwatch();

        public StopwatchActions()
        {
            if (this.stopwatch.IsRunning) this.stopwatch.Stop();
        }


        public bool Matches(string clipboardText)
        {
            return clipboardText.IndexOf(nameof(this.stopwatch), StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public ActionResult TryExecute(string clipboardText)
        {
            var actionResult = new ActionResult { Title = "Stopwatch" };
            var currentCulture = Thread.CurrentThread.CurrentCulture;

            // moved this from every case statement to up here. Not sure what to do if the clipboard text matches.
            // I don't think this code is needed at all.
            if (!string.Equals(clipboardText, this.lastClipboard, StringComparison.Ordinal))
            {
                this.lastClipboard = clipboardText;
            }

            switch (clipboardText)
            {
                case "start stopwatch": //start
                    {
                        if (this.stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchAlreadyRunning();
                        }
                        else
                        {
                            this.stopwatch.Start();
                            actionResult.Description = FillStopwatchStarted();
                        }

                        break;
                    }
                case "reset stopwatch": //reset
                    {
                        if (this.stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchAlreadyRunning();
                        }
                        else
                        {
                            this.stopwatch.Restart();
                            actionResult.Description = FillStopwatchReset(this.stopwatch.Elapsed);

                            this.lastClipboard = clipboardText;
                            this.stopwatch.Reset();
                            this.stopwatch = new Stopwatch();
                            this.stopwatch.Start();
                            var ts = this.stopwatch.Elapsed;
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
                        if (!this.stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchNotRunning();
                        }
                        else
                        {
                            this.stopwatch.Stop();
                            actionResult.Description = this.FillStopwatchPause(this.stopwatch.Elapsed);

                            this.lastClipboard = clipboardText;
                            var ts = this.stopwatch.Elapsed;
                            this.stopwatch.Stop();

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
                        if (this.stopwatch.IsRunning)
                        {
                            actionResult.Description = FillStopwatchAlreadyRunning();
                        }
                        else
                        {
                            this.stopwatch.Start();
                            actionResult.Description = this.FillStopwatchResume(this.stopwatch.Elapsed);

                            this.lastClipboard = clipboardText;
                            var ts = this.stopwatch.Elapsed;
                            this.stopwatch.Start();
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
                    break;
                case "stop stopwatch": //stop
                    {
                        {
                            if (!this.stopwatch.IsRunning)
                            {
                                actionResult.Description = FillStopwatchNotRunning();
                            }
                            else
                            {
                                this.stopwatch.Stop();
                                actionResult.Description = FillStopwatchStop(stopwatch.Elapsed);
                            }
                        }
                        this.lastClipboard = null;
                        this.stopwatch.Stop();
                        var ts = this.stopwatch.Elapsed;
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

        private static string FillStopwatchAlreadyRunning(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

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

        private static string FillStopwatchNotRunning(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

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

        private static string FillStopwatchStarted(CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

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

        private string FillStopwatchPause(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = $"Stopwatch paused on: {this.GetElaspedTime(timeSpan)}";
                    break;
                case 1043: // dutch
                    description = $"Stopwatch gepauzeerd op: {this.GetElaspedTime(timeSpan)}";
                    break;
            }

            return description;
        }

        private string FillStopwatchReset(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

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

        private string FillStopwatchResume(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

            string description = null;

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    description = $"Stopwatch resumed on: {this.GetElaspedTime(timeSpan)}";
                    break;
                case 1043: // dutch
                    description = $"Stopwatch hervat op: {this.GetElaspedTime(timeSpan)}";
                    break;
            }

            return description;
        }

        private string FillStopwatchStop(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

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

        #region Stopwatch Fill Methods

        private string GetElaspedTime(TimeSpan timeSpan, CultureInfo currentCulture = null)
        {
            if (currentCulture is null)
            {
                currentCulture = Thread.CurrentThread.CurrentCulture;
            }

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

        #endregion Stopwatch Fill Methods
    }
}