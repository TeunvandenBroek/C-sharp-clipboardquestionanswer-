namespace it.Actions
{
    public class ActionResult
    {
        internal string Title { get; set; }
        internal string Description { get; set; }
        internal bool IsProcessed { get; set; }

        public ActionResult(string title = null, string description = null, bool isProcessed = true)
        {
            Title = title;
            Description = description;
            IsProcessed = isProcessed;
        }
    }

    internal interface IAction
    {
        ActionResult TryExecute(string clipboardText);
        bool Matches(string clipboardText);
    }

    internal abstract class ActionBase : IAction
    {
        public abstract ActionResult TryExecute(string clipboardText);
        public abstract bool Matches(string clipboardText);
    }
}
