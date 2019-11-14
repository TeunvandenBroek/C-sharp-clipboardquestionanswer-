using System;
using System.Windows.Forms;

namespace it
{
    internal sealed class Program : IDisposable
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Bootstrap bootstrap = new Bootstrap();
            bootstrap.Startup();

            //_disposable = new Form1();
            Application.Run();
        }

        //private static Form1 _disposable;

        public void Dispose()
        {
            //_disposable?.Dispose();
        }
    }
}
