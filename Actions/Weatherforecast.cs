using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace it.Actions
{
    public class Weatherforecast : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.EndsWith("weather", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            string[] splits = clipboardText.Split(' ');
            var city = splits[1];
            string json = new WebClient().DownloadString($"http://api.openweathermap.org/data/2.5/weather?q={splits[2]}&appid=ac7c75b9937a495021393024d0a90c44&units=metric");
            dynamic deserializedJson = JsonSerializer.Deserialize<dynamic>(json);
            actionResult.Description = ($"{clipboardText} =  {splits[2]}");
            return actionResult;
        }

        public class WeatherLocation
        {
            public Dictionary<string, string> city { get; set; }
        }
    }
}
