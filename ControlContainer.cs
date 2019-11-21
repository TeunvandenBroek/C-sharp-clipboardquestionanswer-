using System;
using System.ComponentModel;

namespace it
{
    internal sealed class ControlContainer : IContainer
    {
        public ControlContainer()
        {
            Components = new ComponentCollection(Array.Empty<IComponent>());
        }

        public void Add(IComponent component)
        {
        }

        public void Add(IComponent component, string name)
        {
        }

        public void Remove(IComponent component)
        {
        }

        public ComponentCollection Components { get; private set; }

        public void Dispose()
        {
            Components = null;
        }
    }
}