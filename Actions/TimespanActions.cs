namespace it.Actions
{
    using System;
    using System.Globalization;

    public class TimespanActions : IAction
    {
        private DateTime? prevDate;
        private readonly string[] DateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy"
        };
        public int Priority => 0;

        public bool Matches(string clipboardText)
        {
            return DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult(isProcessed: false);

            if (DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate))
            {
                actionResult.IsProcessed = true;

                if (prevDate.HasValue)
                {
                    TimeSpan? difference = newDate - prevDate;
                    if (difference.HasValue)
                    {
                        prevDate = null;
                        actionResult.Title = "Days between:";
                        actionResult.Description = difference.Value.Days.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    prevDate = newDate;
                }
            }

            return actionResult;
        }
    }
}
