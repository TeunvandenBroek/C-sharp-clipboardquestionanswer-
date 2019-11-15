//TO DO
namespace it.Actions
{
    public class TryCalcBmi : IAction
    {
        ActionResult IAction.TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi"))
            {

            }
            return new ActionResult(isProcessed: false);
        }
    }
}
