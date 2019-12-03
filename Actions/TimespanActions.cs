namespace it.Actions
{
    using System;
    using System.Globalization;

    public sealed class TimespanActions : IAction, IEquatable<TimespanActions>
    {

        private readonly string[] dateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy",
        };
        private bool isUsingTimespan;

        private DateTimeOffset? prevDate;

        public bool Matches(string clipboardText = null)
        {
            return DateTimeOffset.TryParseExact(clipboardText, formats: this.dateFormats, formatProvider: CultureInfo.CurrentCulture, styles: DateTimeStyles.AssumeLocal, result: out DateTimeOffset newDate);
        }

        public ActionResult TryExecute(string clipboardText = null)
        {
            this.isUsingTimespan = !this.isUsingTimespan;
            var actionResult = new ActionResult(isProcessed: false);
            if (DateTimeOffset.TryParseExact(clipboardText, formats: this.dateFormats, CultureInfo.CurrentCulture,  DateTimeStyles.AssumeLocal, result: out var newDate))
            {
                actionResult.IsProcessed = true;

                if (this.prevDate.HasValue)
                {
                    var difference = newDate - this.prevDate;
                    this.prevDate = null;
                    actionResult.Title = "Days between:";
                    actionResult.Description = difference.Value.Days.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    this.prevDate = newDate;
                }
            }

            return actionResult;
        }

        public bool Equals(TimespanActions other)
        {
            throw new NotImplementedException();
        }
    }
}