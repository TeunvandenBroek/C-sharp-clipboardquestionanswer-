namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class MathActions : IAction
    {
        private readonly Dictionary<string, Func<double, double, double>> binaryOperators =
            new Dictionary<string, Func<double, double, double>>(
                StringComparer.Ordinal)
                {
                    { "+", (a, b) => a + b },
                    { "x", (a, b) => a * b },
                    { "*", (a, b) => a * b },
                    { "-", (a, b) => a - b },
                    { "/", (a, b) => b == 0 ? double.NaN : a / b },
                    { ":", (a, b) => b == 0 ? double.NaN : a / b },
                    { "%", (a, b) => a / b * 100 },
                };

        private readonly Regex mathRegex =
            new Regex(@"^(?<lhs>\d+(?:[,.]{1}\d)*)(([ ]*(?<operator>[+\-\:x\%\*/])[ ]*(?<rhs>\d+(?:[,.]{1}\d)*)+)+)");

        public bool Matches(string clipboardText)
        {
            var match = this.mathRegex.Match(clipboardText.Replace(',', '.'));
            return match.Success;
        }

        public ActionResult TryExecute(string clipboardText)
        {
            var actionResult = new ActionResult(clipboardText);

            var match = this.mathRegex.Match(clipboardText.Replace(',', '.'));
            if (!match.Success)
            {
                actionResult.IsProcessed = false;
            }
            else
            {
                var operators = (from Capture capture
                        in match.Groups["operator"].Captures
                                 select capture.Value).ToList();

                var lhs = double.Parse(match.Groups["lhs"].Value, CultureInfo.InvariantCulture);

                var rhss = (from Capture capture
                        in match.Groups["rhs"].Captures
                            select double.Parse(capture.Value, CultureInfo.InvariantCulture)).ToArray();

                var answer = lhs;

                var i = 0;

                for (var i2 = rhss.Length - 1; i2 >= 0; i2--)
                {
                    answer = this.binaryOperators[operators[i++]](answer, rhss[i2]);
                }

                Clipboard.SetText(answer.ToString(CultureInfo.CurrentCulture));
                actionResult.Description = answer.ToString(CultureInfo.CurrentCulture);
            }

            return actionResult;
        }
    }
}