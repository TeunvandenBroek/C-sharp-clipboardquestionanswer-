//TO DO

namespace it
{
    public class TryCalcBmi : IAction
    {
        private readonly Form1 form1;

        public TryCalcBmi(Form1 form1)
        {
            this.form1 = form1;
        }

        public bool TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi"))
            {
            }

            return true;
        }

        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }
    }
}