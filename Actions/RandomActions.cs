using System;

namespace it
{
    public class RandomActions : IAction
    {
        private readonly Form1 form1;

        public RandomActions(Form1 form1)
        {
            this.form1 = form1;
        }
        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }

        public bool TryExecute(string clipboardText)
        {
            return TryRandomActions(clipboardText);
        }

        private readonly Random _random = new Random();
        private bool TryRandomActions(string clipboardText)
        {
            switch (clipboardText)
            {
                case "kop of munt":
                {
                    ShowNotification("Kop of munt?", _random.NextDouble() < 0.5 ? "Kop" : "Munt");

                    return true;
                }
                default:
                    return false;
            }
        }
    }
}