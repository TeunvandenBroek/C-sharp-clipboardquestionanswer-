using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace it.Actions
{
    internal class RandomActions : IAction
    {
        private readonly Random _random = new Random();
        QuestionAnswer IAction.TryExecute(string clipboardText)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            switch (clipboardText)
            {
                case "kop of munt":
                case "heads or tails":
                    {
                        bool isHeads = (int)(_random.NextDouble()) % 2 > 0;

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                return new QuestionAnswer("heads or tails?", isHeads ? "Heads" : "Tails");
                            case 1043: // dutch
                                return new QuestionAnswer("Kop of munt?", isHeads ? "Kop" : "Munt");
                            default:
                                return null;
                        }

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
                        }
                        return new QuestionAnswer("Random password", password.ToString());
                    }
            }
            return new QuestionAnswer(isSuccessful: true);
        }
    }
}
