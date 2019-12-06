using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace it.Actions
{
    internal sealed class TryRomanActions : IAction
    {
        public static readonly Dictionary<int, string> NumberRomanDictionary;

        public static readonly Dictionary<char, int> RomanNumberDictionary;

        private readonly Regex roman = new Regex("^([1-3]?[0-9]{3}|[1-9][0-9]{0,2})(?= to roman)");

        static TryRomanActions()
        {
            RomanNumberDictionary = new Dictionary<char, int>
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };

            NumberRomanDictionary = new Dictionary<int, string>
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
            StringBuilder roman = new StringBuilder();

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
        public bool Matches(string clipboardText = null)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }

            return clipboardText.EndsWith(" to roman");
        }

        public ActionResult TryExecute(string clipboardText = null)
        {
            ActionResult actionResult = new ActionResult();

            Match match = roman.Match(clipboardText);
            if (match.Success)
            {
                int number = int.Parse(match.Groups[0].Value);

                actionResult.Title = "Nummer naar romeins";
                actionResult.Description = $"{number} = {To(number)}";
            }

            return actionResult;
        }
    }
}
