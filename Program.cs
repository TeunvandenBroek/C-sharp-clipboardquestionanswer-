using System;
using System.Windows.Forms;

namespace it
{
    internal sealed class Program : IDisposable
    {
        /// <summary>
        /// The main entry point for the appliation.
        /// </summary>
        [STAThread]
        private static void Main()
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
