using System;
using System.Windows.Forms;

namespace it
{
    internal sealed class Program : IDisposable
    {
        //private static Form1 _disposable;

        public void Dispose()
        {
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var bootstrap = new Bootstrap())
            {
                Bootstrap.Startup();
                Application.Run();
            }
        }
    }
}