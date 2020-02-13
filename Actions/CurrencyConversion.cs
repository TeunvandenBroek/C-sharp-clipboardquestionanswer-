namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class CurrencyConversion : IAction
    {
        public bool Matches(string clipboardText)
        {
            //dollar to euro
            return clipboardText.EndsWith("USD to EUR", StringComparison.Ordinal) ||
            clipboardText.EndsWith("dollar to EUR", StringComparison.Ordinal) ||
             clipboardText.EndsWith("dollar to euro", StringComparison.Ordinal) ||
              clipboardText.EndsWith("euro to dollar", StringComparison.Ordinal) ||
            //euro to dollar
            clipboardText.EndsWith("euro to USD", StringComparison.Ordinal);

        }

        internal class ExchangeRateModel
        {
            public Dictionary<string, decimal> rates { get; set; }
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            clipboardText = clipboardText.Replace('.', ',');
            string[] splits = clipboardText.Split(' ');

            string from = splits[1];
            string to = splits[3];

            // conversion
            if (from == "dollar") from = "USD";
            if (to == "dollar") to = "USD";
            // ... more converisons
            if (from == "euro") from = "EUR";
            if (to == "euro") to = "EUR";



            string json = new WebClient().DownloadString($"https://api.exchangeratesapi.io/latest?base={from}&symbols={to}");
            ExchangeRateModel deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRateModel>(json);
            decimal.TryParse(splits[0], out decimal amount);
            var rate = deserializedJson.rates[to];

            actionResult.Description = $"{clipboardText} = {amount * rate:N2} {to}";
            return actionResult;
        }
    }
}
