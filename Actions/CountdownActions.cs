using System;
using System.Globalization;
using System.Threading;

namespace it.Actions
{
    internal sealed class CountdownActions : IAction
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as CountdownActions);
        }

        public bool Matches(string clipboardText)
        {
            return clipboardText.StartsWith("timer", StringComparison.Ordinal) && TimeSpan.TryParse(clipboardText.Replace("timer ", string.Empty), out TimeSpan _);
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            _ = TimeSpan.TryParse(clipboardText.Replace("timer ", string.Empty), CultureInfo.InvariantCulture, out TimeSpan ts);
            ActionResult actionResult = new ActionResult();
            Thread.Sleep((int)ts.TotalMilliseconds);
            actionResult.Title = "Countdown timer";
            actionResult.Description = "time is over";
            return actionResult;
        }
    }
}
