namespace it
{
    public interface IAction
    {
        /// <summary>
        /// Returns true if some action was made, and false otherwise
        /// </summary>
        bool TryExecute(string clipboardText);
    }
}
