using System;
using System.Windows.Forms;

namespace it
{
    sealed class Program : IDisposable
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _disposable = new Form1();
            Application.Run(_disposable);
        }

        private static Form1 _disposable;

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
