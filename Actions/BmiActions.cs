using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace it.Actions
{
    public sealed class BmiActions : IAction 
    {
        private readonly Regex bmi = new Regex("^(?<age>[0-9]+)(years|jaar) (?<weight>[0-9]+)kg (?<height>[0-9]+)cm(?= to bmi)");

        private readonly IReadOnlyDictionary<(decimal From, decimal To), string> BmiToDictionary = new Dictionary<(decimal From, decimal To), string>(8)
        {
            {(0, 15),"Very severely underweight"},
            {(15, 16),"Severely underweight"},
            {(16, 18.5m),"Underweight"},
            {(18.5m, 25),"Normal"},
            {(25, 30),"Overweight"},
            {(30, 35),"Moderately Obese"},
            {(35, 40),"Severly Obese"},
            {(40, 99999),"Very Severly Obese"},
        };

        public bool Matches(string clipboardText)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }
            return clipboardText.EndsWith(" to bmi", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            Match match = bmi.Match(clipboardText);
            if (match.Success)
            {
                decimal age = decimal.Parse(match.Groups["age"].Value, CultureInfo.InvariantCulture);
                decimal weight = decimal.Parse(match.Groups["weight"].Value, CultureInfo.InvariantCulture);
                double height = double.Parse(match.Groups["height"].Value, CultureInfo.InvariantCulture);
                decimal bmi = (decimal)Math.Pow(height / 100, 2);
                bmi = Decimal.Round(Decimal.Divide(weight, bmi), 2);
                string bmiDescription = BmiToDictionary.First(kvp => kvp.Key.From <= bmi && bmi < kvp.Key.To).Value;
                actionResult.Title = "Calculate bmi";
                actionResult.Description = $"{bmi}, {bmiDescription}";
            }

            return actionResult;

        }
    }
}