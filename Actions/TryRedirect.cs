namespace it
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class TryRedirect : IAction
    {
        private readonly Form1 form1;

        public TryRedirect(Form1 form1)
        {
            this.form1 = form1;
        }


        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }
        public bool TryExecute(string clipboardText)
        {
            return TryCopyRedirect(clipboardText);
        }

        public bool TryCopyRedirect(string clipboardText)
        {
            switch (clipboardText)
            {
                case "ga naar google":
                    {
                        Process.Start("google.com");
                        return true;
                    }

            }
            return false;
        }
    }
}
