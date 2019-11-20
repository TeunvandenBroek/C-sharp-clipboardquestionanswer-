using System.ComponentModel;

namespace it
{
    internal sealed class ControlContainer : IContainer
    {
        private ComponentCollection _components;
        public ControlContainer()
        {
            _components = new ComponentCollection(System.Array.Empty<IComponent>());
        }
        public void Add(IComponent component)
        { }
        public void Add(IComponent component, string name)
        { }
        public void Remove(IComponent component)
        { }
        public ComponentCollection Components
        {
            get { return _components; }
        }
        public void Dispose()
        {
            _components = null;
        }
    }

}

