using System;
using System.Collections.Generic;
using System.Linq;

namespace it
{
    internal static class Questions
    {
        private static Dictionary<string, string> questionDict = new Dictionary<string, string>(3, StringComparer.Ordinal)
        {
            //3 Question examples:

            ["Test Question 1, Do you have a name?"] = "yes",

            ["Test Question 2, Do you like this program?"] = "Very much!",

            ["Test Question 3, Enjoy the program! "] = "Thanks for using the program!"
        };


        internal static List<Question> LoadQuestions()
        {
            return questionDict.Select(item => new Question(item.Key, item.Value)).ToList();
        }
    }
}