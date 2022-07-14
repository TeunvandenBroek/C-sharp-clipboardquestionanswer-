using System;

namespace it.Actions
{
    public class Wallpaper : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.EndsWith("wallpaper", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            {
                actionResult.Title = "Wallpaper";
                actionResult.Description = "Nieuwe wallpaper ingesteld.";
            }
            return actionResult;
        }
    }
}
