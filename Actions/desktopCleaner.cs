using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace it.Actions
{
    internal sealed class desktopCleaner : IAction
    {
        public bool Matches(string clipboardText)
        {
            return clipboardText.EndsWith("desktop cleaner", StringComparison.Ordinal) || clipboardText.EndsWith("bureaublad schoonmaak", StringComparison.Ordinal);
        }

        public ActionResult TryExecute(string clipboardText)
        {
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                throw new ArgumentException("message", nameof(clipboardText));
            }
            ActionResult actionResult = new ActionResult();
            System.Globalization.CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string cleanupPath = System.IO.Path.Combine(desktopPath, "Cleanup");
            System.IO.Directory.CreateDirectory(cleanupPath);
            string imagesFolder = System.IO.Path.Combine(cleanupPath, "Afbeeldingen");
            System.IO.Directory.CreateDirectory(imagesFolder);
            string audioFolder = System.IO.Path.Combine(cleanupPath, "Audio");
            System.IO.Directory.CreateDirectory(audioFolder);
            string textFolder = System.IO.Path.Combine(cleanupPath, "Text");
            System.IO.Directory.CreateDirectory(textFolder);
            string videosFolder = System.IO.Path.Combine(cleanupPath, "Video");
            System.IO.Directory.CreateDirectory(videosFolder);

            string[] array = Directory.GetFiles(desktopPath);
            for (int i = 0; i < array.Length; i++)
            {
                string fileName = array[i];
                if (fileName.EndsWith(".jpg"))
                    File.Move(fileName, Path.Combine(cleanupPath, "Afbeeldingen", Path.GetFileName(fileName)));
                if (fileName.EndsWith(".aif"))
                    File.Move(fileName, Path.Combine(cleanupPath, "Audio", Path.GetFileName(fileName)));
                if (fileName.EndsWith(".txt"))
                    File.Move(fileName, Path.Combine(cleanupPath, "Text", Path.GetFileName(fileName)));
                if (fileName.EndsWith(".3g2"))
                    File.Move(fileName, Path.Combine(cleanupPath, "Video", Path.GetFileName(fileName)));
            }

            switch (currentCulture.LCID)
            {
                case 1033: // english-us
                    {
                        actionResult.Title = "Desktop cleaner";
                        actionResult.Description = "succes";
                        break;
                    }
                case 1043: // dutch
                    {
                        actionResult.Title = "Desktop cleaner";
                        actionResult.Description = "Bestanden opgeruimt en gesorteerd in de Cleanup map";
                        break;
                    }

                default:
                    break;
            }
            return actionResult;
        }
    }
}