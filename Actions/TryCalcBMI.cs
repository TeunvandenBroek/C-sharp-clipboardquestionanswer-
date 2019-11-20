//TO DO
namespace it.Actions
{
    public class TryCalcBmi : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.ToLower().StartsWith("bmi");
        }

        ActionResult IAction.TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi", System.StringComparison.Ordinal))
            {

            }
            return new ActionResult(isProcessed: false);
        }
    }
}
