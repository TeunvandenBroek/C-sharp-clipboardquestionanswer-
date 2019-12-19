using System;
using System.Collections.Generic;

namespace it
{
    public readonly struct Question
    {
        public readonly string Text { get; }
        public readonly string Answer { get; }

        internal Question(string text, string answer)
            => (Text, Answer) = (text, answer);
    }
}