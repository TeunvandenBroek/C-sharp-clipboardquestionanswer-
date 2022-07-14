using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace it.Actions
{
    internal interface IAction
    {
        bool Matches(string clipboardText = null);

        ActionResult TryExecute(string clipboardText = null);
    }

    public sealed partial class ActionResult : IEquatable<ActionResult>, INotifyPropertyChanged
    {
        internal ActionResult(string title = null, string description = null)
        {
            Title = title;
            Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal string Description { get; set; }

        internal string Title { get; set; }

        public bool Equals(ActionResult other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ActionResult);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
