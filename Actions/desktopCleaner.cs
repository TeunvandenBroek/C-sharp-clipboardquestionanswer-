using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
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

        private static readonly Dictionary<string, string> CategoryAssociations = new Dictionary<string, string>
        (StringComparer.Ordinal)
        {
            //audio
            {".aif", "Audio" },
            {".cda", "Audio" },
            {".mid", "Audio" },
            {".midi", "Audio" },
            {".mp3", "Audio" },
            {".mpa", "Audio" },
            {".ogg", "Audio" },
            {".wav", "Audio" },
            {".wma", "Audio" },
            {".wpl", "Audio" },
            {".m3u", "Audio" },
            //text
            {".txt", "Text" },
            {".doc", "Text" },
            {".docx", "Text" },
            {".odt", "Text" },
            {".pdf", "Text" },
            {".rtf", "Text" },
            {".tex", "Text" },
            {".wks", "Text" },
            {".wps", "Text" },
            {".wpd", "Text" },
            //video
            {".3g2", "Video" },
            {".3gp", "Video" },
            {".avi", "Video" },
            {".flv", "Video" },
            {".h264", "Video" },
            {".m4v", "Video" },
            {".mkv", "Video" },
            {".mov", "Video" },
            {".mp4", "Video" },
            {".mpg", "Video" },
            {".mpeg", "Video" },
            {".rm", "Video" },
            {".swf", "Video" },
            {".vob", "Video" },
            {".wmv", "Video" },
             //images
            { ".ai" ,"Images" },
            { ".bmp" , "Images" },
            { ".gif" ,"Images" },
            { ".ico" ,"Images" },
            { ".jpg" , "Images" },
            { ".jpeg" ,"Images" },
            { ".png" ,"Images" },
            { ".ps" , "Images" },
            { ".psd" ,"Images" },
            { ".svg" ,"Images" },
            { ".tif" , "Images" },
            { ".tiff" ,"Images" },
            { ".CR2" ,"Images" },
            //internet
            { ".asp" ,"Internet" },
            { ".aspx" ,"Internet" },
            { ".cer" ,"Internet" },
            { ".cfm" ,"Internet" },
            { ".cgi" ,"Internet" },
            { ".pl" ,"Internet" },
            { ".css" ,"Internet" },
            { ".htm" ,"Internet" },
            { ".js" ,"Internet" },
            { ".jsp" ,"Internet" },
            { ".part" ,"Internet" },
            { ".php" ,"Internet" },
            { ".rss" ,"Internet" },
            { ".xhtml" ,"Internet" },
            //compressed
            { ".7z",  "Compressed" },
            { ".arj" , "Compressed" },
            { ".deb" , "Compressed" },
            { ".pkg",  "Compressed" },
            { ".rar" , "Compressed" },
            { ".tar.gz" , "Compressed" },
            { ".z" , "Compressed" },
            { ".zip" , "Compressed" },
            //disc
            { ".bin" , "Disc" },
            { ".dmg" , "Disc" },
            { ".iso" , "Disc" },
            { ".toast" , "Disc" },
            { ".vcd" , "Disc" },
            //data
            { ".csv" , "Data" },
            { ".dat" , "Data" },
            { ".db" , "Data" },
            { ".dbf" , "Data" },
            { ".log" , "Data" },
            { ".mdb" , "Data" },
            { ".sav" , "Data" },
            { ".sql" , "Data" },
            { ".tar" , "Data" },
            { ".xml" , "Data" },
            { ".json" , "Data" },
            //executables
            { ".apk" , "Executables" },
            { ".bat" , "Executables" },
            { ".com" , "Executables" },
            { ".exe" , "Executables" },
            { ".gadget" , "Executables" },
            { ".jar" , "Executables" },
            { ".wsf" , "Executables" },
            //fonts
            { ".fnt" , "Fonts" },
            { ".fon" , "Fonts" },
            { ".otf" , "Fonts" },
            { ".ttf" , "Fonts" },
            //presentations
            { ".key" , "Presentations" },
            { ".odp" , "Presentations" },
            { ".pps" , "Presentations" },
            { ".ppt" , "Presentations" },
            { ".pptx" , "Presentations" },
            //programming
            { ".c" , "Programming" },
            { ".class" , "Programming" },
            { ".dart" , "Programming" },
            { ".py" , "Programming" },
            { ".sh" , "Programming" },
            { ".swift" , "Programming" },
            { ".html" , "Programming" },
            { ".h" , "Programming" },
            //spreadsheets
            { ".ods" , "Spreadsheets" },
            { ".xlr" , "Spreadsheets" },
            { ".xls" , "Spreadsheets" },
            { ".xlsx" , "Spreadsheets" },
            //System
            { ".bak" , "System" },
            { ".cab" , "System" },
            { ".cfg" , "System" },
            { ".cpl" , "System" },
            { ".cur" , "System" },
            { ".ddl" , "System" },
            { ".dmp" , "System" },
            { ".drv" , "System" },
            { ".icns" , "System" },
            { ".ini" , "System" },
            { ".lnk" , "System" },
            { ".msi" , "System" },
            { ".sys" , "System" },
            { ".tmp" , "System" },
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

            //create cleanup map on desktop
            string cleanupPath = Path.Combine(desktopPath, "Cleanup");
            Directory.CreateDirectory(cleanupPath);

            //sub maps desktop cleaner
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Audio"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Text"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Video"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Images"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Internet"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Compressed"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Disc"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Data"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Executables"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Fonts"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Presentations"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Programming"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Spreadsheets"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "System"));
            string overig = Path.Combine(cleanupPath, "Overig");
            Directory.CreateDirectory(overig);

            //delete temp files
            const int defaultBufferSize = 4096;
            using var stream = new FileStream(
                Path.GetTempFileName(),
                FileMode.Create, FileAccess.ReadWrite, FileShare.None,
                defaultBufferSize,
                FileOptions.DeleteOnClose);

            // move files from desktop
            string[] array = Directory.GetFiles(desktopPath);
            for (int i = 0; i < array.Length; i++)
            {
                string file = array[i];
                if (Path.HasExtension(file))
                {
                    if (CategoryAssociations.TryGetValue(Path.GetExtension(file), out desktopPath))
                    {
                        File.Move(file, Path.Combine(cleanupPath, desktopPath, Path.GetFileName(file)));
                    }
                    else
                    {
                        File.Move(file, Path.Combine(cleanupPath, "Overig", Path.GetFileName(file)));
                    }
                }
            }

    
            {
                try
                {
                    ConsoleKeyInfo cki;
                    double totalSize = 0;
                    string path = (KnownFolders.GetPath(KnownFolder.Pictures));
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);

                    string path1 = (KnownFolders.GetPath(KnownFolder.Videos));
                    DirectoryInfo directoryInfo1 = new DirectoryInfo(path1);

                    string path2 = (KnownFolders.GetPath(KnownFolder.Music));
                    DirectoryInfo directoryInfo2 = new DirectoryInfo(path2);

                    string path3 = (KnownFolders.GetPath(KnownFolder.Downloads));
                    DirectoryInfo directoryInfo3 = new DirectoryInfo(path3);

                    var fileList = directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList();
                    fileList.AddRange(directoryInfo1.EnumerateFiles("*.*",SearchOption.AllDirectories).ToList());
                    fileList.AddRange(directoryInfo2.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList());
                    fileList.AddRange(directoryInfo3.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList());

                    List<FileDetails> finalDetails = new List<FileDetails>(1000);
                    List<string> ToDelete = new List<string>(1000);
                    finalDetails.Clear();
                    {
                        {
                            for (int i = 0; i < fileList.Count; i++)
                            { 
                                {
                                    string item = fileList[i].FullName;
                                    using (var fs = new FileStream(item, FileMode.Open, FileAccess.Read))
                                    {
                                        finalDetails.Add(new FileDetails()
                                        {
                                            FileName = item,
                                            FileHash = BitConverter.ToString(SHA1.Create().ComputeHash(fs)),
                                        });
                                    }
                                }
                            }
                        }
                        var similarList = finalDetails.GroupBy(f => f.FileHash)
                       .Select(g => new { FileHash = g.Key, Files = g.Select(z => z.FileName).ToList() });


                        ToDelete.AddRange(similarList.SelectMany(f => f.Files.Skip(1)).ToList());
                        if (ToDelete.Count > 0)
                        {
                            Console.WriteLine("Files being deleted- ");
                            for (int i = 0; i < ToDelete.Count; i++)
                            {
                                string item = ToDelete[i];
                                Console.WriteLine(item);
                                FileInfo fi = new FileInfo(item);
                                totalSize += fi.Length;
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Total space made free-  {0}mb", Math.Round((totalSize / 1000000), 6).ToString());
                        {
                            ToDelete.ForEach(File.Delete);
                        }
                    }
                }
                catch (System.UnauthorizedAccessException)
                {

                }
                catch (System.NotSupportedException)
                {
                    
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