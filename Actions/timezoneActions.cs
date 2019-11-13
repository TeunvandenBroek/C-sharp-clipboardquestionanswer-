namespace it
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class TimezoneActions : IAction
    {
        private readonly Form1 form1;

        public TimezoneActions(Form1 form1)
        {
            this.form1 = form1;
        }
        private void ShowNotification(string timeZoneName)
        {
            form1.ShowNotification(timeZoneName);
        }

        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }

        private string country;

        public bool TryExecute(string clipboardText)
        {
            country = clipboardText.Trim().ToLowerInvariant();
            KeyValuePair<string, Countries.UtcOffset> keyValuePair = TryKeypair();
            if (keyValuePair.Key != (default))
            {
                switch (keyValuePair.Value)
                {
                    case Countries.UtcOffset.UtcMinusTwelve:
                        {
                            ShowNotification("Dateline Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusEleven:
                        {
                            ShowNotification("Samoa Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusTen:
                        {
                            ShowNotification("Hawaiian Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusNine:
                        {
                            ShowNotification("Alaskan Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusEight:
                        {
                            ShowNotification("Pacific Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusSeven:
                        {
                            ShowNotification("US Mountain Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusSix:
                        {
                            ShowNotification("Central America Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusFive:
                        {
                            ShowNotification("SA Pacific Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusFour:
                        {
                            ShowNotification("Atlantic Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusThreepoinfive:
                        {
                            ShowNotification("Newfoundland Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusThree:
                        {
                            ShowNotification("E. South America Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusTwo:
                        {
                            ShowNotification("Mid-Atlantic Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcMinusOne:
                        {
                            ShowNotification("Cape Verde Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcZero:
                        {
                            ShowNotification("GMT Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusOne:
                        {
                            ShowNotification(country, DateTime.Now.ToString("HH:mm", CultureInfo.CurrentCulture));
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusTwo:
                        {
                            ShowNotification("Jordan Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusThree:
                        {
                            ShowNotification("Arabic Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusThreepoinfive:
                        {
                            ShowNotification("Iran Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusFour:
                        {
                            ShowNotification("Mauritius Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusFourpointfive:
                        {
                            ShowNotification("Afghanistan Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusFive:
                        {
                            ShowNotification("Ekaterinburg Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusSix:
                        {
                            ShowNotification("N. Central Asia Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusSeven:
                        {
                            ShowNotification("SE Asia Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusEight:
                        {
                            ShowNotification("China Standard Time ");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusNine:
                        {
                            ShowNotification("Korea Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusTen:
                        {
                            ShowNotification("E. Australia Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusEleven:
                        {
                            ShowNotification("Central Pacific Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusTwelve:
                        {
                            ShowNotification("New Zealand Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusThirteen:
                        {
                            ShowNotification("Tonga Standard Time");
                            return true;
                        }
                    case Countries.UtcOffset.UtcPlusFivepointfive:
                        {
                            ShowNotification("India Standard Time");
                            return true;
                        }
                }
                return true;
            }

            return false;
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
