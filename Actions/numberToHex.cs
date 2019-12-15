using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace it.Actions
{
    public sealed class numberToHex : IAction
    {
        private readonly Regex hex = new Regex("(?<number>^[0-9]+([.,][0-9]{1,3})?)(\\s*)(?= to hex)");
        public bool Matches(string clipboardText = null)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }
            return clipboardText.EndsWith(" to hex", StringComparison.Ordinal);
        }
        public ActionResult TryExecute(string clipboardText = null)
        {
            ActionResult actionResult = new ActionResult();
            Match match = hex.Match(clipboardText);
            if (match.Success)
            {
                int number = int.Parse(match.Groups["number"].Value, CultureInfo.InvariantCulture);
                string hex = number.ToString("X");
                actionResult.Title = "Calculate hex";
                actionResult.Description = $"{clipboardText}, {hex}";
            }

            return actionResult;

        }
    }
}
