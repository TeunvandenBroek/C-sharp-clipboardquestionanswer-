namespace it
{
    public sealed partial class SmartPerformanceCounter : System.IEquatable<SmartPerformanceCounter>
    {
        /// <summary>Record Constructor</summary>
        /// <param name="isValueCreated"><see cref="IsValueCreated"/></param>
        internal SmartPerformanceCounter(bool isValueCreated = default)
        {
            IsValueCreated = isValueCreated;
        }

        public bool Equals(SmartPerformanceCounter other)
        {
            if (other is null)
            {
                throw new System.ArgumentNullException(nameof(other));
            }

            throw new System.NotImplementedException();
        }
    }
}