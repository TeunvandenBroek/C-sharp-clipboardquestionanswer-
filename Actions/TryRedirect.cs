using System.Diagnostics;

namespace it.Actions
{
    public class TryRedirect : IAction
    {
        public ActionResult TryExecute(string clipboardText)
        {
            switch (clipboardText)
            {
                case "ga naar google":
                {
                    Process.Start("google.com");
                    return new ActionResult();
                }
            }

            return new ActionResult(isProcessed: false);
        }
    }
}