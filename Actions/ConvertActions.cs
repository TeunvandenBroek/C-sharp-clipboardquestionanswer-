using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace it.Actions
{

    internal class ConvertActions : ActionBase
    {
        private readonly Regex unitRegex = new Regex("(?<number>^[0-9]+([.,][0-9]{1,3})?)(\\s*)(?<from>[a-z]+[2-3]?) to (?<to>[a-z]+[2-3]?)");


        override public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();

            Match matches = unitRegex.Match(clipboardText);

            if (!matches.Success)
            {
                actionResult.IsProcessed = false;
            }
            else
            {

                double number = double.Parse(matches.Groups["number"].Value);
                string from = matches.Groups["from"].Value;
                string to = matches.Groups["to"].Value;
                double meter = 0, gram = 0, liter = 0, oppervlakte = 0;
                switch (from)
                {
                    // lengte eenheden
                    case "mm":
                    case "millimeter":
                        meter = number / 1000;
                        break;
                    case "cm":
                    case "centimer":
                        meter = number / 100;
                        break;
                    case "dm":
                    case "decimeter":
                        meter = number / 10;
                        break;
                    case "m":
                    case "meter":
                        meter = number;
                        break;
                    case "dam":
                    case "decameter":
                        meter = number * 1;
                        break;
                    case "hm":
                    case "hectometer":
                        meter = number * 100;
                        break;
                    case "km":
                    case "kilometer":
                        meter = number * 1000;
                        break;
                    case "feet":
                    case "ft":
                        meter = number * 0.3048;
                        break;
                    case "inch":
                        meter = number * 0.0254;
                        break;
                    case "mile":
                    case "miles":
                        meter = number / 0.00062137;
                        break;
                    case "yard":
                    case "yd":
                        meter = number * 0.9144;
                        break;
                    // gewicht eenheden
                    case "mg":
                    case "milligram":
                        gram = number / 1000;
                        break;
                    case "cg":
                    case "centigram":
                        gram = number / 100;
                        break;
                    case "dg":
                    case "decigram":
                        gram = number / 10;
                        break;
                    case "gr":
                    case "gram":
                        gram = number;
                        break;
                    case "dag":
                    case "decagram":
                        gram = number * 10;
                        break;
                    case "hg":
                    case "hectogram":
                        gram = number * 100;
                        break;
                    case "kg":
                    case "kilogram":
                        gram = number * 1000;
                        break;
                    case "ml":
                    case "milliliter":
                        liter = number / 1000;
                        break;
                    case "cl":
                    case "centiliter":
                        liter = number / 100;
                        break;
                    case "dl":
                    case "deciliter":
                        liter = number / 10;
                        break;
                    case "l":
                    case "liter":
                        liter = number;
                        break;
                    case "dal":
                    case "decaliter":
                        liter = number * 10;
                        break;
                    case "hl":
                    case "hectoliter":
                        liter = number * 100;
                        break;
                    case "kl":
                    case "kiloliter":
                        liter = number * 1000;
                        break;
                    // oppervlakte eenheden
                    case "mm2":
                        oppervlakte = number / 1000000;
                        break;
                    case "cm2":
                        oppervlakte = number / 10000;
                        break;
                    case "dm2":
                        oppervlakte = number / 100;
                        break;
                    case "m2":
                        oppervlakte = number;
                        break;
                    case "dam2":
                        oppervlakte = number * 100;
                        break;
                    case "hm2":
                        oppervlakte = number * 10000;
                        break;
                    case "km2":
                        oppervlakte = number * 1000000;
                        break;
                    default:
                        break;
                }

                // oppervlakte eenheden (area units)
                double result = 0;

                switch (to) // naar (to)
                {
                    // lengte eenheden
                    case "mm":
                    case "millimeter":
                        result = meter * 1000;
                        break;
                    case "cm":
                    case "centimer":
                        result = meter * 100;
                        break;
                    case "dm":
                    case "decimeter":
                        result = meter * 10;
                        break;
                    case "m":
                    case "meter":
                        result = meter;
                        break;
                    case "dam":
                    case "decameter":
                        result = meter / 1;
                        break;
                    case "hm":
                    case "hectometer":
                        result = meter / 100;
                        break;
                    case "km":
                    case "kilometer":
                        result = meter / 1000;
                        break;
                    case "feet":
                    case "ft":
                        result = meter / 0.3048;
                        break;
                    case "inch":
                        result = meter / 0.0254;
                        break;
                    case "mile":
                    case "miles":
                        result = meter * 0.00062137;
                        break;
                    case "yard":
                    case "yd":
                        result = meter * 0.9144;
                        break;
                    // gewicht eenheden (Weight Units)
                    case "mg":
                    case "milligram":
                        result = gram * 1000;
                        break;
                    case "cg":
                    case "centigram":
                        result = gram * 100;
                        break;
                    case "dg":
                    case "decigram":
                        result = gram * 10;
                        break;
                    case "gr":
                    case "gram":
                        result = gram;
                        break;
                    case "dag":
                    case "decagram":
                        result = gram / 10;
                        break;
                    case "hg":
                    case "hectogram":
                        result = gram / 100;
                        break;
                    case "kg":
                    case "kilogram":
                        result = gram / 1000;
                        break;
                    // inhoud (volume units)
                    case "ml":
                    case "milliliter":
                        result = liter * 1000;
                        break;
                    case "cl":
                    case "centiliter":
                        result = liter * 100;
                        break;
                    case "dl":
                    case "deciliter":
                        result = liter * 10;
                        break;
                    case "l":
                    case "liter":
                        result = liter;
                        break;
                    case "dal":
                    case "decaliter":
                        result = liter / 10;
                        break;
                    case "hl":
                    case "hectoliter":
                        result = liter / 100;
                        break;
                    case "kl":
                    case "kiloliter":
                        result = liter / 1000;
                        break;
                    // oppervlakte eenheden (Area Units)
                    case "mm2":
                        result = oppervlakte * 1000000;
                        break;
                    case "cm2":
                        result = oppervlakte * 10000;
                        break;
                    case "dm2":
                        result = oppervlakte * 100;
                        break;
                    case "m2":
                        result = oppervlakte;
                        break;
                    case "dam2":
                        result = oppervlakte / 100;
                        break;
                    case "hm2":
                        result = oppervlakte / 10000;
                        break;
                    case "km2":
                        result = oppervlakte / 1000000;
                        break;
                    default:
                        break;
                }

                Clipboard.SetText(result.ToString(CultureInfo.CurrentCulture));
                actionResult.Title = clipboardText;
                actionResult.Description = result + to;
            }

            return actionResult;
        }

    }
}
