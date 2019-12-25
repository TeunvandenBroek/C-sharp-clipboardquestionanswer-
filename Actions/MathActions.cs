namespace it.Actions
{
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public sealed class MathActions : IAction
    {
        private readonly IReadOnlyDictionary<string, Func<double, double, double>> binaryOperators =
        new Dictionary<string, Func<double, double, double>>(
        StringComparer.Ordinal)
                {
                    { "%", (a, b) => a / b * 100 },
                    { "x", (a, b) => a * b },
                    { "*", (a, b) => a * b },
                    { "/", (a, b) => b == 0 ? double.NaN : a / b },
                    { ":", (a, b) => b == 0 ? double.NaN : a / b },
                    { "+", (a, b) => a + b },
                    { "-", (a, b) => a - b },
                };

        private readonly Regex mathRegex =
            new Regex(@"^(?<lhs>\d+(?:[,.]{1}\d+)*)(([ ]*(?<operator>[+\-\:x\%\*/])[ ]*(?<rhs>\d+(?:[,.]{1}\d+)*)+)+)");

        public override bool Equals(object obj)
        {
            return Equals(obj as MathActions);
        }

        public bool Matches(string clipboardText)
        {
            if (clipboardText is null)
            {
                throw new ArgumentNullException(nameof(clipboardText));
            }

            Match match = mathRegex.Match(clipboardText.Replace(',', '.'));
            return match.Success;
        }

        public ActionResult TryExecute(string clipboardText)
        { 
            ActionResult actionResult = new ActionResult(clipboardText);

            Match match = mathRegex.Match(clipboardText.Replace(',', '.'));
            IReadOnlyList<string> operators = (from Capture capture
                       in match.Groups["operator"].Captures
                                               select capture.Value).ToList();

            double lhs = double.Parse(match.Groups["lhs"].Value, CultureInfo.InvariantCulture);

            double[] rhss = (from Capture capture
                    in match.Groups["rhs"].Captures
                             select double.Parse(capture.Value, CultureInfo.InvariantCulture)).ToArray();

            double answer = lhs;

            for (int i = 0; i < rhss.Length; i++)
            {
                answer = binaryOperators[operators[i]](answer, rhss[i]);
            }

            Clipboard.SetText(answer.ToString(CultureInfo.CurrentCulture));
            actionResult.Description = answer.ToString(CultureInfo.CurrentCulture);
            return actionResult;
        }
    }
}