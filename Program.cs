using System;
using System.Windows.Forms;

namespace it
{
    internal sealed class Program : IDisposable
    {
        private static Form1 _disposable;

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _disposable = new Form1();
            Application.Run(_disposable);
        }
    }
}