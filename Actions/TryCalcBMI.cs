//TO DO

using System;

namespace it.Actions
{
    public class TryCalcBmi : IAction
    {
        ActionResult IAction.TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi", StringComparison.Ordinal))
            {
            }

            return new ActionResult(isProcessed: false);
        }
    }
}