using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace it.Actions
{
    public class Currency : IAction
    {
        private readonly string[] commands = { "bitcoin", "bitcoin prijs", "bitcoin price", "ethereum", "ethereum prijs", "ethereum price" ,
            "litecoin", "litecoin price", "litecoin prijs", "euro to dollar", "euro naar lira"};

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
        public ActionResult TryExecute(string clipboardText = null)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            ActionResult actionResult = new ActionResult(clipboardText);

            switch (clipboardText)
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
                            actionResult.Description = ("€" + (item.price_eur).ToString("F2", CultureInfo.InvariantCulture));
                        }
                    }
                    return actionResult;               
                case "euro to dollar":
                    {
                        string url = "https://api.exchangeratesapi.io/latest?base=EUR";
                        string json = new WebClient().DownloadString(url);
                        var amount = 1;
                        var currency = JsonConvert.DeserializeObject<dynamic>(json);
                        double curAmount = amount * (double)currency.rates.USD;
                        {
                            actionResult.Title = clipboardText;
                            actionResult.Description = $"{amount:N2} {currency.@base} = {curAmount:N2} Dollar";
                        }
                    }
                    return actionResult;
                case "euro naar lira":
                    {
                        string url = "https://api.exchangeratesapi.io/latest?base=EUR";
                        string json = new WebClient().DownloadString(url);
                        var amount = 1;
                        var currency = JsonConvert.DeserializeObject<dynamic>(json);
                        double curAmount = amount * (double)currency.rates.TRY;
                        {
                            actionResult.Title = clipboardText;
                            actionResult.Description = $"{amount:N2} {currency.@base} = {curAmount:N2} Turkse lira";
                        }
                    }
                    return actionResult;
            }
            
            return actionResult;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as DeviceActions);
        }
    }
}