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

        public QuestionAnswer TryExecute(string clipboardText)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer(isProcessed: false);

            if (DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate))
            {
                questionAnswer.IsProcessed = true;

                if (prevDate.HasValue)
                {
                    TimeSpan? difference = newDate - prevDate;
                    if (difference.HasValue)
                    {
                        prevDate = null;
                        questionAnswer.Question = "Days between:";
                        questionAnswer.Answer = difference.Value.Days.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    prevDate = newDate;
                }
            }

            return questionAnswer;
        }
    }
}
