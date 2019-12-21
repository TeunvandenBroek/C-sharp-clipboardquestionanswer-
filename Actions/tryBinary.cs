using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace it.Actions
{
    internal sealed class tryBinary : IAction
    {
        private readonly Regex binary = new Regex("(?<number>^[0-9]+([.,][0-9]+)?)(\\s*)(?= to binary)");

        public bool Matches(string clipboardText)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }
            return clipboardText.EndsWith(" to binary", StringComparison.Ordinal);
        }
        public ActionResult TryExecute(string clipboardText = null)
        {
            ActionResult actionResult = new ActionResult();
            Match match = binary.Match(clipboardText);
            if (match.Success)
            {
                int number = int.Parse(match.Groups["number"].Value, CultureInfo.InvariantCulture);
                string binary = Convert.ToString(number, 2);
                actionResult.Title = "Calculate binary";
                actionResult.Description = $"{clipboardText}, {binary}";
            }
            return actionResult;

        }
    }
}