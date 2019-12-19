using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace it.Actions
{
    public class Benchmark
    {
        public void RunTheMethod(Action action)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            // Do something.
            for (int i = 0; i < 10000000; i++)
            {
                action();
            }

            // Stop timing.
            stopwatch.Stop();
            // Write result.
            Console.WriteLine("Total time: {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Avg time: {0}", stopwatch.Elapsed.TotalMilliseconds / 10000000.0);
        }
    }
}