namespace it.Actions
{
    using System.Diagnostics;

    public class TryRedirect : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.ToLower().StartsWith("ga naar");
        }

        public ActionResult TryExecute(string clipboardText)
        {
            switch (clipboardText)
            {
                case "ga naar google":
                    {
                        Process.Start("google.com");
                        return new ActionResult(isProcessed: true);
                    }

            }
            return new ActionResult(isProcessed: false);
        }
    }
}
