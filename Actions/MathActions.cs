using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace it.Actions
{
    public class MathActions : IAction
    {
        private readonly Regex mathRegex = new Regex(@"^(?<lhs>\d+(?:[,.]{1}\d)*)(([ ]*(?<operator>[+\-\:x\%\*/])[ ]*(?<rhs>\d+(?:[,.]{1}\d)*)+)+)");

        private readonly Dictionary<string, Func<double, double, double>> binaryOperators = new Dictionary<string, Func<double, double, double>>
            {
               { "+", (a, b) => a + b},
               { "x", (a, b) => a * b },
               { "*", (a, b) => a * b },
               { "-", (a, b) => a - b },
               { "/", (a, b) => b == 0 ? double.NaN : a / b },
               { ":", (a, b) => b == 0 ? double.NaN : a / b },
               { "%", (a, b) => a / b * 100 },
            };

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult(title: clipboardText);

            Match match = mathRegex.Match(clipboardText.Replace(',', '.'));
            if (!match.Success)
            {
                actionResult.IsProcessed = false;
            }
            else
            {

                string[] operators = (from Capture capture
                                      in match.Groups["operator"].Captures
                                      select capture.Value).ToArray();

                double lhs = double.Parse(match.Groups["lhs"].Value, CultureInfo.InvariantCulture);

                double[] rhss = (from Capture capture
                                 in match.Groups["rhs"].Captures
                                 select double.Parse(capture.Value, CultureInfo.InvariantCulture)).ToArray();

                double answer = lhs;

                int i = 0;

                for (int i2 = 0; i2 < rhss.Length; i2++)
                {
                    answer = binaryOperators[operators[i++]](answer, rhss[i2]);
                }

                Clipboard.SetText(answer.ToString(CultureInfo.CurrentCulture));
                actionResult.Description = answer.ToString(CultureInfo.CurrentCulture);
            }

            return actionResult;
        }
    }
}
