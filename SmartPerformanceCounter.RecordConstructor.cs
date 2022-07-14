using System;

namespace it
{
    public sealed partial class SmartPerformanceCounter : IEquatable<SmartPerformanceCounter>
    {
        /// <summary>
        /// Record Constructor
        /// </summary>
        /// <param name="isValueCreated">
        /// <see cref="IsValueCreated" />
        /// </param>
        internal SmartPerformanceCounter(bool isValueCreated = default)
        {
            IsValueCreated = isValueCreated;
        }

        public bool Equals(SmartPerformanceCounter other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SmartPerformanceCounter);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
