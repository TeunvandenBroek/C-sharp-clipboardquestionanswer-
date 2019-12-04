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
                lock (@lock)
                {
                    if (!IsValueCreated)
                    {
                        value?.Dispose();
                        value = factory();
                        IsValueCreated = true;
                    }
                }

                cpuCounterLastAccessedTimestamp = Stopwatch.GetTimestamp();
                return value;
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            value?.Dispose();
        }

        public async Task FunctionAsync()
        {
            await Task.Delay(time).ConfigureAwait(false);
            DoCleaningCheck();
        }

        private void DoCleaningCheck()
        {
            if (Stopwatch.GetTimestamp() - cpuCounterLastAccessedTimestamp <= time.Ticks)
            {
                return;
            }

            lock (@lock)
            {
                IsValueCreated = false;
                value.Close();
                value.Dispose();
                value = null;
            }
        }
    }
}