namespace it
{
    using System.Collections.Generic;

    public static class Questions
    {
        private static Dictionary<string, string> questionDict = new Dictionary<string, string>
        {
            // store here your custom questions / answers
            ["question"] = "answer",

        };

        public static Dictionary<string, string> GetQuestionDict()
        {
            return questionDict;
        }

        public static void SetQuestionDict(Dictionary<string, string> value)
        {
            questionDict = value;
        }

        internal static List<Question> LoadQuestions()
        {
            List<Question> result = new List<Question>();
            foreach (KeyValuePair<string, string> item in GetQuestionDict())
            {
                result.Add(new Question(item.Key, item.Value));
            }

            return result;
        }
    }
}
