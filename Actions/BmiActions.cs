using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace it.Actions
{
    public sealed class BmiActions : IAction
    {
        private readonly Regex bmi = new Regex("^(?<age>[0-9]+)years (?<weight>[0-9]+)kg (?<height>[0-9]+)cm(?= to bmi)");
        private IReadOnlyDictionary<(double From, double To), string> BmiToDictionary = new Dictionary<(double From, double To), string>
        {
            {(0, 15),"Very severely underweight"},
            {(15, 16),"Severely underweight"},
            {(16, 18.5),"Underweight"},
            {(18.5, 25),"Normal"},
            {(25, 30),"Overweight"},
            {(30, 35),"Moderately Obese"},
            {(35, 40),"Severly Obese"},
            {(40, 99999),"Very Severly Obese"},
        };

        public bool Matches(string clipboardText = null)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }
            return clipboardText.EndsWith(" to bmi", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText = null)
        {
            ActionResult actionResult = new ActionResult();
            Match match = bmi.Match(clipboardText);
            if (match.Success)
            {
                int age = int.Parse(match.Groups["age"].Value, CultureInfo.InvariantCulture);
                int weight = int.Parse(match.Groups["weight"].Value, CultureInfo.InvariantCulture);
                int height = int.Parse(match.Groups["height"].Value, CultureInfo.InvariantCulture);
                var bmi = weight / Math.Pow(height / 100, 2);
                var bmiDescription = BmiToDictionary.First(kvp => kvp.Key.From <= bmi && bmi < kvp.Key.To).Value;
                actionResult.Title = "Calculate bmi";
                actionResult.Description = $"{age} = {(weight)} {(height)}";
            }

            return actionResult;

        }
    }
}