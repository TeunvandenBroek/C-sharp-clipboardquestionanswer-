namespace it.Actions
{
    using System;
    using System.Text;
    using System.Threading;

    internal sealed class RandomActions : IAction, IEquatable<RandomActions>
    {
        private readonly string[] commands = { "kop of munt", "heads or tails", "random password" };
        private readonly Random random = new Random();

        public bool Matches(string clipboardText)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                string command = commands[i];
                if (command.Equals(clipboardText, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }


        ActionResult IAction.TryExecute(string clipboardText)
        {
            System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            ActionResult actionResult = new ActionResult();

            switch (clipboardText)
            {
                case "kop of munt":
                case "heads or tails":
                    {
                        bool isHeads = (int)random.NextDouble() % 2 > 0;

                        switch (currentCulture.LCID)
                        {
                            case 1033: // english-us
                                {
                                    actionResult.Title = "heads or tails?";
                                    actionResult.Description = isHeads ? "Heads" : "Tails";
                                    break;
                                }
                            case 1043: // dutch
                                {
                                    actionResult.Title = "Kop of munt?";
                                    actionResult.Description = isHeads ? "Kop" : "Munt";
                                    break;
                                }
                            default:
                                {
                                    actionResult.IsProcessed = false;
                                    break;
                                }
                        }

                        break;
                    }
                case "random password":
                    {
                        const int minLength = 8;
                        const int maxLength = 12;
                        const string charAvailable = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789-";
                        StringBuilder password = new StringBuilder();
                        int passwordLength = random.Next(minLength, maxLength + 1);
                        while (passwordLength-- > 0)
                        {
                            password.Append(charAvailable[random.Next(charAvailable.Length)]);
                        }

                        actionResult.Title = "Random password";
                        actionResult.Description = password.ToString();
                    }
                    break;
                default:
                    {
                        actionResult.IsProcessed = false;
                        break;
                    }
            }

            return actionResult;
        }

        public bool Equals(RandomActions other)
        {
            throw new NotImplementedException();
        }
    }
}