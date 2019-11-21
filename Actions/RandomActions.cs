using System;
using System.Text;
using System.Threading;

namespace it.Actions
{
    internal class RandomActions : IAction
    {
        private readonly Random _random = new Random();


        private string[] commands = { "kop of munt", "heads or tails", "random password" };

        public bool Matches(string clipboardText)
        {
            foreach (string command in commands)
            {
                if (command.Equals(clipboardText.ToLower())) {
                    return true;
                }
            }
            return false;
        }


        ActionResult IAction.TryExecute(string clipboardText)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var actionResult = new ActionResult();

            switch (clipboardText)
            {
                case "kop of munt":
                case "heads or tails":
                {
                    var isHeads = (int) _random.NextDouble() % 2 > 0;

                    switch (currentCulture.LCID)
                    {
                        case 1033: // english-us
                            actionResult.Title = "heads or tails?";
                            actionResult.Description = isHeads ? "Heads" : "Tails";
                            break;
                        case 1043: // dutch
                            actionResult.Title = "Kop of munt?";
                            actionResult.Description = isHeads ? "Kop" : "Munt";
                            break;
                    }

                    break;
                }
                case "random password":
                {
                    const int minLength = 8;
                    const int maxLength = 12;
                    const string charAvailable = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789-";
                    var password = new StringBuilder();
                    var passwordLength = _random.Next(minLength, maxLength + 1);
                    while (passwordLength-- > 0) password.Append(charAvailable[_random.Next(charAvailable.Length)]);
                    actionResult.Title = "Random password";
                    actionResult.Description = password.ToString();
                }
                    break;
                default:
                    actionResult.IsProcessed = false;
                    break;
            }

            return actionResult;
        }
    }
}