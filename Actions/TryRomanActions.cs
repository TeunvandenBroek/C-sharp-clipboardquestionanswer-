using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace it.Actions
{

    internal sealed class TryRomanActions : IAction
    {
        public static readonly Dictionary<int, string> NumberRomanDictionary;

        public static readonly Dictionary<char, int> RomanNumberDictionary;

        static TryRomanActions()
        {
            RomanNumberDictionary = new Dictionary<char, int>(7)
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };

            NumberRomanDictionary = new Dictionary<int, string>(12)
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" },

        };
        }

        public static int From(string roman)
        {
            if (string.IsNullOrWhiteSpace(roman))
            {
                throw new ArgumentException("message", nameof(roman));
            }

            int total = 0;

            int current, previous;
            char currentRoman, previousRoman = '\0';

            for (int i = 0; i < roman.Length; i++)
            {
                currentRoman = roman[i];

                previous = previousRoman != '\0' ? RomanNumberDictionary[previousRoman] : '\0';
                current = RomanNumberDictionary[currentRoman];

                if (previous != 0 && current > previous)
                {
                    total = total - (2 * previous) + current;
                }
                else
                {
                    total += current;
                }

                previousRoman = currentRoman;
            }

            return total;
        }

        public static string To(int number)
        {
            StringBuilder roman = new StringBuilder(1000);
            roman.Clear();
            foreach (KeyValuePair<int, string> item in NumberRomanDictionary)
            {
                while (number >= item.Key)
                {
                    _ = roman.Append(item.Value);
                    number -= item.Key;
                }
            }
            return roman.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TryRomanActions);
        }


        public bool Matches(string clipboardText = null)
        {
            return clipboardText.EndsWith(" to roman", StringComparison.Ordinal) || clipboardText.EndsWith(" naar romeins", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText = null)
        {
            ActionResult actionResult = new ActionResult();

            int index = clipboardText.IndexOf("to roman", StringComparison.Ordinal);
            string numberString = clipboardText.Substring(0, index).Trim();
            if (DateTime.TryParse(numberString, out DateTime dateTime))
            {
                (int year, int month, int day) = (dateTime.Year, dateTime.Month, dateTime.Day);
                actionResult.Title = "Date in roman";
                actionResult.Description = $"{day}-{month}-{year} = {To(day)}-{To(month)}-{To(year)}";
                return actionResult;
            }
            else
            {
                int numberToConvert = int.Parse(clipboardText.Substring(0, index).Trim());
                actionResult.Title = "Nummer naar romeins";
                actionResult.Description = $"{numberToConvert} = {To(numberToConvert)}";
            }

            return actionResult;
        }
    }
}