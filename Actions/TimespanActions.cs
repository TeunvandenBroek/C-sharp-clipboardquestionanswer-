namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public sealed class TimespanActions : IAction, IEquatable<TimespanActions>
    {

        private readonly string[] dateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy", 
            //american format
            "MM.dd.yyyy",
            "MM-dd-yyyy",
        };
        private bool isUsingTimespan;

        private DateTimeOffset? prevDate;

        public bool Matches(string clipboardText)
        {
            return DateTimeOffset.TryParseExact(clipboardText, dateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out _);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            isUsingTimespan = !isUsingTimespan;
            ActionResult actionResult = new ActionResult();
            if (DateTimeOffset.TryParseExact(clipboardText, dateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTimeOffset newDate))
            {
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

        public override bool Equals(object obj)
        {
            return Equals(obj as TimespanActions);
        }

        public override int GetHashCode()
        {
            var hashCode = 953797473;
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(dateFormats);
            hashCode = hashCode * -1521134295 + isUsingTimespan.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(prevDate);
            return hashCode;
        }

        public static bool operator ==(TimespanActions left, TimespanActions right)
        {
            return EqualityComparer<TimespanActions>.Default.Equals(left, right);
        }

        public static bool operator !=(TimespanActions left, TimespanActions right)
        {
            return !(left == right);
        }
    }
}