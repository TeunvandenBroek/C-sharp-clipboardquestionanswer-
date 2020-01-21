using System;
using System.Collections.Generic;

namespace it
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
    public readonly struct Question
    {
        public readonly string Text { get; }
        public readonly string Answer { get; }

        internal Question(string text, string answer)
            => (Text, Answer) = (text, answer);
    }
}