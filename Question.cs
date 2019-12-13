namespace it
{
    public sealed class Question
    {
        public Question(string text, string answer)
        {
            Text = text;
            Answer = answer;
        }

        public string Text { get; private set; }
        public string Answer { get; private set; }
    }
}