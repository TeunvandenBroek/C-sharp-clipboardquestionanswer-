using System;


namespace it.Actions
{
    public sealed partial class ActionResult : IEquatable<ActionResult>
    {
        internal ActionResult(string title = null, string description = null)
        {
            Title = title;
            Description = description;
        }
        internal string Description { get; set; }

        internal string Title { get; set; }

        public bool Equals(ActionResult other)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IAction
    {
        ActionResult TryExecute(string clipboardText = null);
        bool Matches(string clipboardText = null);
    }

    internal abstract class ActionBase : IAction, IEquatable<ActionBase>
    {

        public bool Equals(ActionBase other)
        {
            throw new NotImplementedException();
        }
        public abstract bool Matches(string clipboardText = null);
        public abstract ActionResult TryExecute(string clipboardText = null);
    }
}