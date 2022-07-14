using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace it.Actions
{
    public sealed class autoClicker : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.StartsWith("auto clicker", StringComparison.Ordinal) && TimeSpan.TryParse(clipboardText.Replace("auto clicker", string.Empty), CultureInfo.InvariantCulture, out TimeSpan _);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            _ = TimeSpan.TryParse(clipboardText.Replace("auto clicker", string.Empty), CultureInfo.InvariantCulture, out TimeSpan ts);
            ActionResult actionResult = new ActionResult();
            Thread.Sleep((int)ts.TotalMilliseconds);
            actionResult.Title = "autoclicker";
            actionResult.Description = "autoclicker gestart";
            return actionResult;
        }
    }
}
