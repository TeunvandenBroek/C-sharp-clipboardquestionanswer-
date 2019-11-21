//TO DO

using System;

namespace it.Actions
{
    public class TryCalcBmi : IAction
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