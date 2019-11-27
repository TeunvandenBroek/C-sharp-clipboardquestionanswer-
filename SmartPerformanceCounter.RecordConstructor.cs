namespace it
{
    public partial class SmartPerformanceCounter
    {
        /// <summary>Record Constructor</summary>
        /// <param name="isValueCreated"><see cref="IsValueCreated"/></param>
        public SmartPerformanceCounter(bool isValueCreated = default)
        {
            this.IsValueCreated = isValueCreated;
        }
    }
}
