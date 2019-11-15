namespace it.Actions
{
    using System.Diagnostics;

    public class TryRedirect : IAction
    {
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
