using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace it.Actions
{
    public class Bitcoin : IAction
    {
        private readonly string[] commands = { "bitcoin", "bitcoin prijs", "bitcoin price", "ethereum", "ethereum prijs", "ethereum price" ,
            "litecoin", "litecoin price", "litecoin prijs"};

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
            return clipboardText.StartsWith("dollar to euro", StringComparison.Ordinal);
        }
        public class Item{
            //Coinmarketcap
            public string id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string rank { get; set; }
            public decimal price_eur { get; set; }
            [JsonProperty(PropertyName = "24h_volume_usd")]   
            public string volume_usd_24h { get; set; }
            public string market_cap_usd { get; set; }
            public string available_supply { get; set; }
            public string total_supply { get; set; }
            public string percent_change_1h { get; set; }
            public string percent_change_24h { get; set; }
            public string percent_change_7d { get; set; }
            public string last_updated { get; set; }
        }
        ActionResult IAction.TryExecute(string clipboardText)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            ActionResult actionResult = new ActionResult(clipboardText);

            switch (clipboardText.ToLower(CultureInfo.InvariantCulture))
            {
                case "bitcoin":
                case "bitcoin price":
                case "bitcoin prijs":
                    {
                        string json = new WebClient().DownloadString("https://api.coinmarketcap.com/v1/ticker/bitcoin/?convert=EUR");
                        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                        for (int i = 0; i < items.Count; i++)
                        {
                            Item item = items[i];
                            actionResult.Title = clipboardText;
                            actionResult.Description = ("€" + (item.price_eur).ToString("F2", CultureInfo.InvariantCulture));
                        }
                    }
                    return actionResult;
                case "ethereum":
                case "ethereum price":
                case "ethereum prijs":
                    {
                        string json = new WebClient().DownloadString("https://api.coinmarketcap.com/v1/ticker/ethereum/?convert=EUR");
                        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                        for (int i = 0; i < items.Count; i++)
                        {
                            Item item = items[i];
                            actionResult.Title = clipboardText;
                            actionResult.Description = ("€" + (item.price_eur).ToString("F2", CultureInfo.InvariantCulture));
                        }
                    }
                    return actionResult;
                case "litecoin":
                case "litecoin price":
                case "litecoin prijs":
                    {
                        string json = new WebClient().DownloadString("https://api.coinmarketcap.com/v1/ticker/litecoin/?convert=EUR");
                        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                        for (int i = 0; i < items.Count; i++)
                        {
                            Item item = items[i];
                            actionResult.Title = clipboardText;
                            actionResult.Description = ("€"  + (item.price_eur).ToString("F2", CultureInfo.InvariantCulture));
                        }
                    }
                    return actionResult;
            }
            return actionResult;
        }

        public static void EUR_TO_usd(string clipboardText)
        {
            ActionResult actionResult = new ActionResult(clipboardText);
            if (clipboardText.EndsWith(" dollar to euro"))
            {
                var parts = clipboardText.Split(' ');
                if (parts.Length == 4)
                {
                    if (double.TryParse(parts[0], out double amount))
                    {
                        if (parts[1] == "dollar" && parts[2] == "to" && parts[3] == "euro")
                        {
                            string url = "http://api.openrates.io/latest?base=USD";
                            string json = new WebClient().DownloadString(url);
                            var currency = JsonConvert.DeserializeObject<dynamic>(json);
                            double curAmount = amount * Convert.ToSingle(currency.rates.EUR);
                            actionResult.Description = string.Format("{0:N2} {1} = {2:N2} {3}", amount, currency["base"], curAmount, "EUR");
                        }
                    }
                }
            }
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as DeviceActions);
        }
    }
}