namespace it.Actions
{
    public class QuestionAnswer
    {
        internal string Question { get; }
        internal string Answer { get; }
        internal bool IsSuccessful { get; }

        public QuestionAnswer(string quesiton = null, string answer = null, bool isSuccessful = true)
        {
            Question = quesiton;
            Answer = answer;
            IsSuccessful = isSuccessful;
        }
    }

    internal interface IAction
    {
        internal QuestionAnswer TryExecute(string clipboardText);
    }
}
