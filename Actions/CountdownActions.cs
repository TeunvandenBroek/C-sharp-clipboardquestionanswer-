using System;
using System.Threading;

namespace it.Actions
{
    internal class CountdownActions : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.StartsWith("timer") && TimeSpan.TryParse(clipboardText.Replace("timer ", ""), out TimeSpan ts);
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();

            if (clipboardText.StartsWith("timer") && TimeSpan.TryParse(clipboardText.Replace("timer ", ""), out TimeSpan ts))
            {
                Thread.Sleep((int)ts.TotalMilliseconds);
                // using async here may cause a dead lock. Since we are not using a UI thread, we can just sleep a thread.
                //async Task p()
                //{
                //    await Task.Delay(ts).ConfigureAwait(false);
                //    ShowNotification("Countdown timer", "time is over");
                //}
                //Task.Run(p);
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
