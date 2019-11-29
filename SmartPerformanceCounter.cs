using System;

namespace it
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public sealed partial class SmartPerformanceCounter : IDisposable
    {

        private readonly object @lock = new object();

        private long cpuCounterLastAccessedTimestamp;
        private bool disposed;
        private readonly Func<PerformanceCounter> factory;

        private readonly TimeSpan time;

        private PerformanceCounter value;

        public SmartPerformanceCounter(Func<PerformanceCounter> factory, TimeSpan time)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.time = time;
        }

        public bool IsValueCreated { get; private set; }

        public PerformanceCounter Value
        {
            get
            {
                lock (this.@lock)
                {
                    if (!this.IsValueCreated)
                    {
                        this.value?.Dispose();
                        this.value = this.factory();
                        this.IsValueCreated = true;
                    }
                }

                this.cpuCounterLastAccessedTimestamp = Stopwatch.GetTimestamp();
                return this.value;
            }
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            this.value?.Dispose();
        }

        public async Task FunctionAsync()
        {
            await Task.Delay(this.time).ConfigureAwait(false);
            this.DoCleaningCheck();
        }

        private void DoCleaningCheck()
        {
            var now = Stopwatch.GetTimestamp();
            if (now - this.cpuCounterLastAccessedTimestamp <= this.time.Ticks)
            {
                return;
            }

            lock (this.@lock)
            {
                this.IsValueCreated = false;
                this.value.Close();
                this.value.Dispose();
                this.value = null;
            }
        }

        private void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
    }
}