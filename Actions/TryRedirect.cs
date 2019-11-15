namespace it.Actions
{
    using System.Diagnostics;

    public class TryRedirect : IAction
    {
        public QuestionAnswer TryExecute(string clipboardText)
        {
            switch (clipboardText)
            {
                case "ga naar google":
                    {
                        Process.Start("google.com");
                        return new QuestionAnswer();
                    }

            }
            return new QuestionAnswer(isProcessed: false);
        }
    }
}
