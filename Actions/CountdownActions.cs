namespace it.Actions
{
    using System;
    using System.Threading;

    internal sealed class CountdownActions : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.StartsWith("timer", StringComparison.Ordinal) && TimeSpan.TryParse(clipboardText.Replace("timer ", string.Empty), out TimeSpan ts);
        }


        ActionResult IAction.TryExecute(string clipboardText)
        {
            TimeSpan.TryParse(clipboardText.Replace("timer ", string.Empty), out var ts);
            var actionResult = new ActionResult();
            Thread.Sleep((int)ts.TotalMilliseconds);
            actionResult.Title = "Countdown timer";
            actionResult.Description = "time is over";
            return actionResult;
        }
    }
}