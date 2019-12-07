namespace it.Actions
{
    using System;
    using System.Globalization;
    using System.Threading;

    internal sealed class CountdownActions : IAction, IEquatable<CountdownActions>
    {
        public bool Matches(string clipboardText)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }

            return clipboardText.StartsWith("timer", StringComparison.Ordinal) && TimeSpan.TryParse(clipboardText.Replace("timer ", string.Empty), out TimeSpan _);
        }


        ActionResult IAction.TryExecute(string clipboardText)
        {
            TimeSpan.TryParse(clipboardText.Replace("timer ", string.Empty), CultureInfo.InvariantCulture, out TimeSpan ts);
            ActionResult actionResult = new ActionResult();
            Thread.Sleep((int)ts.TotalMilliseconds);
            actionResult.Title = "Countdown timer";
            actionResult.Description = "time is over";
            return actionResult;
        }

        public bool Equals(CountdownActions other)
        {
            throw new NotImplementedException();
        }
    }
}