namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal sealed class TimezoneActions : IAction
    {
        private string country;
        private string timeZoneId = string.Empty;

        public override bool Equals(object obj)
        {
            return Equals(obj as TimezoneActions);
        }


        public bool Matches(string clipboardText)
        {
            if (clipboardText is null)
            {
                throw new ArgumentNullException(nameof(clipboardText));
            }

            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> keyValuePair = TryKeypair();
            return keyValuePair.Key != default;
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> keyValuePair = TryKeypair();

            actionResult.Title = country;
            switch (keyValuePair.Value)
            {
                case Countries.UtcOffset.UtcMinusTwelve:
                    {
                        timeZoneId = "Dateline Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusEleven:
                    {
                        timeZoneId = "Samoa Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusTen:
                    {
                        timeZoneId = "Hawaiian Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusNine:
                    {
                        timeZoneId = "Alaskan Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusEight:
                    {
                        timeZoneId = "Pacific Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusSeven:
                    {
                        timeZoneId = "US Mountain Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusSix:
                    {
                        timeZoneId = "Central America Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusFive:
                    {
                        timeZoneId = "SA Pacific Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusFour:
                    {
                        timeZoneId = "Atlantic Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusThreepoinfive:
                    {
                        timeZoneId = "Newfoundland Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusThree:
                    {
                        timeZoneId = "E. South America Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusTwo:
                    {
                        timeZoneId = "Mid-Atlantic Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcMinusOne:
                    {
                        timeZoneId = "Cape Verde Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcZero:
                    {
                        timeZoneId = "GMT Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusOne:
                    {
                        timeZoneId = "Central European Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusTwo:
                    {
                        timeZoneId = "Jordan Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusThree:
                    {
                        timeZoneId = "Arabic Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusThreepoinfive:
                    {
                        timeZoneId = "Iran Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusFour:
                    {
                        timeZoneId = "Mauritius Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusFourpointfive:
                    {
                        timeZoneId = "Afghanistan Standard Time"; 
                        break;
                    }

                case Countries.UtcOffset.UtcPlusFive:
                    {
                        timeZoneId = "Ekaterinburg Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusSix:
                    {
                        timeZoneId = "N. Central Asia Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusSeven:
                    {
                        timeZoneId = "SE Asia Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusEight:
                    {
                        timeZoneId = "China Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusNine:
                    {
                        timeZoneId = "Korea Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusTen:
                    {
                        timeZoneId = "E. Australia Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusEleven:
                    {
                        timeZoneId = "Central Pacific Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusTwelve:
                    {
                        timeZoneId = "New Zealand Standard Time";
                        break;
                    }

                case Countries.UtcOffset.UtcPlusThirteen:
                    {
                        timeZoneId = "Tonga Standard Time";
                        break;
                    }
                case Countries.UtcOffset.UtcPlusFivepointfive:
                    {
                        timeZoneId = "India Standard Time";
                        break;
                    }
                default:
                    break;
            }

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTimeOffset dateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, timeZoneInfo);
            actionResult.Description = dateTime.ToString("HH:mm", CultureInfo.CurrentCulture);
            return actionResult;
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