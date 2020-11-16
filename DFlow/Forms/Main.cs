using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Microsoft.VisualBasic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;

namespace DFlow
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "<Pending>")]
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public static string chosen_language = "";
        public static string inputed_text = "";
        //public static List<string> Catch_Error_Message = new List<string>();
        readonly List<string> movie_names = new List<string>();
        bool is_series = false;
        int sub_index = 0;
        bool have_seasons = false;
        bool get_from_text_file = false;
        int rename_episode_index = 1;
        int s = 0;
        readonly database database = new database();
        public static List<anime> Animes = new List<anime>();
        public static List<anime_episode> Anime_Episodes = new List<anime_episode>();
        public static List<anime_season> Anime_Seasons = new List<anime_season>();
        public static List<anime_movie> Anime_Movies = new List<anime_movie>();
        public static List<audio_channel> Audio_Channels = new List<audio_channel>();
        public static List<audio_codec> Audio_Codecs = new List<audio_codec>();
        public static List<encoder> Encoders = new List<encoder>();
        public static List<movie> Movies = new List<movie>();
        public static List<quality> Qualities = new List<quality>();
        public static List<source> Sources = new List<source>();
        public static List<tv_series> Tv_Series = new List<tv_series>();
        public static List<tv_series_episode> Tv_Series_Episodes = new List<tv_series_episode>();
        public static List<tv_series_season> Tv_Series_Seasons = new List<tv_series_season>();
        public static List<video_codec> Video_Codecs = new List<video_codec>();
        public Main()
        {
            InitializeComponent();
            Destination.Text = Properties.Settings.Default.merge_destination;
            if (database.Check_Connection())
                Log("Connection Successfull...", "Success", true, true, false);
        }

        public void Log(string str, string Type, bool no_line = false, bool first_msg = false, bool InvokeRequired = true)
        {
            try
            {
                if (InvokeRequired)
                {
                    Log_Text.BeginInvoke(new Action(() =>
                    {
                        if (!no_line && !first_msg)
                            Log_Text.AppendText(Environment.NewLine);
                        Log_Text.AppendText(str);
                        if (str.Split('\n').Length > 1)
                            Log_Text.Select(Log_Text.TextLength - str.Length + str.Split('\n').Length - 1, str.Length + str.Split('\n').Length);
                        else
                            Log_Text.Select(Log_Text.TextLength - str.Length, str.Length);
                        if (Type == "Success")
                            Log_Text.SelectionColor = Color.LimeGreen;
                        else if (Type == "Error")
                            Log_Text.SelectionColor = Color.Crimson;
                        else if (Type == "Warning")
                            Log_Text.SelectionColor = Color.DarkOrange;
                        else if (Type == "Msg")
                            Log_Text.SelectionColor = Color.Black;
                        Log_Text.ScrollToCaret();
                    }));
                }
                else
                {
                    if (!no_line && !first_msg)
                        Log_Text.AppendText(Environment.NewLine);
                    Log_Text.AppendText(str);
                    if (str.Split('\n').Length > 1)
                        Log_Text.Select(Log_Text.TextLength - str.Length + str.Split('\n').Length - 1, str.Length + str.Split('\n').Length);
                    else
                        Log_Text.Select(Log_Text.TextLength - str.Length, str.Length);
                    if (Type == "Success")
                        Log_Text.SelectionColor = Color.LimeGreen;
                    else if (Type == "Error")
                        Log_Text.SelectionColor = Color.Crimson;
                    else if (Type == "Warning")
                        Log_Text.SelectionColor = Color.DarkOrange;
                    else if (Type == "Msg")
                        Log_Text.SelectionColor = Color.Black;
                    Log_Text.ScrollToCaret();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Timer_Button_Click(object sender, EventArgs e)
        {
            Shutdown_Timer.Enabled = !Shutdown_Timer.Enabled;
            if (!Shutdown_Timer.Enabled)
                Log("Timer Started...", "Msg");
            else
                Log("Timer Stopped...", "Msg");
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void Browse_Button_Click(object sender, EventArgs e)
        {
            if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.merge_destination = Folder_Browser_Dialog.SelectedPath;
                Destination.Text = Properties.Settings.Default.merge_destination;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Date_Time_Picker.Value <= DateTime.Now)
                {
                    Shutdown_Timer.Enabled = false;
                    Process.Start("shutdown", "/s /t 0");
                }
            }
            catch (Exception) { }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Notification_Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        public void Rename_Sub(string filepath, string filenew, string fileext, DirectoryInfo d)
        {
            try
            {
                //MessageBox.Show(d.GetFiles("*.*", SearchOption.AllDirectories).Count().ToString());
                foreach (FileInfo FileName in d.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (!FileName.Name.Contains(".srt"))
                        movie_names.Add(FileName.Name.Substring(0, FileName.Name.LastIndexOf(".")));
                    filepath = FileName.FullName.Substring(0, FileName.FullName.LastIndexOf(@"\"));
                    fileext = FileName.FullName.Substring(FileName.FullName.LastIndexOf("."));
                    if (!is_series)
                    {
                        filenew = filepath.Substring(filepath.LastIndexOf(@"\") + 1);
                        try
                        {
                            File.Move(FileName.FullName, filenew + fileext);
                            Log("Renamed --> " + filenew + fileext, "Msg");
                        }
                        catch (Exception ex)
                        {
                            if (ex.HResult == -2146232800)
                                Log("File Exists --> " + filenew + fileext, "Warning");
                            else
                                Log("Couldn't Rename --> " + filenew + fileext + Environment.NewLine + "\t" + ex.ToString(), "Error");
                        }
                        ProgressBar.Value += 1;
                        Status_Text.Text = ProgressBar.Value + "/" + ProgressBar.Maximum;
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
                        string episode_index = "";
                        string vid_path = "";
                        string vid_ext = "";
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
                                    Log("Renamed --> " + "Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext, "Msg");
                                    movie_names.Add("Episode " + episode_index + " - " + Names_From_Text[index]);
                                }
                                catch (Exception ex)
                                {
                                    if (ex.HResult == -2146232800)
                                        Log("File Exists --> " + "Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext, "Warning");
                                    else
                                        Log("Coulnd't Rename --> Episode " + episode_index + " - " + Names_From_Text[index] + vid_ext + Environment.NewLine + "\t" + ex.ToString(), "Error");
                                }
                                ProgressBar.Value += 1;
                                index1 += 1;
                                rename_episode_index += 1;
                                index += 1;
                            }
                        }
                    }
                    foreach (FileInfo SubFiles in d.GetFiles("*.srt", SearchOption.AllDirectories))
                    {
                        filepath = SubFiles.FullName.Substring(0, SubFiles.FullName.LastIndexOf(@"\"));
                        try
                        {
                            File.Move(SubFiles.FullName, movie_names[sub_index] + ".srt");
                            Log("Renamed --> " + movie_names[sub_index] + ".srt", "Msg");
                        }
                        catch (Exception ex)
                        {
                            if (ex.HResult == -2146232800)
                                Log("File Exists --> " + movie_names[sub_index] + ".srt", "Warning");
                            else
                                Log("Coulnd't Rename --> " + movie_names[sub_index] + ".srt" + Environment.NewLine + "\t" + ex.ToString(), "Error");
                        }
                        ProgressBar.Value += 1;
                        Status_Text.Text = ProgressBar.Value + "/" + ProgressBar.Maximum;
                        sub_index += 1;
                    }
                }
            }
            catch (Exception ex) { Log(ex.ToString(), "Error"); }
        }

        private void Movie_Folder_Click(object sender, EventArgs e)
        {
            try
            {
                if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                {
                    DirectoryInfo d = new DirectoryInfo(Folder_Browser_Dialog.SelectedPath);
                    MessageBox.Show(d.Name);
                }
                //Status_Text.Text = "";
                //Status_Text.Visible = false;
                //Status_Text.ForeColor = Color.Black;
                //sub_index = 0;
                //movie_names.Clear();
                //if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                //{
                //    string filepath = "";
                //    string filenew = "";
                //    string fileext = "";
                //    DirectoryInfo d = new DirectoryInfo(Folder_Browser_Dialog.SelectedPath);
                //    if (MessageBox.Show(this, "Is it", "Series?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //    {
                //        is_series = true;
                //        ProgressBar.Maximum = d.GetFiles("*", SearchOption.AllDirectories).Count() / 2;
                //        if (MessageBox.Show(this, "Does it", "Have Seasons?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //            have_seasons = true;
                //        else
                //            have_seasons = false;
                //        if (MessageBox.Show(this, "Should I", "Get names from text files?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //            get_from_text_file = true;
                //        else
                //            get_from_text_file = false;
                //    }
                //    else
                //    {
                //        is_series = false;
                //        ProgressBar.Maximum = d.GetFiles("*", SearchOption.AllDirectories).Count();
                //    }
                //    rename_episode_index = 1;
                //    if (have_seasons)
                //    {
                //        foreach (DirectoryInfo directory in d.GetDirectories())
                //        {
                //            Rename_Sub(filepath, filenew, fileext, directory);
                //        }
                //    }
                //    else
                //        Rename_Sub(filepath, filenew, fileext, d);
                //}
                //Status_Text.Text = "Done";
                //Status_Text.Visible = true;
                //Status_Text.ForeColor = Color.Green;
            }
            catch (Exception ex) { Log(ex.ToString(), "Error"); }
        }
        private void Movie_File_Click(object sender, EventArgs e)
        {

        }

        private void Merge_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Merge_Button.Text != "Stop Merging")
                {
                    if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                    {
                        Merge_Background.RunWorkerAsync();
                        Merge_Button.Text = "Stop Merging";
                    }
                }
                else
                {
                    Merge_Background.CancelAsync();
                    Merge_Button.Text = "Merge Movies";
                }
            }
            catch (Exception ex) { Log(ex.ToString(), "Error"); }
        }

        private void Merge_Background_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string filepath = "";
                string filenew = "";
                string fileext = "";
                List<string> Merging_Movies = new List<string>();
                List<string> commands = new List<string>();
                List<string> commands_report = new List<string>();
                if (MessageBox.Show("Series?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    is_series = true;
                else
                    is_series = false;
                if (!is_series)
                    foreach (string FileName in Directory.GetDirectories(Folder_Browser_Dialog.SelectedPath))
                    { //\"C:/Program Files/MKVToolNix\mkvmerge.exe\"--ui - language en--output ^ \"D:\MKV Convertions\Arrietty.mkv^\"--language 0:eng--language 1: eng ^ \ "^(^\" ^ \ "F:\Movies\New Downloads\Arrietty\Arrietty.mp4^\" ^ \ "^)^\" - -language 0:eng--track - Name ^ \ "0:English Subtitles^\" - -default- track 0:yes--forced - track 0: yes ^ \ "^(^\" ^ \ "F:\Movies\New Downloads\Arrietty\Arrietty.srt^\" ^ \ "^)^\" - -track - order 0:0,0:1,1:0
                        filepath = FileName;
                        filenew = filepath.Substring(filepath.LastIndexOf(@"\") + 1);
                        commands.Add(@"""" + Application.StartupPath + @"\mkvmerge.exe"" --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + filenew + @".mkv ^ ""--language 0:eng--language 1:eng ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + @".mp4 ^ "" ^ "" ^) ^ ""--language 0:eng--track - name ^ ""0:English Subtitle ^ ""--default - track 0:yes--forced - track 0:yes ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + @".srt ^ "" ^ "" ^) ^ ""--track - order 0:0, 0:1, 1:0");
                        if (File.Exists(filepath + @"\" + filenew + ".srt"))
                            commands_report.Add("Done.");
                        else
                            commands_report.Add("Subtitle File Not Found.");
                        Merging_Movies.Add(filenew + ".mkv");
                    }
                else
                {
                    DirectoryInfo d = new DirectoryInfo(Folder_Browser_Dialog.SelectedPath);
                    foreach (FileInfo files in d.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        filepath = files.FullName.Substring(0, files.FullName.LastIndexOf(@"\"));
                        filenew = files.Name.Substring(0, files.Name.IndexOf("."));
                        fileext = files.Name.Substring(files.Name.IndexOf("."));
                        if (Properties.Settings.Default.Video_Extensions.Contains(fileext))
                        {
                            commands.Add(@"""" + Application.StartupPath + @"\mkvmerge.exe"" --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + filenew + @".mkv ^ ""--language 0:eng--language 1:eng ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + "" + fileext + @" ^ "" ^ "" ^) ^ ""--language 0:eng--track - name ^ ""0:English Subtitle ^ ""--default - track 0:yes--forced - track 0:yes ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + @".srt ^ "" ^ "" ^) ^ ""--track - order 0:0, 0:1, 1:0");
                            commands.Add(@"""" + Application.StartupPath + @"\mkvmerge.exe"" --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + filenew + @".mkv ^ ""--language 0:eng--language 1:eng ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + "" + fileext + @" ^ "" ^ "" ^) ^ ""--language 0:eng--track - name ^ ""0:English Subtitle ^ ""--default - track 0:yes--forced - track 0:yes ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + @".srt ^ "" ^ "" ^) ^ ""--track - order 0:0, 0:1, 1:0");
                            commands.Add(@"""" + Application.StartupPath + @"\mkvmerge.exe"" --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + filenew + @".mkv ^ ""--language 0:eng--language 1:eng ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + "" + fileext + @" ^ "" ^ "" ^) ^ ""--language 0:eng--track - name ^ ""0:English Subtitle ^ ""--default - track 0:yes--forced - track 0:yes ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + @".srt ^ "" ^ "" ^) ^ ""--track - order 0:0, 0:1, 1:0");
                            commands.Add(@"""" + Application.StartupPath + @"\mkvmerge.exe"" --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + filenew + @".mkv ^ ""--language 0:eng--language 1:eng ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + "" + fileext + @" ^ "" ^ "" ^) ^ ""--language 0:eng--track - name ^ ""0:English Subtitle ^ ""--default - track 0:yes--forced - track 0:yes ^ "" ^ (^ "" ^ """ + filepath + @"\" + filenew + @".srt ^ "" ^ "" ^) ^ ""--track - order 0:0,0:1,1:0");
                            if (File.Exists(filepath + @"\" + filenew + ".srt"))
                            {
                                commands_report.Add("Done.");
                                commands_report.Add("Done.");
                                commands_report.Add("Done.");
                                commands_report.Add("Done.");
                            }
                            else
                            {
                                commands_report.Add("Subtitle File Not Found.");
                                commands_report.Add("Subtitle File Not Found.");
                                commands_report.Add("Subtitle File Not Found.");
                                commands_report.Add("Subtitle File Not Found.");
                            }
                            Merging_Movies.Add(filenew + ".mkv");
                        }
                    }
                }
                using (FileStream fs = File.Create(Application.StartupPath + @"\Merge_Bat.bat"))
                {
                    fs.Close();
                }
                ProgressBar.Maximum = commands.Count * 100;
                Mini_ProgressBar.Maximum = 100;
                Status_Text.Text = "0/" + commands.Count;
                Status_Text.BeginInvoke(new MethodInvoker(Visible_Status));
                s = 0;
                foreach (string merge_command in commands)
                {
                    Log("Multiplexing Movie --> " + Merging_Movies[commands.IndexOf(merge_command)] + "... ", "Msg");
                    using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\Merge_Bat.bat"))
                    {
                        sw.WriteLine(merge_command + Environment.NewLine);
                    }
                    ProcessStartInfo p = new ProcessStartInfo() { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardInput = true, WindowStyle = ProcessWindowStyle.Hidden, FileName = Application.StartupPath + @"\Merge_Bat.bat" };
                    Process bat = Process.Start(p);
                    bat.OutputDataReceived += new DataReceivedEventHandler(proc_OutputDataReceived);
                    bat.BeginOutputReadLine();
                    do
                    {
                        try
                        {
                            if (Merge_Background.CancellationPending)
                            {
                                Process[] ProcID = Process.GetProcessesByName("mkvmerge");
                                bat.CancelOutputRead();
                                ProcID[0].Kill();
                                bat.Kill();
                                Log("Canceled by User.", "Error", true);
                                break;
                            }
                        }
                        catch (Exception) { }
                    }
                    while (!bat.HasExited);
                    bat.WaitForExit();
                    s += 1;
                    Status_Text.Text = s + "/" + commands.Count;
                    Status_Text.BeginInvoke(new MethodInvoker(Visible_Status));
                    if (commands_report[commands.IndexOf(merge_command)] == "Done.")
                        Log(commands_report[commands.IndexOf(merge_command)], "Success", true);
                    else
                        Log(commands_report[commands.IndexOf(merge_command)], "Error", true);
                }
                File.Delete(Application.StartupPath + @"\Merge_Bat.bat");
                ProgressBar.Value = 0;
                Mini_ProgressBar.Value = 0;
                Status_Text.Text = "All Done";
                Status_Text.BeginInvoke(new MethodInvoker(Visible_Status));
                Status_Text.ForeColor = Color.Green;
                Merge_Button.Text = "Merge Movies";
            }
            catch (Exception ex) { Log(ex.ToString(), "Error"); }
        }

        public void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                string n = "";
                if (e.Data.Contains("Progress"))
                {
                    n = e.Data;
                    n = n.Substring(n.LastIndexOf("Progress") + 9);
                    n = n.Replace("%", "");
                    Mini_ProgressBar.Value = Convert.ToInt32(n);
                    ProgressBar.Value = Convert.ToInt32((s * 100) + n);
                }
            }
            catch (Exception) { }
        }

        public void Visible_Status()
        {
            Status_Text.Visible = true;
        }

        private void Server_Config_Click(object sender, EventArgs e)
        {
            new movie_record().Show();
        }

        private void Anime_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                new anime_record().Show();
                //using (anime_record animeRecord = new anime_record() { Tag = database.Get_From_Database("SELECT Auto_increment FROM information_schema.tables WHERE table_name='Anime'") })
                //    animeRecord.ShowDialog();
            }
            catch (Exception) { }
        }

        private void Music_Button_Click(object sender, EventArgs e)
        {
            if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
            {
                int thecount = Directory.GetFiles(Folder_Browser_Dialog.SelectedPath, "*.mp3").Count();
                ProgressBar.Maximum = thecount;
                foreach (string file in Directory.GetFiles(Folder_Browser_Dialog.SelectedPath, "*.mp3"))
                {
                    try
                    {
                        using (TagLib.File mp3 = TagLib.File.Create(file))
                        {
                            if (mp3.Tag.AlbumArtists.Length > 0)
                            {
                                if (mp3.Tag.JoinedPerformers.Length > 0)
                                {
                                    if (!mp3.Tag.JoinedPerformers.ToString().Contains(mp3.Tag.AlbumArtists[0].ToString()))
                                    {
                                        List<string> C_Artists = new List<string>();
                                        string C_Artists_All = mp3.Tag.JoinedPerformers.ToString();
                                        while (C_Artists_All != "")
                                        {
                                            if (C_Artists_All.Contains("/"))
                                            {
                                                C_Artists.Add(C_Artists_All.Substring(0, C_Artists_All.IndexOf("/")));
                                                C_Artists_All = C_Artists_All.Substring(C_Artists_All.IndexOf("/") + 1);
                                            }
                                            else if (C_Artists_All.Contains("; "))
                                            {
                                                C_Artists.Add(C_Artists_All.Substring(0, C_Artists_All.IndexOf("; ")));
                                                C_Artists_All = C_Artists_All.Substring(C_Artists_All.IndexOf("; ") + 2);
                                            }
                                            else
                                            {
                                                C_Artists.Add(C_Artists_All);
                                                C_Artists_All = "";
                                            }
                                        }
                                        string Artists = mp3.Tag.AlbumArtists[0].ToString();
                                        for (int i = 0; i < C_Artists.Count; i++)
                                        {
                                            if (i == 1)
                                                Artists = Artists + " & " + C_Artists[i];
                                            else if (i == 0)
                                                Artists = Artists + " ft. " + C_Artists[i];
                                            else
                                                Artists = Artists + ", " + C_Artists[i];
                                        }
                                        try
                                        {
                                            File.Move(file, Folder_Browser_Dialog.SelectedPath + "/" + Artists + " - " + mp3.Tag.Title.ToString() + ".mp3");
                                            Log("Renamed --> " + Artists + " - " + mp3.Tag.Title.ToString(), "Msg");
                                        }
                                        catch (Exception ex)
                                        {
                                            if (ex.HResult == -2146232800)
                                                Log("File Exists --> " + Artists + " - " + mp3.Tag.Title.ToString(), "Warning");
                                            else
                                                Log("Couldn't Rename --> " + Artists + " - " + mp3.Tag.Title.ToString() + Environment.NewLine + ex.ToString(), "Error");
                                        }
                                        ProgressBar.Value += 1;
                                    }
                                    else
                                    {
                                        List<string> C_Artists = new List<string>();
                                        string C_Artists_All = mp3.Tag.JoinedPerformers.ToString();
                                        while (C_Artists_All != "")
                                        {
                                            if (C_Artists_All.Contains("/"))
                                            {
                                                C_Artists.Add(C_Artists_All.Substring(0, C_Artists_All.IndexOf("/")));
                                                C_Artists_All = C_Artists_All.Substring(C_Artists_All.IndexOf("/") + 1);
                                            }
                                            else if (C_Artists_All.Contains("; "))
                                            {
                                                C_Artists.Add(C_Artists_All.Substring(0, C_Artists_All.IndexOf("; ")));
                                                C_Artists_All = C_Artists_All.Substring(C_Artists_All.IndexOf("; ") + 2);
                                            }
                                            else
                                            {
                                                C_Artists.Add(C_Artists_All);
                                                C_Artists_All = "";
                                            }
                                        }
                                        string Artists = C_Artists[0].ToString();
                                        for (int i = 1; i < C_Artists.Count; i++)
                                        {
                                            if (i == 1)
                                                Artists = Artists + " & " + C_Artists[i];
                                            else
                                                Artists = Artists + ", " + C_Artists[i];
                                        }
                                        try
                                        {
                                            File.Move(file, Folder_Browser_Dialog.SelectedPath + "/" + Artists + " - " + mp3.Tag.Title.ToString() + ".mp3");
                                            Log("Renamed --> " + Artists + " - " + mp3.Tag.Title.ToString(), "Msg");
                                        }
                                        catch (Exception ex)
                                        {
                                            if (ex.HResult == -2146232800)
                                                Log("File Exists --> " + Artists + " - " + mp3.Tag.Title.ToString(), "Warning");
                                            else
                                                Log("Couldn't Rename --> " + Artists + " - " + mp3.Tag.Title.ToString() + Environment.NewLine + ex.ToString(), "Error");
                                        }
                                        ProgressBar.Value += 1;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        File.Move(file, Folder_Browser_Dialog.SelectedPath + "/" + mp3.Tag.AlbumArtists[0].ToString() + " - " + mp3.Tag.Title.ToString() + ".mp3");
                                        Log("Renamed --> " + mp3.Tag.AlbumArtists[0].ToString() + " - " + mp3.Tag.Title.ToString(), "Msg");
                                    }
                                    catch (Exception ex)
                                    {
                                        if (ex.HResult == -2146232800)
                                            Log("File Exists --> " + mp3.Tag.AlbumArtists[0].ToString() + " - " + mp3.Tag.Title.ToString(), "Warning");
                                        else
                                            Log("Couldn't Rename --> " + mp3.Tag.AlbumArtists[0].ToString() + " - " + mp3.Tag.Title.ToString() + Environment.NewLine + ex.ToString(), "Error");
                                    }
                                    ProgressBar.Value += 1;
                                }
                            }
                            else if (mp3.Tag.AlbumArtists.Length <= 0 && mp3.Tag.JoinedPerformers.Length > 0)
                            {
                                List<string> C_Artists = new List<string>();
                                string C_Artists_All = mp3.Tag.JoinedPerformers.ToString();
                                while (C_Artists_All != "")
                                {
                                    if (C_Artists_All.Contains("/"))
                                    {
                                        C_Artists.Add(C_Artists_All.Substring(0, C_Artists_All.IndexOf("/")));
                                        C_Artists_All = C_Artists_All.Substring(C_Artists_All.IndexOf("/") + 1);
                                    }
                                    else if (C_Artists_All.Contains("; "))
                                    {
                                        C_Artists.Add(C_Artists_All.Substring(0, C_Artists_All.IndexOf("; ")));
                                        C_Artists_All = C_Artists_All.Substring(C_Artists_All.IndexOf("; ") + 2);
                                    }
                                    else
                                    {
                                        C_Artists.Add(C_Artists_All);
                                        C_Artists_All = "";
                                    }
                                }
                                string Artists = C_Artists[0].ToString();
                                for (int i = 0; i < C_Artists.Count; i++)
                                {
                                    if (i == 1)
                                        Artists = Artists + " & " + C_Artists[i];
                                    else
                                        Artists = Artists + ", " + C_Artists[i];
                                }
                                try
                                {
                                    File.Move(file, Folder_Browser_Dialog.SelectedPath + "/" + Artists + " - " + mp3.Tag.Title.ToString() + ".mp3");
                                    Log("Renamed --> " + Artists + " - " + mp3.Tag.Title.ToString(), "Msg");
                                }
                                catch (Exception ex)
                                {
                                    if (ex.HResult == -2146232800)
                                        Log("File Exists --> " + Artists + " - " + mp3.Tag.Title.ToString(), "Warning");
                                    else
                                        Log("Couldn't Rename --> " + Artists + " - " + mp3.Tag.Title.ToString() + Environment.NewLine + ex.ToString(), "Error");
                                }
                                ProgressBar.Value += 1;
                            }
                            else if (mp3.Tag.AlbumArtists.Length <= 0 && mp3.Tag.JoinedPerformers.Length <= 0)
                            {
                                try
                                {
                                    File.Move(file, Folder_Browser_Dialog.SelectedPath + "/" + mp3.Tag.Title.ToString() + ".mp3");
                                    Log("Renamed --> " + mp3.Tag.Title.ToString(), "Msg");
                                }
                                catch (Exception ex)
                                {
                                    if (ex.HResult == -2146232800)
                                        Log("File Exists --> " + mp3.Tag.Title.ToString(), "Warning");
                                    else
                                        Log("Couldn't Rename --> " + mp3.Tag.Title.ToString() + Environment.NewLine + ex.ToString(), "Error");
                                }
                                ProgressBar.Value += 1;
                            }
                        }
                    }
                    catch (Exception ex) { Log(file + Environment.NewLine + "\t" + ex.ToString(), "Error"); }
                }
                Status_Text.Text = "Done";
                Status_Text.Visible = true;
                Status_Text.ForeColor = Color.Green;
            }
        }

        private void Anime_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                {
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
                        int Count = Directory.GetFiles(Folder_Browser_Dialog.SelectedPath, "*.*", SearchOption.AllDirectories).Count();
                        ProgressBar.Maximum = Count * 100;
                        Mini_ProgressBar.Maximum = 100;
                        Status_Text.Text = "0/" + Count;
                        Status_Text.Visible = true;
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
                                    thedr = database.Get_From_Database("SELECT `Season " + chosen_language + " Name` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + j + "'");
                                    Directory.CreateDirectory(Folder_Browser_Dialog.SelectedPath + @"\" + thedr);
                                    thecount = Convert.ToInt32(database.Get_From_Database("SELECT `Episode Count` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + j + "'"));
                                    thecount += k;
                                    while (k < thecount)
                                    {
                                        Log(The_Files[k].ToString() + " --> " + Folder_Browser_Dialog.SelectedPath + @"\" + thedr + @"\" + The_Files[k].Substring(The_Files[k].LastIndexOf(@"\") + 1), "Msg");
                                        File.Move(The_Files[k].ToString(), Folder_Browser_Dialog.SelectedPath + @"\" + thedr + @"\" + The_Files[k].Substring(The_Files[k].LastIndexOf(@"\") + 1));
                                        k += 1;
                                    }
                                    Log(Environment.NewLine, "Msg");
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
                            strReader = new StringReader(database.Get_From_Database("SELECT `Episodes " + chosen_language + "` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + i + "'"));
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
                                        if (chosen_language == "English")
                                        {
                                            File.Move(FileName, "Episode " + Episode_Number_String + fileext);
                                            Log("Renaming :  " + FileName + "   -->   " + "Episode " + Episode_Number_String + fileext, "Msg");
                                        }
                                        else if (chosen_language == "Japanese")
                                        {
                                            File.Move(FileName, "第 " + Episode_Number_String + " 話" + fileext);
                                            Log("Renaming :  " + FileName + "   -->   " + "第 " + Episode_Number_String + " 話" + fileext, "Msg");
                                        }
                                    }
                                    catch (Exception) { }
                                    using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\anime.bat"))
                                    {
                                        if (chosen_language == "English")
                                            sw.WriteLine("chcp 65001" + Environment.NewLine + @"""" + Application.StartupPath + @"\mkvpropedit.exe"" """ + filepath + @"\" + "Episode " + episode_index + fileext + @"""--edit info--set ""title = " + The_List[The_Episode_Index] + @"""" + Environment.NewLine);
                                        else if (chosen_language == "Japanese")
                                            sw.WriteLine("chcp 65001" + Environment.NewLine + @"""" + Application.StartupPath + @"\mkvpropedit.exe"" """ + filepath + @"\" + "第 " + episode_index + " 話" + fileext + @"""--edit info--set ""title = " + The_List[The_Episode_Index] + @"""" + Environment.NewLine);
                                    }
                                    ProcessStartInfo p = new ProcessStartInfo() { UseShellExecute = false, RedirectStandardOutput = true, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden, FileName = Application.StartupPath + @"\anime.bat" };
                                    Process bat = Process.Start(p);
                                    bat.WaitForExit();
                                    s += 1;
                                    Status_Text.Text = s + "/" + Count;
                                    rename_episode_index += 1;
                                    The_Episode_Index += 1;
                                }
                            }
                            catch (Exception ex2)
                            {
                                Log(ex2.ToString(), "Error");
                            }
                            try
                            {
                                Directory.Move(The_Folders[i - 1].ToString(), database.Get_From_Database("SELECT `Season " + chosen_language + " Name` FROM `Anime Seasons` WHERE `ID`='" + Anime_ID + "' AND `Season Number`='" + i + "'"));
                            }
                            catch (Exception) { }
                        }
                        //File.Delete(Application.StartupPath & "\anime.bat")
                        ProgressBar.Value = 0;
                        Mini_ProgressBar.Value = 0;
                        Status_Text.Text = "All Done";
                        Status_Text.Visible = true;
                        Status_Text.ForeColor = Color.Green;
                        rename_episode_index = 1;
                    }
                    //Try
                    //    Dim Movie_Folder As String = Folder_Browser_Dialog.SelectedPath
                    //    If (Chosen_Language = "English") Then
                    //        Movie_Folder += "\Movies"
                    //    ElseIf (Chosen_Language = "Japanese") Then
                    //        Movie_Folder += "\劇場版"
                    //    End If
                    //    If (Season_Count > 1) Then
                    //        Movie_Folder = Folder_Browser_Dialog.SelectedPath
                    //    End If
                    //    If Directory.Exists(Folder_Browser_Dialog.SelectedPath & "\Movies") Then
                    //        Movie_Folder = Folder_Browser_Dialog.SelectedPath & "\Movies"
                    //    ElseIf Directory.Exists(Folder_Browser_Dialog.SelectedPath & "\劇場版") Then
                    //        Movie_Folder = Folder_Browser_Dialog.SelectedPath & "\劇場版"
                    //    End If
                    //    The_Files = Directory.GetFiles(Movie_Folder).ToList
                    //    strReader = New StringReader(Database_Connection.Get_From_Database("SELECT `" & Chosen_Language & "_Name` FROM `Anime Movies` WHERE `ID`='" & Anime_ID & "'", 0))
                    //    Dim The_Movies As New List(Of String)
                    //    The_Movies.Clear()
                    //    While True
                    //        aLine = strReader.ReadLine()
                    //        If aLine Is Nothing Then
                    //            Exit While
                    //        Else
                    //            The_Movies.Add(aLine)
                    //        End If
                    //    End While
                    //    strReader.Close()
                    //    Dim fileext As String = ""
                    //    //Using fs As FileStream = File.Create(Application.StartupPath & "\anime.bat")
                    //    //    fs.Close()
                    //    //End Using
                    //    For Each Filename In The_Files
                    //        fileext = Filename.Substring(Filename.LastIndexOf("."))
                    //        If (Chosen_Language = "English") Then
                    //            My.Computer.FileSystem.RenameFile(Filename, "Movie " & (The_Files.IndexOf(Filename) + 1) & fileext)
                    //            Log("Renaming :  " & Filename & "   -->   Movie " & (The_Files.IndexOf(Filename) + 1) & fileext, "Msg")
                    //        ElseIf (Chosen_Language = "Japanese") Then
                    //            My.Computer.FileSystem.RenameFile(Filename, "劇場版 第 " & (The_Files.IndexOf(Filename) + 1) & fileext)
                    //            Log("Renaming :  " & Filename & "   -->   劇場版 第 " & (The_Files.IndexOf(Filename) + 1) & fileext, "Msg")
                    //        End If
                    //        //Using sw As StreamWriter = New StreamWriter(Application.StartupPath & "\anime.bat")
                    //        //    If Chosen_Language = "English" Then
                    //        //        sw.WriteLine("chcp 65001" & vbNewLine & """" & Application.StartupPath & "\mkvpropedit.exe"" """ & Movie_Folder & "\" & "Movie  " & (The_Files.IndexOf(Filename) + 1) & fileext & """--edit info--set ""title = " & The_Movies(The_Files.IndexOf(Filename)) & """" & Environment.NewLine)
                    //        //    ElseIf Chosen_Language = "Japanese" Then
                    //        //        sw.WriteLine("chcp 65001" & vbNewLine & """" & Application.StartupPath & "\mkvpropedit.exe"" """ & Movie_Folder & "\" & "劇場版 第 " & (The_Files.IndexOf(Filename) + 1) & " 話" & fileext & """--edit info--set ""title = " & The_Movies(The_Files.IndexOf(Filename)) & """" & Environment.NewLine)
                    //        //    End If
                    //        //End Using
                    //        //Dim p As New ProcessStartInfo With {.UseShellExecute = False, .RedirectStandardOutput = True, .CreateNoWindow = True, .WindowStyle = ProcessWindowStyle.Hidden, .FileName = Application.StartupPath & "\anime.bat"}
                    //        //Dim bat As Process = Process.Start(p)
                    //        //bat.WaitForExit()
                    //    Next
                    //    My.Computer.FileSystem.RenameDirectory(Folder_Browser_Dialog.SelectedPath, Database_Connection.Get_From_Database("SELECT `" & Chosen_Language & " Name` FROM `Anime` WHERE `ID`='" & Anime_ID & "'", 0))
                    //Catch ex As Exception
                    //    MessageBox.Show(ex.ToString())
                    //End Try
                }
                else { }
            }
            catch (Exception ex)
            {
                Log(ex.ToString(), "Error");
            }
        }

        private void Fill_Classes_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Animes = (List<anime>)database.getObjectFromDatabase<anime>();
                Anime_Episodes = (List<anime_episode>)database.getObjectFromDatabase<anime_episode>();
                Anime_Movies = (List<anime_movie>)database.getObjectFromDatabase<anime_movie>();
                Anime_Seasons = (List<anime_season>)database.getObjectFromDatabase<anime_season>();
                Audio_Channels = (List<audio_channel>)database.getObjectFromDatabase<audio_channel>();
                Audio_Codecs = (List<audio_codec>)database.getObjectFromDatabase<audio_codec>();
                Encoders = (List<encoder>)database.getObjectFromDatabase<encoder>();
                Movies = (List<movie>)database.getObjectFromDatabase<movie>();
                Qualities = (List<quality>)database.getObjectFromDatabase<quality>();
                Sources = (List<source>)database.getObjectFromDatabase<source>();
                Tv_Series = (List<tv_series>)database.getObjectFromDatabase<tv_series>();
                Tv_Series_Episodes = (List<tv_series_episode>)database.getObjectFromDatabase<tv_series_episode>();
                Tv_Series_Seasons = (List<tv_series_season>)database.getObjectFromDatabase<tv_series_season>();
                Video_Codecs = (List<video_codec>)database.getObjectFromDatabase<video_codec>();
            }
            catch (Exception ex) { Log(ex.Message + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error"); }
            finally { Log("Initiation completed...", "Success"); }
        }

        private void Fill_Classes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //try
            //{
            //    string all = "";
            //    foreach (movie Movie in Movies)
            //        all += Movie.source + Environment.NewLine;
            //    foreach (anime Anime in Animes)
            //        all += Anime.english_name + Environment.NewLine;
            //    Log(all.Substring(0, all.LastIndexOf(Environment.NewLine)), "Error");
            //}
            //catch (Exception) { }
            //finally { Log("Database initiated...", "Success"); }
        }
    }
}
