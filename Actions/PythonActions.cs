using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it.Actions
{
    internal sealed class PythonActions : IAction
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as PythonActions);
        }


        public bool Matches(string clipboardText = null)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }

            return clipboardText.EndsWith("python", StringComparison.Ordinal);
        }
        ActionResult IAction.TryExecute(string clipboardText)
        {
            {
                ActionResult actionResult = new ActionResult(clipboardText);
                switch (clipboardText.ToLower(CultureInfo.InvariantCulture))
                {
                    case "python":
                        {
                            actionResult.Description = "successfully";
                            ScriptEngine engine = Python.CreateEngine();
                            engine.ExecuteFile(@"helloworld.py");
                            break;
                        }
                    default:
                        {
                            return actionResult;
                        }
                }

                return actionResult;
            }
        }
    }
}
