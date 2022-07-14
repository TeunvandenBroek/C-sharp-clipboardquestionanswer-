using System;
using System.Collections.Generic;
using System.Linq;

namespace it.Actions
{
    public sealed class BmiActions : IAction
    {
        private readonly Dictionary<(double From, double To), string> BmiToDictionary = new Dictionary<(double From, double To), string>(8)
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

        public override bool Equals(object obj)
        {
            return Equals(obj as BmiActions);
        }

        public bool Matches(string clipboardText)
        {
            return clipboardText.EndsWith(" to bmi", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            int ageIndex = clipboardText.IndexOf("age", StringComparison.Ordinal);
            int age = (int)decimal.Parse(clipboardText.Substring(0, ageIndex).Trim());
            int weigthIndex = clipboardText.IndexOf("weight", StringComparison.Ordinal);
            int weight = (int)decimal.Parse(clipboardText.Substring(ageIndex + 3, weigthIndex - (ageIndex + 3)).Trim());
            int heightIndex = clipboardText.IndexOf("height", StringComparison.Ordinal);
            double height = (int)decimal.Parse(clipboardText.Substring(weigthIndex + 6, heightIndex - (weigthIndex + 6)).Trim());
            double bmi = CalculateBMI(weight, height);
            bmi = Math.Round(bmi, 2);
            string bmiDescription = BmiToDictionary.First(kvp => kvp.Key.From <= bmi && bmi < kvp.Key.To).Value;
            actionResult.Title = "Calculate bmi";
            actionResult.Description = $"{bmi}, {bmiDescription}";
            return actionResult;
        }

        private static double CalculateBMI(int weight, double height)
        {
            return weight / (Math.Pow(height / 100.0, 2));
        }
    }
}
