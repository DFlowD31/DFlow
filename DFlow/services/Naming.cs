using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace LazyPortal.services
{
    public static class Naming
    {
        private static readonly Main main = Program.Main_Form;
        private static int rename_episode_index = 1;
        private static int sub_index = 0;
        #region Music Renamer

        public static async void RenameMusic()
        {
            await Task.Run(() => renameMusic());
        }
        private static void renameMusic()
        {
            FolderBrowserDialog Folder_Browser_Dialog = main.get_folderbrowserdialog();
            if (Folder_Browser_Dialog != null)
            {
                int fileCount = Directory.GetFiles(Folder_Browser_Dialog.SelectedPath, "*.mp3").Count();
                int currentCount = 0;
                main.max_progressbar(fileCount, main.ProgressBar);
                main.update_progressbar(0, main.ProgressBar);

                main.change_status("0/" + fileCount, msgType.message);

                foreach (string file in Directory.GetFiles(Folder_Browser_Dialog.SelectedPath, "*.mp3"))
                {
                    try
                    {
                        using (TagLib.File mp3 = TagLib.File.Create(file))
                        {
                            string fileName = string.Empty;
                            if (mp3.Tag.AlbumArtists.Length > 0 || (mp3.Tag.AlbumArtists.Length < 0 && mp3.Tag.JoinedPerformers.Length > 0))
                            {
                                List<string> cArtists = new List<string>();
                                if (mp3.Tag.JoinedPerformers.Contains('/'))
                                    cArtists = mp3.Tag.JoinedPerformers.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                else if (mp3.Tag.JoinedPerformers.Contains(';'))
                                    cArtists = mp3.Tag.JoinedPerformers.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                else if (mp3.Tag.JoinedPerformers.Length > 0)
                                    cArtists.Add(mp3.Tag.JoinedPerformers);

                                if (mp3.Tag.AlbumArtists.Length > 0)
                                    fileName = mp3.Tag.AlbumArtists[0];
                                foreach (string cArtist in cArtists)
                                {
                                    switch (cArtists.IndexOf(cArtist))
                                    {
                                        case 0:
                                            if (string.IsNullOrEmpty(fileName))
                                                fileName = cArtist;
                                            else
                                                fileName += " ft. " + cArtist;
                                            break;
                                        case 1:
                                            fileName += " & " + cArtist; break;
                                        default:
                                            fileName += ", " + cArtist; break;
                                    }
                                }
                                fileName += " - "; ;
                            }

                            fileName += mp3.Tag.Title;

                            try
                            {
                                File.Move(file, Folder_Browser_Dialog.SelectedPath + "/" + fileName + ".mp3");
                                main.log("Renamed --> " + fileName, msgType.message);
                            }
                            catch (Exception e)
                            {
                                if (e.HResult == -2146232800)
                                    main.log("File Exists --> " + fileName, msgType.warning);
                                else
                                    main.log("Couldn't Rename --> " + fileName + Environment.NewLine + e.ToString(), msgType.error);
                            }
                        }
                        currentCount += 1;
                        main.change_status(currentCount + "/" + fileCount, msgType.message);
                        main.update_progressbar(main.get_progressbar_value(main.ProgressBar) + 1, main.ProgressBar);
                    }
                    catch (Exception ex) { main.log(file + Environment.NewLine + "\t" + ex.ToString(), msgType.error); }
                }
                main.change_status("Done", msgType.success);
            }
        }

        #endregion

        #region Anime Renamer

        public static async void RenameAnime()
        {
            await Task.Run(() => renameAnime());
        }

        private static void renameAnime()
        {
            try
            {
                FolderBrowserDialog Folder_Browser_Dialog = main.get_folderbrowserdialog();
                if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                {
                    int rename_episode_index = 1;
                    List<string> The_List = new List<string>();
                    List<double> The_Episodes = new List<double>();
                    List<string> The_Files = new List<string>();
                    List<string> The_Folders = new List<string>();
                    bool To_Continue = false;
                    string aLine = "";
                    StringReader strReader = null;
                    string Anime_ID = "";
                    int Season_Count = 1;
                    int s = 0;
                    if (database.Get_From_Database("select exists(select `English Name` from `Anime` where `English Name`='" + Folder_Browser_Dialog.SelectedPath.Substring(Folder_Browser_Dialog.SelectedPath.LastIndexOf(@"\") + 1) + "' limit 1)") == "1")
                    {
                        using (language_choice languageChoice = new language_choice())
                        {
                            if (languageChoice.ShowDialog() == DialogResult.OK)
                            {
                                Anime_ID = database.Get_From_Database("SELECT `ID` FROM `Anime` WHERE `English Name`='" + Folder_Browser_Dialog.SelectedPath.Substring(Folder_Browser_Dialog.SelectedPath.LastIndexOf(@"\") + 1) + "'");
                                To_Continue = true;
                            }
                            else
                                To_Continue = false;
                        }
                    }
                    else if (database.Get_From_Database("select exists(select `English Name` from `Anime` where `Japanese Name`='" + Folder_Browser_Dialog.SelectedPath.Substring(Folder_Browser_Dialog.SelectedPath.LastIndexOf(@"\") + 1) + "' limit 1)") == "1")
                    {
                        using (language_choice languageChoice = new language_choice())
                        {
                            if (languageChoice.ShowDialog() == DialogResult.OK)
                            {
                                Anime_ID = database.Get_From_Database("SELECT `ID` FROM `Anime` WHERE `Japanese Name`='" + Folder_Browser_Dialog.SelectedPath.Substring(Folder_Browser_Dialog.SelectedPath.LastIndexOf(@"\") + 1) + "'");
                                To_Continue = true;
                            }
                            else
                                To_Continue = false;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Could Not Recognize From The Folder Name!!!" + Environment.NewLine + "Want To Add This Anime To The Database?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            using (anime_record animeRecord = new anime_record())
                                animeRecord.ShowDialog();
                        }
                        else
                            To_Continue = false;
                    }
                    if (To_Continue)
                    {
                        Season_Count = Convert.ToInt32(database.Get_From_Database("SELECT COUNT(*) FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "'"));
                        using (FileStream fs = File.Create(Application.StartupPath + "\anime.bat"))
                        { fs.Close(); }
                        int fileCount = Directory.GetFiles(Folder_Browser_Dialog.SelectedPath, "*.*", SearchOption.AllDirectories).Count();

                        main.max_progressbar(fileCount * 100, main.ProgressBar);
                        main.max_progressbar(100, main.Mini_ProgressBar);

                        main.change_status("0/" + fileCount, msgType.message);

                        try
                        {
                            The_Folders = Directory.GetDirectories(Folder_Browser_Dialog.SelectedPath).ToList();
                            if (The_Folders.Contains(Folder_Browser_Dialog.SelectedPath + @"\Movies"))
                            {
                                MessageBox.Show("Have");
                                The_Folders.Remove(Folder_Browser_Dialog.SelectedPath + @"\Movies");
                            }
                            else if (The_Folders.Contains(Folder_Browser_Dialog.SelectedPath + @"\劇場版"))
                                The_Folders.Remove(Folder_Browser_Dialog.SelectedPath + @"\劇場版");
                            The_Folders.Sort(new MyComparer());
                            if (The_Folders.Count <= 0 && Season_Count > 1)
                            {
                                The_Files = Directory.GetFiles(Folder_Browser_Dialog.SelectedPath).ToList();
                                The_Files.Sort(new MyComparer());
                                int k = 0;
                                string thedr = "";
                                int thecount = 0;
                                for (int j = 1; j <= Season_Count; j++)
                                {
                                    thedr = database.Get_From_Database("SELECT `Season " + choice.chosenLanguage + " Name` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + j + "'");
                                    Directory.CreateDirectory(Folder_Browser_Dialog.SelectedPath + @"\" + thedr);
                                    thecount = Convert.ToInt32(database.Get_From_Database("SELECT `Episode Count` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + j + "'"));
                                    thecount += k;
                                    while (k < thecount)
                                    {
                                        main.log(The_Files[k].ToString() + " --> " + Folder_Browser_Dialog.SelectedPath + @"\" + thedr + @"\" + The_Files[k].Substring(The_Files[k].LastIndexOf(@"\") + 1), msgType.message);
                                        File.Move(The_Files[k].ToString(), Folder_Browser_Dialog.SelectedPath + @"\" + thedr + @"\" + The_Files[k].Substring(The_Files[k].LastIndexOf(@"\") + 1));
                                        k += 1;
                                    }
                                    main.log(Environment.NewLine, msgType.message);
                                }
                                The_Folders = Directory.GetDirectories(Folder_Browser_Dialog.SelectedPath).ToList();
                                The_Folders.Sort(new MyComparer());
                            }
                        }
                        catch (Exception) { }
                        for (int i = 1; i <= Season_Count; i++)
                        {
                            int The_Episode_Index = 0;
                            The_List.Clear();
                            int index1 = Convert.ToInt32(Interaction.InputBox("Set starting episode number", "Set starting episode number", rename_episode_index.ToString()));
                            rename_episode_index = index1;
                            //The_List.Clear();
                            strReader = new StringReader(database.Get_From_Database("SELECT `Episodes " + choice.chosenLanguage + "` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + i + "'"));
                            while (true)
                            {
                                aLine = strReader.ReadLine();
                                if (aLine == null)
                                    break;
                                else
                                    The_List.Add(aLine);
                            }
                            strReader.Close();
                            strReader = new StringReader(database.Get_From_Database("SELECT `Episode Numbers` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + i + "'"));
                            The_Episodes.Clear();
                            while (true)
                            {
                                aLine = strReader.ReadLine();
                                if (aLine == null)
                                    break;
                                else
                                    The_Episodes.Add(Convert.ToDouble(aLine));
                            }
                            strReader.Close();
                            try
                            {
                                string filepath = "";
                                string fileext = "";
                                string episode_index = "";
                                if (Season_Count == 1)
                                {
                                    The_Files = Directory.GetFiles(Folder_Browser_Dialog.SelectedPath).ToList();
                                    The_Files.Sort(new MyComparer());
                                }
                                else
                                {
                                    The_Files = Directory.GetFiles(The_Folders[i - 1].ToString()).ToList();
                                    The_Files.Sort(new MyComparer());
                                }
                                foreach (string FileName in The_Files)
                                {
                                    filepath = FileName.Substring(0, FileName.LastIndexOf(@"\"));
                                    fileext = FileName.Substring(FileName.LastIndexOf("."));
                                    if (rename_episode_index < 10)
                                        episode_index = "0" + rename_episode_index;
                                    else
                                        episode_index = rename_episode_index.ToString();
                                    try
                                    {
                                        string Episode_Number_String = "";
                                        if (The_Episodes[The_Files.IndexOf(FileName)] % 1 == 0)
                                            Episode_Number_String = The_Episodes[The_Files.IndexOf(FileName)].ToString("00");
                                        else
                                            Episode_Number_String = The_Episodes[The_Files.IndexOf(FileName)].ToString("00.0");
                                        if (choice.chosenLanguage == language.english)
                                        {
                                            File.Move(FileName, "Episode " + Episode_Number_String + fileext);
                                            main.log("Renaming :  " + FileName + "   -->   " + "Episode " + Episode_Number_String + fileext, msgType.message);
                                        }
                                        else if (choice.chosenLanguage == language.japanese)
                                        {
                                            File.Move(FileName, "第 " + Episode_Number_String + " 話" + fileext);
                                            main.log("Renaming :  " + FileName + "   -->   " + "第 " + Episode_Number_String + " 話" + fileext, msgType.message);
                                        }
                                    }
                                    catch (Exception) { }
                                    using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\anime.bat"))
                                    {
                                        if (choice.chosenLanguage == language.english)
                                            sw.WriteLine("chcp 65001" + Environment.NewLine + @"""" + Application.StartupPath + @"\mkvpropedit.exe"" """ + filepath + @"\" + "Episode " + episode_index + fileext + @"""--edit info--set ""title = " + The_List[The_Episode_Index] + @"""" + Environment.NewLine);
                                        else if (choice.chosenLanguage == language.japanese)
                                            sw.WriteLine("chcp 65001" + Environment.NewLine + @"""" + Application.StartupPath + @"\mkvpropedit.exe"" """ + filepath + @"\" + "第 " + episode_index + " 話" + fileext + @"""--edit info--set ""title = " + The_List[The_Episode_Index] + @"""" + Environment.NewLine);
                                    }
                                    ProcessStartInfo p = new ProcessStartInfo() { UseShellExecute = false, RedirectStandardOutput = true, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden, FileName = Application.StartupPath + @"\anime.bat" };
                                    Process bat = Process.Start(p);
                                    bat.WaitForExit();
                                    s += 1;
                                    main.change_status(s + "/" + fileCount, msgType.message);
                                    rename_episode_index += 1;
                                    The_Episode_Index += 1;
                                }
                            }
                            catch (Exception ex2)
                            {
                                main.log(ex2.ToString(), msgType.error);
                            }
                            try
                            {
                                Directory.Move(The_Folders[i - 1].ToString(), database.Get_From_Database("SELECT `Season " + choice.chosenLanguage + " Name` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + i + "'"));
                            }
                            catch (Exception) { }
                        }
                        //File.Delete(Application.StartupPath & "\anime.bat")
                        main.update_progressbar(0, main.ProgressBar);
                        main.update_progressbar(0, main.Mini_ProgressBar);
                        main.change_status("All Done", msgType.success);
                        rename_episode_index = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                main.log(ex.ToString(), msgType.error);
            }
        }

        #endregion

        #region Subtitle Renamer

        public static async void RenameSub()
        {
            await Task.Run(() => renameSub());
        }

        private static void renameSub()
        {
            main.change_status("", msgType.message);

            bool is_series = false;
            bool have_seasons = false;
            bool get_from_text_file = false;
            FolderBrowserDialog Folder_Browser_Dialog = main.get_folderbrowserdialog();
            if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo d = new DirectoryInfo(Folder_Browser_Dialog.SelectedPath);

                if (MessageBox.Show("Is it", "Series?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    is_series = true;
                    main.max_progressbar(d.GetFiles("*", SearchOption.AllDirectories).Count() / 2, main.ProgressBar);
                    if (MessageBox.Show("Does it", "Have Seasons?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        have_seasons = true;

                    if (MessageBox.Show("Should I", "Get names from text files?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        get_from_text_file = true;
                }
                else
                    main.max_progressbar(d.GetFiles("*", SearchOption.AllDirectories).Count(), main.ProgressBar);
 
                if (have_seasons)
                    foreach (DirectoryInfo directory in d.GetDirectories())
                        Rename_Sub(directory, is_series, get_from_text_file);
                else
                    Rename_Sub(d, is_series, get_from_text_file);
            }

            main.change_status("Done", msgType.success);
        }

        private static void Rename_Sub(DirectoryInfo d, bool is_series, bool get_from_text_file)
        {
            List<string> movie_names = new List<string>();
            foreach (FileInfo FileName in d.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (!FileName.Name.Contains(".srt"))
                    movie_names.Add(FileName.Name.Substring(0, FileName.Name.LastIndexOf(".")));

                string filepath = FileName.FullName.Substring(0, FileName.FullName.LastIndexOf(@"\"));
                string fileext = FileName.FullName.Substring(FileName.FullName.LastIndexOf("."));
                if (!is_series)
                {
                    string filenew = filepath.Substring(filepath.LastIndexOf(@"\") + 1);
                    try
                    {
                        File.Move(FileName.FullName, filenew + fileext);
                        main.log("Renamed --> " + filenew + fileext, msgType.message);
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == -2146232800)
                            main.log("File Exists --> " + filenew + fileext, msgType.warning);
                        else
                            main.log("Couldn't Rename --> " + filenew + fileext + Environment.NewLine + "\t" + ex.ToString(), msgType.error);
                    }
                    main.increment_progressbar(1, main.ProgressBar);
                    main.change_status(main.ProgressBar.Value + "/" + main.ProgressBar.Maximum, msgType.message);
                }
            }
            if (is_series)
            {
                if (get_from_text_file)
                {
                    movie_names.Clear();
                    List<string> Names_From_Text = new List<string>();
                    foreach (FileInfo files in d.GetFiles("*.txt", SearchOption.AllDirectories))
                    {
                        StreamReader sr = new StreamReader(files.FullName);
                        while (sr.Peek() >= 0)
                        {
                            Names_From_Text.Add(sr.ReadLine());
                        }
                        sr.Close();
                    }
                    int index1 = Convert.ToInt32(Interaction.InputBox("Set starting episode number", "Set starting episode number", rename_episode_index.ToString()));
                    rename_episode_index = index1;
                    string episode_index;
                    string vid_path;
                    string vid_ext;
                    int index = 0;
                    foreach (FileInfo VidFiles in d.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        if (!VidFiles.Name.Contains("*.srt") && !VidFiles.Name.Contains("*.txt"))
                        {
                            if (index1 < 10)
                                episode_index = "0" + index1;
                            else
                                episode_index = index1.ToString();
                            vid_path = VidFiles.FullName.Substring(0, VidFiles.FullName.LastIndexOf(@"\"));
                            vid_ext = VidFiles.FullName.Substring(VidFiles.FullName.LastIndexOf("."));
                            try
                            {
                                File.Move(VidFiles.FullName, "Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext);
                                main.log("Renamed --> " + "Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext, msgType.message);
                                movie_names.Add("Episode " + episode_index + " - " + Names_From_Text[index]);
                            }
                            catch (Exception ex)
                            {
                                if (ex.HResult == -2146232800)
                                    main.log("File Exists --> " + "Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext, msgType.warning);
                                else
                                    main.log("Coulnd't Rename --> Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext + Environment.NewLine + "\t" + ex.ToString(), msgType.error);
                            }
                            main.increment_progressbar(1, main.ProgressBar);
                            index1 += 1;
                            rename_episode_index += 1;
                            index += 1;
                        }
                    }
                }
                foreach (FileInfo SubFiles in d.GetFiles("*.srt", SearchOption.AllDirectories))
                {
                    //string filepath = SubFiles.FullName.Substring(0, SubFiles.FullName.LastIndexOf(@"\"));
                    try
                    {
                        File.Move(SubFiles.FullName, movie_names[sub_index] + ".srt");
                        main.log("Renamed --> " + movie_names[sub_index] + ".srt", msgType.message);
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == -2146232800)
                            main.log("File Exists --> " + movie_names[sub_index] + ".srt", msgType.warning);
                        else
                            main.log("Coulnd't Rename --> " + movie_names[sub_index] + ".srt" + Environment.NewLine + "\t" + ex.ToString(), msgType.error);
                    }
                    main.increment_progressbar(1, main.ProgressBar);
                    main.change_status(main.ProgressBar.Value + "/" + main.ProgressBar.Maximum, msgType.message);
                    sub_index += 1;
                }
            }
        }

        #endregion
    }
}
