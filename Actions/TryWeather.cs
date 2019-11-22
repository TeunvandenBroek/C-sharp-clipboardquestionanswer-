using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it.Actions
{
    public class TryWeather : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.ToLower().StartsWith("bmi", StringComparison.Ordinal);
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi", StringComparison.Ordinal))
            {
            }

            return new ActionResult(isProcessed: false);
        }
    }
}