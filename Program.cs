namespace it
{
    using System;
    using System.Windows.Forms;

    internal sealed class Program : IDisposable
    {

        private static Bootstrap bootstrap;
        private bool disposed;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;

            if (disposing)
            {

            }
            if (bootstrap != null)
            {
                bootstrap?.Dispose();
                bootstrap = null;
            }
        }
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bootstrap = new Bootstrap();
            Application.Run();
        }
    }
}