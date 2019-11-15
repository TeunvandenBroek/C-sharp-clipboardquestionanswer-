namespace it.Actions
{
    public class QuestionAnswer
    {
        internal string Question { get; set; }
        internal string Answer { get; set; }
        internal bool IsProcessed { get; set; }
        internal string ClipboardText { get; set; }

        public QuestionAnswer(string quesiton = null, string answer = null, string clipboardText = null, bool isProcessed = false)
        {
            Question = quesiton;
            Answer = answer;
            ClipboardText = clipboardText;
            IsProcessed = isProcessed;
        }
    }

    internal interface IAction
    {
        public QuestionAnswer TryExecute(string clipboardText);
    }

    internal abstract class ActionBase : IAction
    {
        public abstract QuestionAnswer TryExecute(string clipboardText);
    }
}
