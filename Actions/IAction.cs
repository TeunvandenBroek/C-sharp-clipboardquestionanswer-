namespace it.Actions
{
    public sealed class ActionResult
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
    }

    internal interface IAction
    {
        ActionResult TryExecute(string clipboardText);
        bool Matches(string clipboardText);
    }

    internal abstract class ActionBase : IAction
    {
        public abstract bool Matches(string clipboardText);
        public abstract ActionResult TryExecute(string clipboardText);
    }
}