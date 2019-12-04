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

        public bool Matches(string clipboardText)
        {
            return DateTimeOffset.TryParseExact(clipboardText, formats: dateFormats, formatProvider: CultureInfo.CurrentCulture, styles: DateTimeStyles.AssumeLocal, result: out DateTimeOffset newDate);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            isUsingTimespan = !isUsingTimespan;
            ActionResult actionResult = new ActionResult(isProcessed: false);
            if (DateTimeOffset.TryParseExact(clipboardText, formats: dateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, result: out DateTimeOffset newDate))
            {
                actionResult.IsProcessed = true;

                if (prevDate.HasValue)
                {
                    TimeSpan? difference = newDate - prevDate;
                    prevDate = null;
                    actionResult.Title = "Days between:";
                    actionResult.Description = difference.Value.Days.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    prevDate = newDate;
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