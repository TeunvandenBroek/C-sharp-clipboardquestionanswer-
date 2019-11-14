//TO DO
namespace it.Actions
{
    public class TryCalcBmi : IAction
    {
        QuestionAnswer IAction.TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi"))
            {

            }
            return new QuestionAnswer();
        }
    }
}
