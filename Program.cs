namespace it
{
    using System;
    using System.CodeDom.Compiler;
    using System.Windows.Forms;
    using Microsoft.CSharp;

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