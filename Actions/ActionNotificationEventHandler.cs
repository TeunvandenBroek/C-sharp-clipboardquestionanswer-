using System;

namespace it.Actions
{
    internal class ActionNotificationEventHandler : EventArgs
    {
        QuestionAnswer QuestionAnswer { get; set; }
    }
}
