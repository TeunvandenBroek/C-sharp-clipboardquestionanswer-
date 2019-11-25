using System;
using System.Globalization;

namespace it.Actions
{
    public class TimespanActions : IAction
    {
        private bool IsUsingTimespan = false;

        private readonly string[] DateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy"
        };

        private DateTime? prevDate;
        public int Priority => 0;

        public bool Matches(string clipboardText)
        {
            return DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            IsUsingTimespan = !IsUsingTimespan;
            var actionResult = new ActionResult(isProcessed: false);

            if (DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal, out var newDate))
            {
                actionResult.IsProcessed = true;

                if (prevDate.HasValue)
                {
                    var difference = newDate - prevDate;
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