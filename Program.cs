namespace it
{
    using System;
    using System.Windows.Forms;

    internal sealed class Program : IDisposable
    {

        private static Bootstrap bootstrap;
        public void Dispose()
        {
            bootstrap?.Dispose();
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
            Bootstrap.Startup();
            Application.Run();
        }
    }
}