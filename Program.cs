using System;
using System.Windows.Forms;

namespace it
{
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
