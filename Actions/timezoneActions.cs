namespace it.Actions
{
    using System.Collections.Generic;
    using System.Linq;

    public class TimezoneActions : IAction
    {

        private string country;

        ActionResult IAction.TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();

            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> keyValuePair = TryKeypair();
            if (keyValuePair.Key == (default))
            {
                actionResult.IsProcessed = false;
            }
            else
            {
                actionResult.Title = country;

                switch (keyValuePair.Value)
                {
                    case Countries.UtcOffset.UtcMinusTwelve:
                        {
                            actionResult.Description = "Dateline Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusEleven:
                        {
                            actionResult.Description = "Samoa Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusTen:
                        {
                            actionResult.Description = "Hawaiian Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusNine:
                        {
                            actionResult.Description = "Alaskan Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusEight:
                        {
                            actionResult.Description = "Pacific Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusSeven:
                        {
                            actionResult.Description = "US Mountain Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusSix:
                        {
                            actionResult.Description = "Central America Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusFive:
                        {
                            actionResult.Description = "SA Pacific Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusFour:
                        {
                            actionResult.Description = "Atlantic Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusThreepoinfive:
                        {
                            actionResult.Description = "Newfoundland Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusThree:
                        {
                            actionResult.Description = "E. South America Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusTwo:
                        {
                            actionResult.Description = "Mid-Atlantic Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcMinusOne:
                        {
                            actionResult.Description = "Cape Verde Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcZero:
                        {
                            actionResult.Description = "GMT Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusOne:
                        {
                            actionResult.Description = "Central European Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusTwo:
                        {
                            actionResult.Description = "Jordan Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusThree:
                        {
                            actionResult.Description = "Arabic Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusThreepoinfive:
                        {
                            actionResult.Description = "Iran Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusFour:
                        {
                            actionResult.Description = "Mauritius Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusFourpointfive:
                        {
                            actionResult.Description = "Afghanistan Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusFive:
                        {
                            actionResult.Description = "Ekaterinburg Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusSix:
                        {
                            actionResult.Description = "N. Central Asia Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusSeven:
                        {
                            actionResult.Description = "SE Asia Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusEight:
                        {
                            actionResult.Description = "China Standard Time ";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusNine:
                        {
                            actionResult.Description = "Korea Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusTen:
                        {
                            actionResult.Description = "E. Australia Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusEleven:
                        {
                            actionResult.Description = "Central Pacific Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusTwelve:
                        {
                            actionResult.Description = "New Zealand Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusThirteen:
                        {
                            actionResult.Description = "Tonga Standard Time";
                            break;
                        }
                    case Countries.UtcOffset.UtcPlusFivepointfive:
                        {
                            actionResult.Description = "India Standard Time";
                            break;
                        }
                    default:
                        {
                            actionResult.Description = "Unknown time zone";
                            break;
                        }
                }
            }

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
