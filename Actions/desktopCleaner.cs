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
using System.Security.Principal;
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
            { ".iso" , "Disc/Iso" },
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
            { ".c" , "Programming/C" },
            { ".class" , "Programming/Classes" },
            { ".dart" , "Programming/Dart" },
            { ".py" , "Programming/Python"},
            { ".sh" , "Programming/Shell" },
            { ".swift" , "Programming/Swift" },
            { ".html" , "Programming/HTML" },
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
        private static void CreateSubMaps(string dir)
        {
            string cleanupPath = Path.Combine(dir);
            //sub maps desktop cleaner
            var subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Audio")); ;
            {
                //Subfolders in Audio folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Text"));
            {
                //Subfolders in Text folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Video"));
            {
                //Subfolders in Video folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Images"));
            {
                //Subfolders in Image folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Internet"));
            {
                //Subfolders in Internet folder
            }

            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Compressed"));
            {
                //Subfolders in Compressed folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Disc"));
            {
                //Subfolders in Disc folder
                subFolders.CreateSubdirectory("Iso");
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Data"));
            {
                //Subfolders in Data folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Executables"));
            {
                //Subfolders in Executeables folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Fonts"));
            {
                //Subfolders in Fonts folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Presentations"));
            {
                //Subfolders in Presentations folder
                subFolders.CreateSubdirectory("Powepoints");

            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Programming"));
            {
                //Subfolders in Programming folder
                subFolders.CreateSubdirectory("Python");
                subFolders.CreateSubdirectory("HTML");
                subFolders.CreateSubdirectory("Swift");
                subFolders.CreateSubdirectory("Dart");
                subFolders.CreateSubdirectory("C");
                subFolders.CreateSubdirectory("Classes");
                subFolders.CreateSubdirectory("Shell");
            }
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Spreadsheets"));
            Directory.CreateDirectory(Path.Combine(cleanupPath, "System"));
            string overig = Path.Combine(cleanupPath, "Overig");
            Directory.CreateDirectory(overig);

            string[] array1 = Directory.GetFiles(dir,"*", SearchOption.AllDirectories);
            for (int i = 0; i < array1.Length; i++)
            {
                string file = array1[i];
                if (Path.HasExtension(file))
                {
                    try
                    {
                        if (CategoryAssociations.TryGetValue(Path.GetExtension(file), out dir))
                        {
                            File.Move(file, Path.Combine(cleanupPath, dir, Path.GetFileName(file)));
                        }
                        else
                        {
                            File.Move(file, Path.Combine(cleanupPath, "Overig", Path.GetFileName(file)));
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
        private static void MoveFolders(string dir)
        {
            try
            {
                string directoryName = dir;
                DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
                if (dirInfo.Exists == false)
                    Directory.CreateDirectory(directoryName);

                List<string> MyFiles = Directory
                                   .GetFiles(dir, "*.*", SearchOption.AllDirectories).ToList();

                foreach (string file in MyFiles)
                {
                    FileInfo mFile = new FileInfo(file);
                    // to remove name collisions
                    if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
                    {
                        mFile.MoveTo(dirInfo + "\\" + mFile.Name);
                    }

                }

            }
            catch (Exception)
            {

            }
        }
        private static void DeleteEmptyDirs(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                throw new ArgumentException(
                    "Starting directory is a null reference or an empty string",
                    "dir");

            try
            {
                foreach (var d in Directory.EnumerateDirectories(dir))
                {
                    DeleteEmptyDirs(d);
                }

                var entries = Directory.EnumerateFileSystemEntries(dir);

                if (!entries.Any())
                {
                    try
                    {
                        try
                        {
                            Directory.Delete(dir);

                            if (Directory.GetFiles(dir).Length == 0 &&
                             Directory.GetDirectories(dir).Length == 0)
                            {
                                Directory.Delete(dir, false);
                            }

                        }
                        catch (Exception)
                        {

                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (UnauthorizedAccessException) { }
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


            //DOWNLOADS 
            string downloadPath = (KnownFolders.GetPath(KnownFolder.Downloads));
            MoveFolders(downloadPath);
            CreateSubMaps(downloadPath);
            DeleteEmptyDirs(downloadPath);

            string picturesPath = (KnownFolders.GetPath(KnownFolder.Pictures));
            MoveFolders(picturesPath);
            CreateSubMaps(picturesPath);
            DeleteEmptyDirs(picturesPath);

            string videoPath = (KnownFolders.GetPath(KnownFolder.Videos));
            MoveFolders(videoPath);
            CreateSubMaps(videoPath);
            DeleteEmptyDirs(videoPath);

            string musicPath = (KnownFolders.GetPath(KnownFolder.Music));
            MoveFolders(musicPath);
            CreateSubMaps(musicPath);
            DeleteEmptyDirs(musicPath);

            string documentsPath = (KnownFolders.GetPath(KnownFolder.Documents));
            DeleteEmptyDirs(documentsPath);
            //MoveFolders(documentsPath);
            //CreateSubMaps(documentsPath);

            //delete empty dirs
            DirectoryInfo directoryInfo = new DirectoryInfo(picturesPath);
            DirectoryInfo directoryInfo1 = new DirectoryInfo(videoPath);
            DirectoryInfo directoryInfo2 = new DirectoryInfo(musicPath);
            DirectoryInfo directoryInfo3 = new DirectoryInfo(downloadPath);
           
            if (Directory.Exists(picturesPath + videoPath + musicPath + downloadPath))
            {
                File.SetAttributes(picturesPath + videoPath + musicPath + downloadPath, FileAttributes.Normal);
                DeleteEmptyDirs(picturesPath + videoPath + musicPath + downloadPath);
            }
            //create cleanup map on desktop
            string cleanupPath = Path.Combine(desktopPath, "Cleanup");

            //sub maps desktop cleaner
            var subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath,"Audio"));;
            {
                //Subfolders in Audio folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Text"));
            {
                //Subfolders in Text folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Video"));
            {
                //Subfolders in Video folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Images"));
            {
                //Subfolders in Image folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Internet"));
            {
                //Subfolders in Internet folder
            }

            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Compressed"));
            {
                //Subfolders in Compressed folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Disc"));
            {
                //Subfolders in Disc folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Data"));
            {
                //Subfolders in Data folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Executables"));
            {
                //Subfolders in Executeables folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath,  "Fonts"));
            {
                //Subfolders in Fonts folder
            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath, "Presentations"));
            {
                //Subfolders in Presentations folder
                subFolders.CreateSubdirectory("Powepoints");

            }
            subFolders = Directory.CreateDirectory(Path.Combine(cleanupPath,"Programming"));
            {
                //Subfolders in Programming folder
                subFolders.CreateSubdirectory("Python");
                subFolders.CreateSubdirectory("HTML");
                subFolders.CreateSubdirectory("Swift");
                subFolders.CreateSubdirectory("Dart");
                subFolders.CreateSubdirectory("C");
                subFolders.CreateSubdirectory("Classes");
                subFolders.CreateSubdirectory("Shell");
            }
            Directory.CreateDirectory(Path.Combine(cleanupPath, "Spreadsheets"));
            Directory.CreateDirectory(Path.Combine(cleanupPath,  "System"));
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
            string[] array1 = Directory.GetFiles(desktopPath, "*", SearchOption.AllDirectories);
            for (int i = 0; i < array1.Length; i++)
            {
                string file = array1[i];
                if (Path.HasExtension(file))
                {
                    try
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
                    catch (Exception) { }
                }
            }

    
            {
                try
                {
                    try
                    {
                        ConsoleKeyInfo cki;
                        double totalSize = 0;
                        var fileList = directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList();
                        fileList.AddRange(directoryInfo1.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList());
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
                                        using (var fs = new BufferedStream(File.OpenRead(item), 1200000))
                                        {
                                            finalDetails.Add(new FileDetails()
                                            {
                                                FileName = item,
                                                FileHash = BitConverter.ToString(MD5.Create().ComputeHash(fs)),
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
            }
            return actionResult;
        }
    }
}