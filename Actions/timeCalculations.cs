using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace it.Actions
{
    internal sealed class timeCalculations : IAction
    {
        private readonly Regex unitRegex =
        new Regex("(?<number>^[0-9]+([.,][0-9]+)?)(\\s*)(?<from>[a-z]+[2-3]?) (in) (?<to>[a-z]+[2-3]?)", RegexOptions.Compiled);

        public bool Matches(string clipboardText = null)
        {
            if (clipboardText is null)
            {
                throw new System.ArgumentNullException(nameof(clipboardText));
            }

            Match matches = unitRegex.Match(clipboardText);
            return matches.Success;
        }

        public ActionResult TryExecute(string clipboardText)
        {
            Match matches = unitRegex.Match(clipboardText);
            ActionResult actionResult = new ActionResult();
            double number = double.Parse(matches.Groups["number"].Value);
            string from = matches.Groups["from"].Value;
            string to = matches.Groups["to"].Value;
            double minuut = 0;
            switch (from)
            {
                case "min":
                case "minuten":
                    {
                        minuut = number;
                        break;
                    }
                case "u":
                case "uur":
                    {
                        minuut = number * 60;
                        break;
                    }
                case "sec":
                case "seconde":
                    {
                        minuut = number / 60;
                        break;
                    }
            }

            // oppervlakte eenheden (area units)
            double result = 0;
            switch (to) // naar (to)
            {
                // lengte eenheden
                case "min":
                case "minuten":
                    {
                        result = minuut;
                        break;
                    }
                case "u":
                case "uur":
                    {
                        result = minuut * 60;
                        break;
                    }
                case "sec":
                case "seconde":
                    {
                        minuut = number / 60;
                        break;
                    }
            }

            Clipboard.SetText(result.ToString(CultureInfo.CurrentCulture));
            actionResult.Title = clipboardText;
            actionResult.Description = result + " " + to;

            return actionResult;
        }

    }
}
