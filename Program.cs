namespace it
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom.Compiler;
    using System.Windows.Forms;

    internal sealed class Program 
    {
        private static Bootstrap bootstrap;
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