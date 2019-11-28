namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal sealed class TimezoneActions : IAction
    {
        private  string country;
        private string timeZoneId = string.Empty;

        public bool Matches(string clipboardText)
        {
            this.country = clipboardText.Trim().ToLowerInvariant();
            var keyValuePair = this.TryKeypair();
            return keyValuePair.Key != default;
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            var actionResult = new ActionResult();
            this.country = clipboardText.Trim().ToLowerInvariant();
            var keyValuePair = this.TryKeypair();
            if (keyValuePair.Key == default)
            {
                actionResult.IsProcessed = false;
                return actionResult;
            }

            actionResult.Title = this.country;
            switch (keyValuePair.Value)
            {
                case Countries.UtcOffset.UtcMinusTwelve:
                {
                    this.timeZoneId = "Dateline Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusEleven:
                {
                    this.timeZoneId = "Samoa Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusTen:
                {
                    this.timeZoneId = "Hawaiian Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusNine:
                {
                    this.timeZoneId = "Alaskan Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusEight:
                {
                    this.timeZoneId = "Pacific Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusSeven:
                {
                    this.timeZoneId = "US Mountain Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusSix:
                {
                    this.timeZoneId = "Central America Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusFive:
                {
                    this.timeZoneId = "SA Pacific Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusFour:
                {
                    this.timeZoneId = "Atlantic Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusThreepoinfive:
                {
                    this.timeZoneId = "Newfoundland Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusThree:
                {
                    this.timeZoneId = "E. South America Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusTwo:
                {
                    this.timeZoneId = "Mid-Atlantic Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcMinusOne:
                {
                    this.timeZoneId = "Cape Verde Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcZero:
                {
                    this.timeZoneId = "GMT Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusOne:
                {
                    this.timeZoneId = "Central European Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusTwo:
                {
                    this.timeZoneId = "Jordan Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusThree:
                {
                    this.timeZoneId = "Arabic Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusThreepoinfive:
                {
                    this.timeZoneId = "Iran Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusFour:
                {
                    this.timeZoneId = "Mauritius Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusFourpointfive:
                {
                    this.timeZoneId = "Afghanistan Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusFive:
                {
                    this.timeZoneId = "Ekaterinburg Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusSix:
                {
                    this.timeZoneId = "N. Central Asia Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusSeven:
                {
                    this.timeZoneId = "SE Asia Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusEight:
                {
                    this.timeZoneId = "China Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusNine:
                {
                    this.timeZoneId = "Korea Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusTen:
                {
                    this.timeZoneId = "E. Australia Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusEleven:
                {
                    this.timeZoneId = "Central Pacific Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusTwelve:
                {
                    this.timeZoneId = "New Zealand Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusThirteen:
                {
                    this.timeZoneId = "Tonga Standard Time";
                    break;
                }

                case Countries.UtcOffset.UtcPlusFivepointfive:
                {
                    this.timeZoneId = "India Standard Time";
                    break;
                }

                default:
                {
                    actionResult.IsProcessed = false;
                    return actionResult;
                }
            }

            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(this.timeZoneId);
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            actionResult.Description = dateTime.ToString("HH:mm", CultureInfo.CurrentCulture);
            return actionResult;
        }

        private KeyValuePair<string, Countries.UtcOffset> TryKeypair()
        {
            bool predicate(KeyValuePair<string, Countries.UtcOffset> x)
            {
                return x.Key.Contains(this.country);
            }

            return Countries.UtcOffsetByCountry.FirstOrDefault(predicate);
        }
    }
}