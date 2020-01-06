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

        private static Dictionary<string, string> CategoryAssociations = new Dictionary<string, string>
        (StringComparer.Ordinal)
        {
            { ".7zip",  "Compressed" },
            { ".bmp" ,"Images" },
            { ".jpg" , "Images" },
            { ".png" ,"Images" },
            { ".rar" , "Compressed" },
            { ".zip" , "Compressed" },
            {".txt", "Text" }
        };

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
            string imagesFolder = System.IO.Path.Combine(cleanupPath, "Images");
            System.IO.Directory.CreateDirectory(imagesFolder);
            string audioFolder = System.IO.Path.Combine(cleanupPath, "Audio");
            System.IO.Directory.CreateDirectory(audioFolder);
            string compressedFolder = System.IO.Path.Combine(cleanupPath, "Compressed");
            System.IO.Directory.CreateDirectory(compressedFolder);
            string textFolder = System.IO.Path.Combine(cleanupPath, "Text");
            System.IO.Directory.CreateDirectory(textFolder);
            string videosFolder = System.IO.Path.Combine(cleanupPath, "Video");
            System.IO.Directory.CreateDirectory(videosFolder);
            string overig = System.IO.Path.Combine(cleanupPath, "overig");
            System.IO.Directory.CreateDirectory(overig);

            string[] array = Directory.GetFiles(desktopPath);
            for (int i = 0; i < array.Length; i++)
            {
                string file = array[i];
                if (Path.HasExtension(file))
                {
                    if (!CategoryAssociations.ContainsKey(Path.GetExtension(file)))
                    {
                        File.Move(file, Path.Combine(cleanupPath, "overig", Path.GetFileName(file)));
                    }
                    else
                    {
                        File.Move(file, Path.Combine(cleanupPath, CategoryAssociations[Path.GetExtension(file)], Path.GetFileName(file)));
                    }
                }
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