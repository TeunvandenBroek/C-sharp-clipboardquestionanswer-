using System;
using System.Windows;

namespace it.Actions
{
    public sealed class numberToHex : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.EndsWith(" to hex", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }

            ActionResult actionResult = new ActionResult();
            int toHexIndex = clipboardText.IndexOf("to hex", StringComparison.Ordinal);
            clipboardText.Substring(0, toHexIndex);
            string hex = toHexIndex.ToString("X");
            actionResult.Title = "Calculate hex";
            actionResult.Description = $"{clipboardText}, {hex}";
            Clipboard.SetText($"{hex}");
            return actionResult;
        }
    }
}
