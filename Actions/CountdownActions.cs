using System;
using System.Threading.Tasks;

namespace it
{
    public class CountdownActions : IAction
    {
        private readonly Form1 form1;

        public CountdownActions(Form1 form1)
        {
            this.form1 = form1;
        }

        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }

        public bool TryExecute(string clipboardText)
        {
            return TryCountDown(clipboardText);
        }
        private bool TryCountDown(string clipboardText)
        {
            if (clipboardText.StartsWith("timer") && TimeSpan.TryParse(clipboardText.Replace("timer ", ""), out TimeSpan ts))
            {
                async Task p()
                {
                    await Task.Delay(ts).ConfigureAwait(false);
                    ShowNotification("Countdown timer", "time is over");
                }
                Task.Run(p);
                return true;
            }
            return false;
        }
    }
}
