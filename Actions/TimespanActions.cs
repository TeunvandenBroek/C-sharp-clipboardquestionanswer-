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

        QuestionAnswer IAction.TryExecute(string clipboardText)
        {
            if (DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate))
            {
                if (prevDate is object)
                {
                    TimeSpan? difference = newDate - prevDate;
                    if (difference is object)
                    {
                        return new QuestionAnswer("Days between:", difference.Value.Days.ToString(CultureInfo.InvariantCulture));
                    }
                    prevDate = null;
                }
                else
                {
                    prevDate = newDate;
                }
                return new QuestionAnswer(isSuccessful: true);
            }
            prevDate = null;
            return new QuestionAnswer(isSuccessful: true);
        }
    }
}
