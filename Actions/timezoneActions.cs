namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class TimezoneActions : IAction
    {

        private string country;

        QuestionAnswer IAction.TryExecute(string clipboardText)
        {
            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> keyValuePair = TryKeypair();
            if (keyValuePair.Key != (default))
            {
                switch (keyValuePair.Value)
                {
                    case Countries.UtcOffset.UtcMinusTwelve:
                        {
                            return new QuestionAnswer("Dateline Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusEleven:
                        {
                            return new QuestionAnswer("Samoa Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusTen:
                        {
                            return new QuestionAnswer("Hawaiian Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusNine:
                        {
                            return new QuestionAnswer("Alaskan Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusEight:
                        {
                            return new QuestionAnswer("Pacific Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusSeven:
                        {
                            return new QuestionAnswer("US Mountain Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusSix:
                        {
                            return new QuestionAnswer("Central America Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusFive:
                        {
                            return new QuestionAnswer("SA Pacific Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusFour:
                        {
                            return new QuestionAnswer("Atlantic Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusThreepoinfive:
                        {
                            return new QuestionAnswer("Newfoundland Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusThree:
                        {
                            return new QuestionAnswer("E. South America Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusTwo:
                        {
                            return new QuestionAnswer("Mid-Atlantic Standard Time");
                        }
                    case Countries.UtcOffset.UtcMinusOne:
                        {
                            return new QuestionAnswer("Cape Verde Standard Time");
                        }
                    case Countries.UtcOffset.UtcZero:
                        {
                            return new QuestionAnswer("GMT Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusOne:
                        {
                            return new QuestionAnswer(country, DateTime.Now.ToString("HH:mm", CultureInfo.CurrentCulture));
                        }
                    case Countries.UtcOffset.UtcPlusTwo:
                        {
                            return new QuestionAnswer("Jordan Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusThree:
                        {
                            return new QuestionAnswer("Arabic Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusThreepoinfive:
                        {
                            return new QuestionAnswer("Iran Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusFour:
                        {
                            return new QuestionAnswer("Mauritius Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusFourpointfive:
                        {
                            return new QuestionAnswer("Afghanistan Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusFive:
                        {
                            return new QuestionAnswer("Ekaterinburg Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusSix:
                        {
                            return new QuestionAnswer("N. Central Asia Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusSeven:
                        {
                            return new QuestionAnswer("SE Asia Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusEight:
                        {
                            return new QuestionAnswer("China Standard Time ");
                        }
                    case Countries.UtcOffset.UtcPlusNine:
                        {
                            return new QuestionAnswer("Korea Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusTen:
                        {
                            return new QuestionAnswer("E. Australia Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusEleven:
                        {
                            return new QuestionAnswer("Central Pacific Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusTwelve:
                        {
                            return new QuestionAnswer("New Zealand Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusThirteen:
                        {
                            return new QuestionAnswer("Tonga Standard Time");
                        }
                    case Countries.UtcOffset.UtcPlusFivepointfive:
                        {
                            return new QuestionAnswer("India Standard Time");
                        }
                    default:
                        {
                            return new QuestionAnswer("Unknown");
                        }
                }
            }
            else
            {
                return new QuestionAnswer(isSuccessful: true);
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
