namespace it
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    internal static class Program
    {
        private static Bootstrap bootstrap;

        [STAThread]
        private static void Main()
        {
            bootstrap = new Bootstrap();
            Application.Run();
        }
    }
}
