using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TO DO
namespace it
{
    public class TryCalcBmi : IAction
    {
        private readonly Form1 form1;

        public TryCalcBmi(Form1 form1)
        {
            this.form1 = form1;
        }
        private void ShowNotification(string question, string answer)
        {
            form1.ShowNotification(question, answer);
        }
        public bool TryExecute(string clipboardText)
        {
            if (clipboardText.StartsWith("bmi"))
            {

            }
            return true;
        }
    }
}
 
 
