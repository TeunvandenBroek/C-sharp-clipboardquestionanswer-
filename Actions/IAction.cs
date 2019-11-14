namespace it
{
    public interface IAction
    {
        bool TryExecute(string clipboardText);
    }
}