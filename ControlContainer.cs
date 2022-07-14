using System.ComponentModel;

namespace it
{
    internal sealed partial class ControlContainer : IContainer, System.IEquatable<ControlContainer>
    {
        public ControlContainer(ControlContainer obj)
        {
            Components = obj.Components;
        }

        public ComponentCollection Components { get; private set; }

        public void Add(IComponent component)
        {
        }

        public void Add(IComponent component, string name)
        {
            if (component is null)
            {
                throw new System.ArgumentNullException(nameof(component));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }
        }

        public void Dispose()
        {
            Components = null;
        }

        public bool Equals(ControlContainer other)
        {
            throw new System.NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(IComponent component)
        {
            if (component is null)
            {
                throw new System.ArgumentNullException(nameof(component));
            }
        }
    }
}
