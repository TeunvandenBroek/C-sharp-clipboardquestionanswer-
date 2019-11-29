namespace it.Actions
{
    using System;
    using System.Net;
    using Newtonsoft.Json;

    public class TryCurrency : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.ToLower().StartsWith("eur to dollar", StringComparison.Ordinal);
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            var actionResult = new ActionResult();

            switch (clipboardText)
            {
                case "dollar to eur":
                {
                    double amount, curAmount;
                    var url = "http://api.openrates.io/latest?base=USD";
                        using (var webClient = new WebClient())
                        {
                            var json = webClient.DownloadString(url);
                            var currency = JsonConvert.DeserializeObject<dynamic>(json);
                            Console.Write("Enter Your Amount:");
                            amount = Convert.ToSingle(Console.ReadLine());
                            curAmount = amount * Convert.ToSingle(currency.rates.EUR);
                            actionResult.Title = "Dollar to euro";
                            actionResult.Description = ("{0:N2} {1} = {2:N2} {3}" + (amount, currency["base"], curAmount, "EUR"));
                            break;
                        }
                    }
                default:
                    actionResult.IsProcessed = false;
                    return actionResult;

            }

            return actionResult;
        }
    }
}
