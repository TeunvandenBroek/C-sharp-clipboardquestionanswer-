namespace it
{
    using System;
    using System.ComponentModel;

    internal sealed partial class ControlContainer : IContainer
    {
        public ControlContainer()
        {
            this.Components = new ComponentCollection(Array.Empty<IComponent>());
        }

        public ComponentCollection Components { get; private set; }

        public void Add(IComponent component)
        {
        }

        public void Add(IComponent component, string name)
        {
        }

        public void Dispose()
        {
            this.Components = null;
        }

        public void Remove(IComponent component)
        {
        }
    }
}