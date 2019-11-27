using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace it.Actions
{
    public class TryHex : IAction
    {
        //covert number to hex
        ActionResult IAction.TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();
            // Store integer 182
            int decValue = 182;

            // Convert integer 182 as a hex in a string variable
            string hexValue = decValue.ToString("X");

            // Convert the hex string back to the number
            int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

            return actionResult;

        }
    }
}
