using System;
using System.Text;

namespace it.Actions
{
    public sealed class ActionResult : System.IEquatable<ActionResult>
    {
        internal ActionResult(string title = null, string description = null, bool isProcessed = true)
        {
            this.Title = title;
            this.Description = description;
            this.IsProcessed = isProcessed;
        }
        internal string Description { get; set; }
        internal bool IsProcessed { get; set; }

        internal string Title { get; set; }

        public bool Equals(ActionResult other)
        {
            throw new System.NotImplementedException();
        }
    }

    internal interface IAction
    {
        ActionResult TryExecute(string clipboardText = null);
        bool Matches(string clipboardText = null);
    }

    internal abstract class ActionBase : IAction, System.IEquatable<ActionBase>
    {

        public bool Equals(ActionBase other)
        {
            throw new System.NotImplementedException();
        }
        public abstract bool Matches(string clipboardText = null);
        public abstract ActionResult TryExecute(string clipboardText = null);
    }
}