using System;


namespace it.Actions
{
    public sealed partial class ActionResult : System.IEquatable<ActionResult>
    {
        internal ActionResult(string title = null, string description = null, bool isProcessed = true)
        {
            Title = title;
            Description = description;
            IsProcessed = isProcessed;
        }
        internal string Description { get; set; }
        internal bool IsProcessed { get; set; }

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
            throw new System.NotImplementedException();
        }
        public abstract bool Matches(string clipboardText = null);
        public abstract ActionResult TryExecute(string clipboardText = null);
    }
}