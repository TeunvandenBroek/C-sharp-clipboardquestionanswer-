using System;
using System.Text;

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

        private readonly Random _random = new Random();
        public bool TryExecute(string clipboardText)
        {
            return TryRandomActions(clipboardText);
        }

        private bool TryRandomActions(string clipboardText)
        {
            switch (clipboardText)
            {
                case "kop of munt":
                    {
                        if (_random.NextDouble() < 0.5)
                        {
                            ShowNotification("Kop of munt?", "Kop");
                        }
                        else
                        {
                            ShowNotification("Kop of munt?", "Munt");
                        }
                        return true;
                    }
                case "random password":
                    {
                        const int minLength = 8;
                        const int maxLength = 12;
                        const string charAvailable = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789-";
                        StringBuilder password = new StringBuilder();
                        int passwordLength = _random.Next(minLength, maxLength + 1);
                        while (passwordLength-- > 0)
                        {
                            password.Append(charAvailable[_random.Next(charAvailable.Length)]);
                            ShowNotification("Random password", password.ToString());
                        }
                        return true;
                    }
            }
            return false;
        }
    }
}
