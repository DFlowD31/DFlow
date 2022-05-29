using LazyPortal.Classes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyPortal.services
{
    public static class Poster
    {
        #region FolderDll

        public enum FolderCustomSettingsMask : uint
        {
            InfoTip = 0x00000004,
            Clsid = 0x00000008,
            IconFile = 0x00000010,
            Logo = 0x00000020,
            Flags = 0x00000040
        }

        public enum FolderCustomSettingsRW : uint
        {
            Read = 0x00000001,
            ForceWrite = 0x00000002,
            ReadWrite = Read | ForceWrite
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        static extern uint SHGetSetFolderCustomSettings(ref SHFOLDERCUSTOMSETTINGS pfcs, string pszPath, FolderCustomSettingsRW dwReadWrite);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        struct SHFOLDERCUSTOMSETTINGS
        {
            public uint dwSize;
            public FolderCustomSettingsMask dwMask;
            public IntPtr pvid;
            public string pszWebViewTemplate;
            public uint cchWebViewTemplate;
            public string pszWebViewTemplateVersion;
            public string pszInfoTip;
            public uint cchInfoTip;
            public IntPtr pclsid;
            public uint dwFlags;
            public string pszIconFile;
            public uint cchIconFile;
            public int iIconIndex;
            public string pszLogo;
            public uint cchLogo;
        }

        #endregion

        private static readonly Main main = Program.Main_Form;

        public static void SetPoster(string the_directory = "", mediaTypes type = mediaTypes.movie, bool container = false)
        {
            Task set_poster_task = Task.Run(() => setPoster(the_directory, type, container));
            set_poster_task.Wait();
        }

        private static void setPoster(string the_directory = "", mediaTypes type = mediaTypes.movie, bool container = false)
        {
            try
            {
                //mediaTypes type;
                List<DirectoryInfo> directories = new List<DirectoryInfo>();
                if (the_directory != "")
                {
                    if (container)
                        directories = new DirectoryInfo(the_directory).GetDirectories().ToList();
                    else
                        directories.Add(new DirectoryInfo(the_directory));

                    main.log(@"Getting poster for """ + the_directory + @""" " + type, msgType.success);
                    goto dataProvided;
                }

                using (FolderBrowserDialog Folder_Browser_Dialog = main.get_folderbrowserdialog())
                {
                    if (Folder_Browser_Dialog == null)
                        return;
                    if (new choice_box().ShowDialog() == DialogResult.OK)
                    {
                        if (choice.series)
                            type = mediaTypes.tv;
                        else
                            type = mediaTypes.movie;
                        if (choice.container)
                            directories = new DirectoryInfo(Folder_Browser_Dialog.SelectedPath).GetDirectories().ToList();
                        else
                            directories.Add(new DirectoryInfo(Folder_Browser_Dialog.SelectedPath));
                    }
                    else
                        return;
                }

            dataProvided:
                main.max_progressbar(directories.Count, main.ProgressBar);
                main.update_progressbar(0, main.ProgressBar);


                foreach (DirectoryInfo directory in directories)
                {
                    if (Path.GetFileNameWithoutExtension(directory.Name).ToLower() != "tv")
                    {
                        FileInfo[] imageInfo = directory.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(f => extension.imageEXT.Contains(f.Extension.ToLower())).ToArray();
                        string iniPath = directory.FullName + @"\desktop.ini";
                        string icoPath = directory.FullName + @"\folder.ico";
                        if (File.Exists(icoPath)) File.Delete(icoPath);
                        if (File.Exists(iniPath)) File.Delete(iniPath);

                        Bitmap mainBitmap = null;

                        if (imageInfo.Length > 0 && imageInfo.Where(f => f.Name.Contains("YTS")).Count() <= 0)
                            mainBitmap = new Bitmap(imageInfo[0].FullName);
                        else
                        {
                            string name = Path.GetFileNameWithoutExtension(directory.Name);
                            List<int> years = new List<int>();

                            removeFromName<quality>(ref name);

                            foreach (Match match in new Regex(@"\d{4}").Matches(name))
                                years.Add(Convert.ToInt32(match.Value));

                            years.Sort((a, b) => b.CompareTo(a));

                            try { name = Regex.Replace(name, $@"\b{years[0]}\b", string.Empty); } catch (Exception) { }

                            if (years.Count() == 0)
                                years.Add(0);

                            string folderName = name;
                            int s_number = 0;

                            name = name.Replace(".", " ");

                            if (type == mediaTypes.tv)
                            {
                                try
                                {
                                    if (name.Contains("第") && name.Contains("期"))
                                        s_number = Convert.ToInt32(Regex.Match(Regex.Match(name, @"(?:\s+第\s+\d+\s+期|\s+第\s+\d{2}\s+期)|\s+\d+|\s+\d{2}").ToString(), @"\d{2}|\d+").Groups[0].Value);
                                    else
                                        s_number = Convert.ToInt32(Regex.Match(Regex.Match(name, @"(?:\s+Season\s+\d+|\s+Season\s+\d{2})|(\s+S\d{2}E\d{2})|(\s+S\d{2})|(\s+S\d+)|(\s+S\d+E\d+)|(\d+)|(\d{2})").ToString(), @"\d{2}|\d+").Groups[0].Value);
                                }
                                catch (Exception ex) { main.log(ex.ToString(), msgType.error); }
                            }

                            removeFromName<audio_codec>(ref name);
                            removeFromName<encoder>(ref name);
                            removeFromName<source>(ref name);
                            removeFromName<video_codec>(ref name);
                            //removeFromName<languageDB>(ref name);

                            name = Regex.Replace(name, @"[^0-9a-zA-Z\s&一-龯ぁ-んァ-ン\w！：／・]", string.Empty);
                            name = new Regex("[ ]{2,}", RegexOptions.None).Replace(name, " ");
                            name = name.ToLower();

                            if (name.Contains("collection"))
                            {
                                type = mediaTypes.collection;
                                name = name.Replace("collection", string.Empty);
                                name = new Regex("[ ]{2,}", RegexOptions.None).Replace(name, " ");
                            }

                            removeFromName<audio_channel>(ref name);

                            main.log("Simplified name:= " + name, msgType.message);

                            main.max_progressbar(name.Count(f => (f == ' ')), main.Mini_ProgressBar);
                            main.update_progressbar(0, main.Mini_ProgressBar);

                            while (name != string.Empty)
                            {
                                foreach (int year in years)
                                {
                                    RestResponse response = null;
                                    response = new RestClient("https://api.themoviedb.org/3/search/" + type.ToString() + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94&query=" + System.Web.HttpUtility.UrlEncode(name) + "&page=1&include_adult=true&" + mediaType.getDateQuery((int)type) + "=" + year.ToString()).ExecuteAsync(new RestRequest("", Method.Get)).Result;

                                    dynamic responseContent = mediaType.getResponce((int)type, response.Content);

                                    if (responseContent.TotalResults > 0)
                                    {
                                        string closest_index = string.Empty;

                                        if (responseContent.TotalResults == 1)
                                            closest_index = "0";
                                        else
                                        {

                                            double? most_match = 0.0;
                                            foreach (dynamic result in responseContent.Results)
                                            {
                                                var title = string.Empty;
                                                var year2 = string.Empty;
                                                if (type == mediaTypes.collection)
                                                    title = result.Name;
                                                else if (type == mediaTypes.movie)
                                                {
                                                    title = result.Title;
                                                    try { year2 = result.ReleaseDate.Year.ToString(); } catch (Exception) { }
                                                }
                                                else if (type == mediaTypes.tv)
                                                {
                                                    title = result.OriginalName;
                                                    try { year2 = result.FirstAirDate.Year.ToString(); } catch (Exception) { }
                                                }
                                                if (year == 0 || year.ToString() == year2)
                                                {
                                                    title = title.Replace(".", " ");
                                                    title = Regex.Replace(title, @"[^0-9a-zA-Z\s&一-龯ぁ-んァ-ン\w！：／・]", string.Empty);
                                                    title = new Regex("[ ]{2,}", RegexOptions.None).Replace(title, " ");

                                                    double match_case = CalculateSimilarity(title.ToLower(), name.ToLower());
                                                    //Log(title.ToLower() + " := " + name.ToLower() + " with " + match_case.ToString(), "Msg");
                                                    if (match_case > most_match)
                                                    {
                                                        closest_index = responseContent.Results.IndexOf(result).ToString();
                                                        most_match = match_case;
                                                    }
                                                }
                                            }
                                        }
                                        try
                                        {
                                            if (closest_index != string.Empty)
                                            {
                                                if (type == mediaTypes.tv)
                                                {
                                                    // Set your Apikey
                                                    if (s_number > 0)
                                                    {
                                                        //main.Log("https://api.themoviedb.org/3/tv/" + responseContent.Results[Convert.ToInt32(closest_index)].Id + "/season/" + s_number + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94", msgType.warning);
                                                        RestResponse seasonResponse = new RestClient("https://api.themoviedb.org/3/tv/" + responseContent.Results[Convert.ToInt32(closest_index)].Id + "/season/" + s_number + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94").ExecuteAsync(new RestRequest("", Method.Get)).Result;
                                                        try
                                                        {
                                                            dynamic seasonResponseContent = Newtonsoft.Json.Linq.JObject.Parse(seasonResponse.Content);
                                                            mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + seasonResponseContent.poster_path).GetResponse().GetResponseStream());
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            main.log(ex.ToString(), msgType.error);
                                                            mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());
                                                        }

                                                    }
                                                    else
                                                        mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());

                                                    main.log("Poster Found.", msgType.success);
                                                    goto posterFound;
                                                }
                                                else
                                                {
                                                    mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());
                                                    main.log("Poster Found.", msgType.success);
                                                    goto posterFound;
                                                }
                                            }
                                        }
                                        catch (Exception) { }
                                    }

                                    if (name.Contains(" "))
                                        name = name.Substring(0, name.LastIndexOf(" "));
                                    else
                                        name = string.Empty;

                                    main.update_progressbar(1, main.Mini_ProgressBar);
                                }
                            }
                        }
                    posterFound:
                        if (mainBitmap != null)
                        {
                            Bitmap convertedBitmap = new Bitmap(256, 256);

                            double scale = mainBitmap.Height / 256.0;

                            var width = (int)(mainBitmap.Width / scale);
                            var height = (int)(mainBitmap.Height / scale);

                            var destRect = new Rectangle(0, 0, width, height);
                            var destImage = new Bitmap(width, height);

                            using (var graphics = Graphics.FromImage(destImage))
                            {
                                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                                using (var wrapMode = new ImageAttributes())
                                {
                                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                                    graphics.DrawImage(mainBitmap, destRect, 0, 0, mainBitmap.Width, mainBitmap.Height, GraphicsUnit.Pixel, wrapMode);
                                }
                            }

                            convertedBitmap.MakeTransparent(System.Drawing.Color.Transparent);
                            Graphics g = Graphics.FromImage(convertedBitmap);
                            g.DrawImage(destImage, new Point((256 - destImage.Width) / 2, 0));
                            SaveAsIcon(convertedBitmap, directory.FullName + @"\folder.ico");

                            File.WriteAllLines(iniPath, new String[] { "[.ShellClassInfo]", "IconFile=" + directory.FullName + @"\folder.ico,0", "[ViewState]", "Mode=", "Vid=", "FolderType=Videos" });

                            hideFile(iniPath);
                            hideFile(icoPath);

                            SHFOLDERCUSTOMSETTINGS folderSettings = new SHFOLDERCUSTOMSETTINGS
                            {
                                dwMask = FolderCustomSettingsMask.IconFile,
                                pszIconFile = "folder.ico",
                                iIconIndex = 0
                            };

                            SHGetSetFolderCustomSettings(ref folderSettings, directory.FullName, FolderCustomSettingsRW.ForceWrite);

                            main.log("Done.", msgType.success);
                        }
                        else
                            main.log("No Posters Found.", msgType.error);

                        main.update_progressbar(main.Mini_ProgressBar.Maximum, main.Mini_ProgressBar);
                    }

                    main.increment_progressbar(1, main.ProgressBar);
                }
                main.change_status("Done", msgType.success);
                //}
            }
            catch (Exception) { }//MessageBox.Show(e.ToString());
            finally { }
        }

        public static void SetFileThumbnail(string the_file = "")
        {
            Task set_file_thumbnail = Task.Run(() => setFileThumbnail(the_file));
            set_file_thumbnail.Wait();
        }

        private static void setFileThumbnail(string the_file = "")
        {
            try
            {
                //mediaTypes type;
                List<FileInfo> files = new List<FileInfo>();
                if (the_file != "")
                {
                    files.Add(new FileInfo(the_file));
                    goto dataProvided;
                }

                using (OpenFileDialog Open_File_Dialog = main.get_openfiledialog())
                {
                    if (Open_File_Dialog == null)
                        return;

                    files.Add(new FileInfo(Open_File_Dialog.FileName));
                }

            dataProvided:
                main.max_progressbar(200, main.ProgressBar);
                main.update_progressbar(0, main.ProgressBar);

                foreach (FileInfo fileInfo in files)
                {
                    Bitmap mainBitmap = null;

                    string extension = fileInfo.Extension;

                    string name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    List<int> years = new List<int>();

                    removeFromName<quality>(ref name);

                    foreach (Match match in new Regex(@"\d{4}").Matches(name))
                        years.Add(Convert.ToInt32(match.Value));

                    years.Sort((a, b) => b.CompareTo(a));

                    try { name = Regex.Replace(name, $@"\b{years[0]}\b", string.Empty); } catch (Exception) { }

                    if (years.Count() == 0)
                        years.Add(0);

                    string folderName = name;

                    name = name.Replace(".", " ");

                    removeFromName<audio_codec>(ref name);
                    removeFromName<encoder>(ref name);
                    removeFromName<source>(ref name);
                    removeFromName<video_codec>(ref name);
                    //removeFromName<languageDB>(ref name);

                    name = Regex.Replace(name, @"[^0-9a-zA-Z\s&一-龯ぁ-んァ-ン\w！：／・]", string.Empty);
                    name = new Regex("[ ]{2,}", RegexOptions.None).Replace(name, " ");
                    name = name.ToLower();

                    removeFromName<audio_channel>(ref name);

                    main.log("Simplified name:= " + name, msgType.message);

                    main.max_progressbar(name.Count(f => (f == ' ')), main.Mini_ProgressBar);
                    main.update_progressbar(0, main.Mini_ProgressBar);

                    while (name != string.Empty)
                    {
                        foreach (int year in years)
                        {

                            RestResponse response = null;
                            response = new RestClient("https://api.themoviedb.org/3/search/movie?api_key=9a49cbab6d640fd9483fbdd2abe22b94&query=" + System.Web.HttpUtility.UrlEncode(name) + "&page=1&include_adult=true&year=" + year.ToString()).ExecuteAsync(new RestRequest("", Method.Get)).Result;

                            TMDB_movie responseContent = JsonConvert.DeserializeObject<TMDB_movie>(response.Content);

                            if (responseContent.TotalResults > 0)
                            {
                                string closest_index = string.Empty;

                                if (responseContent.TotalResults == 1)
                                    closest_index = "0";
                                else
                                {

                                    double? most_match = 0.0;

                                    foreach (TMDB_movie.Result result in responseContent.Results)
                                    {
                                        var title = result.OriginalTitle;
                                        var year2 = result.ReleaseDate.ToString().Substring(0, 4);

                                        if (year == 0 || year.ToString() == year2)
                                        {
                                            //title = title.Replace(".", " ");
                                            //title = Regex.Replace(title, @"[^0-9a-zA-Z\s&一-龯ぁ-んァ-ン\w！：／・]", string.Empty);
                                            //title = new Regex("[ ]{2,}", RegexOptions.None).Replace(title, " ");

                                            double match_case = CalculateSimilarity(title.ToLower(), name.ToLower());
                                            main.log(match_case.ToString(), msgType.message);
                                            //Log(title.ToLower() + " := " + name.ToLower() + " with " + match_case.ToString(), "Msg");
                                            if (match_case > most_match)
                                            {
                                                closest_index = responseContent.Results.IndexOf(result).ToString();
                                                most_match = match_case;
                                            }
                                        }
                                    }
                                }

                                main.update_progressbar(1, main.Mini_ProgressBar);
                                main.update_progressbar(100 / main.Mini_ProgressBar.Maximum, main.ProgressBar);
                                try
                                {
                                    if (closest_index != string.Empty)
                                    {
                                        main.log("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath, msgType.message);
                                        mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());
                                        main.log("Poster Found.", msgType.success);
                                        main.update_progressbar(100, main.ProgressBar);
                                        goto posterFound;
                                    }
                                }
                                catch (Exception) { }
                            }

                            if (name.Contains(" "))
                                name = name.Substring(0, name.LastIndexOf(" "));
                            else
                                name = string.Empty;
                        }
                    }

                posterFound:
                    if (mainBitmap != null)
                    {
                        mainBitmap.Save(fileInfo.DirectoryName + @"\cover.png", ImageFormat.Png);

                        string mkv_command;
                        if (fileInfo.Extension != ".mkv")
                        {
                            main.max_progressbar(100, main.Mini_ProgressBar);
                            main.update_progressbar(0, main.Mini_ProgressBar);
                            MKVToolNix.currentIndex = 1;
                            if (File.Exists(fileInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + @".srt"))
                                mkv_command = @"""" + Application.StartupPath + @"\MKVToolNix\/c mkvmerge.exe --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + @".mkv^"" --language 0:und --language 1:und ^""^(^"" ^""" + fileInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + "" + fileInfo.Extension + @"^"" ^""^)^"" --language 0:en --track-name ^""0:English Subtitle ^"" --default-track 0:yes --forced-track 0:yes ^""^(^"" ^""" + fileInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + @".srt ^"" ^""^)^"" --track-order 0:0,0:1,1:0 --attachment-name cover.png --attachment-mime-type image/png --attach-file """ + fileInfo.DirectoryName + @"\cover.png""";
                            else
                                mkv_command = @"""" + Application.StartupPath + @"\MKVToolNix\/c mkvmerge.exe --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + @".mkv^"" --language 0:und --language 1:und ^""^(^"" ^""" + fileInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + "" + fileInfo.Extension + @"^"" ^""^)^"" --language 0:en --track-order 0:0,0:1,1:0 --attachment-name cover.png --attachment-mime-type image/png --attach-file """ + fileInfo.DirectoryName + @"\cover.png""";

                            main.log("Creating MKV file with a cover from the given file.", msgType.message);
                            runmkvToolNixCommands(ref mkv_command, true);
                        }
                        else
                        {
                            main.max_progressbar(2, main.Mini_ProgressBar);

                            mkv_command = @"""" + fileInfo.FullName + @""" --delete-attachment mime-type:image/png""";
                            main.log("Deleting all the cover attachment if any.", msgType.message);
                            runmkvToolNixCommands(ref mkv_command);
                            main.update_progressbar(1, main.Mini_ProgressBar);
                            main.update_progressbar(150, main.ProgressBar);

                            mkv_command = @"""" + fileInfo.FullName + @""" --attachment-name cover.png --attachment-mime-type image/png --add-attachment """ + fileInfo.DirectoryName + @"\cover.png""";
                            main.log("Adding cover to Movie --> " + fileInfo.Name + "... ", msgType.message);
                            runmkvToolNixCommands(ref mkv_command);
                            main.update_progressbar(2, main.Mini_ProgressBar);
                            main.update_progressbar(200, main.ProgressBar);
                        }

                        File.Delete(fileInfo.DirectoryName + @"\cover.png");
                        File.Delete(fileInfo.FullName);
                        File.Move(Properties.Settings.Default.merge_destination + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + @".mkv", fileInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fileInfo.Name) + @".mkv");
                        main.log("Done.", msgType.success);
                    }
                    else
                        main.log("No Posters Found.", msgType.error);

                    main.update_progressbar(main.Mini_ProgressBar.Maximum, main.Mini_ProgressBar);
                }
                //}
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            finally { }
        }

        private static void hideFile(String path)
        {
            // Set ini file attribute to "Hidden"
            if ((File.GetAttributes(path) & FileAttributes.Hidden) != FileAttributes.Hidden)
                File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);

            // Set ini file attribute to "System"
            if ((File.GetAttributes(path) & FileAttributes.System) != FileAttributes.System)
                File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.System);
        }

        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        // From: https://stackoverflow.com/a/11448060/368354
        public static void SaveAsIcon(Bitmap SourceBitmap, string FilePath)
        {
            Size original_size = new Size(SourceBitmap.Width, SourceBitmap.Height);
            int maxSize = 256;
            float percent = (new List<float> { (float)maxSize / (float)original_size.Width, (float)maxSize / (float)original_size.Height }).Min();
            Size resultSize = new Size((int)Math.Floor(original_size.Width * percent), (int)Math.Floor(original_size.Height * percent));
            if (resultSize.Height >= 256) { resultSize.Height = 0; }
            if (resultSize.Width >= 256) { resultSize.Width = 0; }


            FileStream FS = new FileStream(FilePath, FileMode.Create);
            // ICO header
            FS.WriteByte(0); FS.WriteByte(0);
            FS.WriteByte(1); FS.WriteByte(0);
            FS.WriteByte(1); FS.WriteByte(0);

            // Image size
            // Set to 0 for 256 px width/height
            //MessageBox.Show(resultSize.Width.ToString());
            FS.WriteByte(Convert.ToByte(resultSize.Width));
            FS.WriteByte(Convert.ToByte(resultSize.Height));
            // Palette
            FS.WriteByte(0);
            // Reserved
            FS.WriteByte(0);
            // Number of color planes
            FS.WriteByte(1); FS.WriteByte(0);
            // Bits per pixel
            FS.WriteByte(32); FS.WriteByte(0);

            // Data size, will be written after the data
            FS.WriteByte(0);
            FS.WriteByte(0);
            FS.WriteByte(0);
            FS.WriteByte(0);

            // Offset to image data, fixed at 22
            FS.WriteByte(22);
            FS.WriteByte(0);
            FS.WriteByte(0);
            FS.WriteByte(0);

            // Writing actual data
            SourceBitmap.Save(FS, System.Drawing.Imaging.ImageFormat.Png);

            // Getting data length (file length minus header)
            long Len = FS.Length - 22;

            // Write it in the correct place
            FS.Seek(14, SeekOrigin.Begin);
            FS.WriteByte((byte)Len);
            FS.WriteByte((byte)(Len >> 8));
            FS.WriteByte((byte)(Len >> 16));
            FS.WriteByte((byte)(Len >> 24));

            FS.Close();
        }
        private static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
        private static double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = Compute(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
        private static void removeFromName<T>(ref string name)
        {
            foreach (dynamic x in (List<T>)database.getObjectFromDatabase<T>())
            {
                name = Regex.Replace(name, $@"\b{x.name}\b", string.Empty);
            }
        }

        private static void runmkvToolNixCommands(ref string commands, bool merge = false)
        {
            Process p;
            try
            {
                if (merge)
                    p = Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = commands, RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden });
                else
                    p = Process.Start(new ProcessStartInfo() { FileName = Application.StartupPath + @"\MKVToolNix\mkvpropedit.exe", Arguments = commands, RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden });


                p.OutputDataReceived += new DataReceivedEventHandler(MKVToolNix.process_OutputDataReceived);
                p.BeginOutputReadLine();
                do
                {
                } while (!p.HasExited);
                p.WaitForExit();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); };
        }
    }
}
