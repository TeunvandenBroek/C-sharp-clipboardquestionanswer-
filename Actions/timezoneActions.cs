namespace it.Actions
{
    using System.Collections.Generic;
    using System.Linq;

    public class TimezoneActions : IAction
    {

        private string country;

        QuestionAnswer IAction.TryExecute(string clipboardText)
        {
            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> keyValuePair = TryKeypair();
            if (keyValuePair.Key == (default))
            {
                return new QuestionAnswer(isProcessed: false);
            }

            switch (keyValuePair.Value)
            {
                case Countries.UtcOffset.UtcMinusTwelve:
                    {
                        return new QuestionAnswer(country, "Dateline Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusEleven:
                    {
                        return new QuestionAnswer(country, "Samoa Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusTen:
                    {
                        return new QuestionAnswer(country, "Hawaiian Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusNine:
                    {
                        return new QuestionAnswer(country, "Alaskan Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusEight:
                    {
                        return new QuestionAnswer(country, "Pacific Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusSeven:
                    {
                        return new QuestionAnswer(country, "US Mountain Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusSix:
                    {
                        return new QuestionAnswer(country, "Central America Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusFive:
                    {
                        return new QuestionAnswer(country, "SA Pacific Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusFour:
                    {
                        return new QuestionAnswer(country, "Atlantic Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusThreepoinfive:
                    {
                        return new QuestionAnswer(country, "Newfoundland Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusThree:
                    {
                        return new QuestionAnswer(country, "E. South America Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusTwo:
                    {
                        return new QuestionAnswer(country, "Mid-Atlantic Standard Time");
                    }
                case Countries.UtcOffset.UtcMinusOne:
                    {
                        return new QuestionAnswer(country, "Cape Verde Standard Time");
                    }
                case Countries.UtcOffset.UtcZero:
                    {
                        return new QuestionAnswer(country, "GMT Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusOne:
                    {
                        return new QuestionAnswer(country, "Central European Time");
                    }
                case Countries.UtcOffset.UtcPlusTwo:
                    {
                        return new QuestionAnswer(country, "Jordan Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusThree:
                    {
                        return new QuestionAnswer(country, "Arabic Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusThreepoinfive:
                    {
                        return new QuestionAnswer(country, "Iran Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusFour:
                    {
                        return new QuestionAnswer(country, "Mauritius Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusFourpointfive:
                    {
                        return new QuestionAnswer(country, "Afghanistan Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusFive:
                    {
                        return new QuestionAnswer(country, "Ekaterinburg Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusSix:
                    {
                        return new QuestionAnswer(country, "N. Central Asia Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusSeven:
                    {
                        return new QuestionAnswer(country, "SE Asia Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusEight:
                    {
                        return new QuestionAnswer(country, "China Standard Time ");
                    }
                case Countries.UtcOffset.UtcPlusNine:
                    {
                        return new QuestionAnswer(country, "Korea Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusTen:
                    {
                        return new QuestionAnswer(country, "E. Australia Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusEleven:
                    {
                        return new QuestionAnswer(country, "Central Pacific Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusTwelve:
                    {
                        return new QuestionAnswer(country, "New Zealand Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusThirteen:
                    {
                        return new QuestionAnswer(country, "Tonga Standard Time");
                    }
                case Countries.UtcOffset.UtcPlusFivepointfive:
                    {
                        return new QuestionAnswer(country, "India Standard Time");
                    }
                default:
                    {
                        return new QuestionAnswer(country, "Unknown time zone");
                    }
            }

        }

        private KeyValuePair<string, Countries.UtcOffset> TryKeypair()
        {
            bool predicate(KeyValuePair<string, Countries.UtcOffset> x)
            {
                return x.Key.Contains(country);
            }

            return Countries.UtcOffsetByCountry.FirstOrDefault(predicate);
        }
    }
}
