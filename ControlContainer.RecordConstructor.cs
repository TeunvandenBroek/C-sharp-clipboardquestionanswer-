namespace it
{
    using System.ComponentModel;

    internal sealed partial class ControlContainer
    {
        /// <summary>Record Constructor</summary>
        /// <param name="components"><see cref="Components"/></param>
        public ControlContainer(ComponentCollection components = default)
        {
            this.Components = components;
        }
    }
}
