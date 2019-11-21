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


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var bootstrap = new Bootstrap();
            bootstrap.EnsureWindowStartup(false);
            bootstrap.Startup();


            //_disposable = new Form1();
            Application.Run();
        }
    }
}