namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public sealed class MathActions : IAction, IEquatable<MathActions>
    {
        private readonly IReadOnlyDictionary<string, Func<double, double, double>> binaryOperators =
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

        public override bool Equals(object obj)
        {
            return Equals(obj as MathActions);
        }

        public bool Equals(MathActions other)
        {
            return other != null &&
                   EqualityComparer<IReadOnlyDictionary<string, Func<double, double, double>>>.Default.Equals(binaryOperators, other.binaryOperators) &&
                   EqualityComparer<Regex>.Default.Equals(mathRegex, other.mathRegex);
        }

        public override int GetHashCode()
        {
            var hashCode = 1935864899;
            hashCode = hashCode * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, Func<double, double, double>>>.Default.GetHashCode(binaryOperators);
            hashCode = hashCode * -1521134295 + EqualityComparer<Regex>.Default.GetHashCode(mathRegex);
            return hashCode;
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

            int i = 0;

            for (int i2 = 0; i2 < rhss.Length; i2++)
            {
                answer = binaryOperators[operators[i++]](answer, rhss[i2]);
            }

            Clipboard.SetText(answer.ToString(CultureInfo.CurrentCulture));
            actionResult.Description = answer.ToString(CultureInfo.CurrentCulture);

            return actionResult;
        }

        public static bool operator ==(MathActions left, MathActions right)
        {
            return EqualityComparer<MathActions>.Default.Equals(left, right);
        }

        public static bool operator !=(MathActions left, MathActions right)
        {
            return !(left == right);
        }
    }
} 