namespace it.Actions
{
    using System;
    using System.Globalization;

    public class TimespanActions : IAction
    {

        private readonly string[] dateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy",
        };
        private bool isUsingTimespan = false;

        private DateTime? prevDate;

        public int Priority => 0;

        public bool Matches(string clipboardText)
        {
            return DateTime.TryParseExact(s: clipboardText, formats: this.dateFormats, provider: CultureInfo.CurrentCulture, style: DateTimeStyles.AssumeLocal, result: out DateTime newDate);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            this.isUsingTimespan = !this.isUsingTimespan;
            var actionResult = new ActionResult(isProcessed: false);
            if (DateTime.TryParseExact(s: clipboardText, formats: this.dateFormats, provider: CultureInfo.CurrentCulture, style: DateTimeStyles.AssumeLocal, result: out var newDate))
            {
                actionResult.IsProcessed = true;

                if (this.prevDate.HasValue)
                {
                    var difference = newDate - this.prevDate;
                    if (difference.HasValue)
                    {
                        this.prevDate = null;
                        actionResult.Title = "Days between:";
                        actionResult.Description = difference.Value.Days.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    this.prevDate = newDate;
                }
            }

            return actionResult;
        }
    }
}