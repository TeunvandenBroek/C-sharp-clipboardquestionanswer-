namespace it.Actions
{
    using System;
    using System.Collections.Generic;
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
                        bool isHeads = random.NextDouble() > 0.5;

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
                                break;
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
                            _ = password.Append(charAvailable[random.Next(charAvailable.Length)]);
                        }

                        actionResult.Title = "Random password";
                        actionResult.Description = password.ToString();
                    }
                    break;
                default:
                    break;
            }

            return actionResult;
        }

        public bool Equals(RandomActions other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RandomActions);
        }

        public override int GetHashCode()
        {
            var hashCode = 289468973;
            hashCode = hashCode * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(commands);
            hashCode = hashCode * -1521134295 + EqualityComparer<Random>.Default.GetHashCode(random);
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(RandomActions left, RandomActions right)
        {
            return EqualityComparer<RandomActions>.Default.Equals(left, right);
        }

        public static bool operator !=(RandomActions left, RandomActions right)
        {
            return !(left == right);
        }
    }
}