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
            //dollar to euro - euro to dollar
            return clipboardText.EndsWith("dollar to euro", StringComparison.Ordinal) ||
             clipboardText.EndsWith("dollar naar euro", StringComparison.Ordinal) ||
              clipboardText.EndsWith("euro to dollar", StringComparison.Ordinal) ||
              clipboardText.EndsWith("euro naar dollar", StringComparison.Ordinal) ||
              //lira to euro - euro to lira
              clipboardText.EndsWith("lira to euro", StringComparison.Ordinal) ||
             clipboardText.EndsWith("lira naar euro", StringComparison.Ordinal) ||
              clipboardText.EndsWith("euro to lira", StringComparison.Ordinal) ||
              clipboardText.EndsWith("euro naar lira", StringComparison.Ordinal) ||
            //engelse pond to euro - pond to euro
             clipboardText.EndsWith("pond to euro", StringComparison.Ordinal) ||
             clipboardText.EndsWith("pond naar euro", StringComparison.Ordinal) ||
             clipboardText.EndsWith("euro to pond", StringComparison.Ordinal) ||
             clipboardText.EndsWith("euro naar pond", StringComparison.Ordinal);
        }

        internal class ExchangeRateModel
        {
            public Dictionary<string, decimal> rates { get; set; }
        }
        private readonly Dictionary<string, string> Currency = new Dictionary<string, string>()
        {
                    //dollar 
                    {"dollar" , "USD" },
                     {"USD" , "USD" },
                    //euro
                    {"euro", "EUR" },
                    {"EUR", "EUR" },
                    //turkse lira
                    {"lira" , "TRY" },
                    {"TRY" , "TRY" },
                    //engelse pond
                    {"pond" , "GBP" },
                    {"GBP" , "GBP" },

        };
        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            clipboardText = clipboardText.Replace('.', ',');
            string[] splits = clipboardText.Split(' ');
            string from = splits[1];
            string to = splits[3];

            if (Currency.TryGetValue(from, out var fromCurrency) && Currency.TryGetValue(to, out var toCurrency))
            {
                var url = $"https://api.exchangeratesapi.io/latest?base={fromCurrency}&symbols={toCurrency}";
                string json = new WebClient().DownloadString(url);
                ExchangeRateModel deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRateModel>(json);
                if(decimal.TryParse(splits[0], out decimal amount));
                {
                    var rate = deserializedJson.rates[toCurrency];
                    actionResult.Description = $"{clipboardText} = {amount * rate:N2} {toCurrency}";
                }
            }
            return actionResult;
        }
    }
}
