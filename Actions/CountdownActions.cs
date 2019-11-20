using System;
using System.Threading;

namespace it.Actions
{
    internal class CountdownActions : IAction
    {
        ActionResult IAction.TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();

            if (clipboardText.StartsWith("timer", StringComparison.Ordinal) && TimeSpan.TryParse(clipboardText.Replace("timer ", ""), out TimeSpan ts))
            {
                Thread.Sleep((int)ts.TotalMilliseconds);
                actionResult.Title = "Countdown timer";
                actionResult.Description = "time is over";
            }
            else
            {
                actionResult.IsProcessed = false;
            }
            return actionResult;
        }
    }
}
