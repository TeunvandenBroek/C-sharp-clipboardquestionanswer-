namespace it
{
    using System;
    using System.Globalization;

    public class TimespanActions : IAction
    {
        private readonly Form1 form1;

        private DateTime? prevDate;

        public TimespanActions(Form1 form1)
        {
            this.form1 = form1;
        }

        private readonly string[] DateFormats =
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy"
        };

        private void ShowNotification(string question, string answer) => form1.ShowNotification(question, answer);

        public bool TryExecute(string clipboardText)
        {
            if (DateTime.TryParseExact(clipboardText, DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime newDate))
            {
                if (prevDate is object)
                {
                    TimeSpan? difference = newDate - prevDate;
                    if (difference is object)
                    {
                        ShowNotification("Days between:", difference.Value.Days.ToString(CultureInfo.InvariantCulture));
                    }
                    prevDate = null;
                }
                else
                {
                    prevDate = newDate;
                }
                return true;
            }
            prevDate = null;
            return false;
        }
    }
}
