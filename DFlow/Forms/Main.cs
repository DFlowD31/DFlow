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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using Newtonsoft.Json;
using DFlow.Classes;
using System.Net;
using FanartTv;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using TraktNet;
using TraktNet.Responses;
using TraktNet.Objects.Get.Shows;
using TraktNet.Requests.Parameters;

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
        //bool have_seasons = false;
        readonly bool get_from_text_file = false;
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
        public static List<language> Languages = new List<language>();

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

        public Main()
        {
            InitializeComponent();
            Destination.Text = Properties.Settings.Default.merge_destination;
            if (database.Check_Connection())
            {
                Log("Connection Successfull...", "Success", true, true, false);
                //Fill_Classes.RunWorkerAsync();
                //Qualities = (List<quality>)database.getObjectFromDatabase<quality>();
            };

            var response = new RestClient("https://api.themoviedb.org/3/authentication/token/new?api_key=9a49cbab6d640fd9483fbdd2abe22b94") { Proxy = SimpleWebProxy.Default }.Execute(new RestRequest(Method.GET));
            if (response != null)
                Log("TMDB connection successfull", "Success");

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

        public void Rename_Sub(DirectoryInfo d)
        {
            try
            {
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
                        string filepath = SubFiles.FullName.Substring(0, SubFiles.FullName.LastIndexOf(@"\"));
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

        [Obsolete]
        private void Server_Config_Click(object sender, EventArgs e)
        {
            //new movie_record().Show();

            //var client = new TraktClient("6129e911f65e311e9b6b47826701c92c89efe788e9ba14696698fbd2feb8b45a", "1f981cf20baf5c306be6f75a102edc84757d5df3e1a5347e52121352ed4573bd");

            //TraktResponse<ITraktShow> sherlock = await client.Shows.GetShowAsync("sherlock", new TraktExtendedInfo().SetFull()).ConfigureAwait(false);

            //ITraktShow show = null;

            //if (sherlock)
            //{
            //    show = sherlock.Value;
            //}

            //// Set your Apikey
            //FanartTv.API.Key = "e0151f36c7575f70eb43869fdc85e46c";

            //FanartTv.TV.Show show2 = new FanartTv.TV.Show(show.Ids.Tvdb.ToString());

            //MessageBox.Show(show2.List.Seasonposter[0].Season);

            if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
            {
                //List<string> strings = Regex.Split(Folder_Browser_Dialog.SelectedPath.Substring(Folder_Browser_Dialog.SelectedPath.LastIndexOf(@"\")), @"\W|_").ToList();

                //string all_s = string.Empty;
                //string quality = string.Empty;

                //for (int i = strings.Count() - 1; i > 0; i--) {
                //    List<quality> Q = (List<quality>)database.getObjectFromDatabase<quality>();
                //    foreach (quality q in Q) {
                //        if (q.alternate_name.Split(',').ToList().Find(x => x.ToLower() == strings[i].ToLower()) is string y && y != null)
                //        {
                //            strings.RemoveAt(i);
                //            quality = q.name;
                //        }
                //    }
                //}

                //MessageBox.Show(quality);

                if (MessageBox.Show("Multiple movie container?", "Directory type", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    String[] directories = Directory.GetDirectories(Folder_Browser_Dialog.SelectedPath);
                    ProgressBar.Step = 1;
                    ProgressBar.Maximum = directories.Count();
                    icoFromImageQueuer.RunWorkerAsync(directories);
                }
                else
                {
                    if (MessageBox.Show("Is this a series folder?", "Type", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        icoFromImage.RunWorkerAsync(new List<string> { Folder_Browser_Dialog.SelectedPath, "tv" });
                    else
                        icoFromImage.RunWorkerAsync(new List<string> { Folder_Browser_Dialog.SelectedPath, "movie" });
                }
            }
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

        private void hideFile(String path)
        {
            // Set ini file attribute to "Hidden"
            if ((File.GetAttributes(path) & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
            }

            // Set ini file attribute to "System"
            if ((File.GetAttributes(path) & FileAttributes.System) != FileAttributes.System)
            {
                File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.System);
            }
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
                Languages = (List<language>)database.getObjectFromDatabase<language>();
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

        public static int Compute(string s, string t)
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

        double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = Compute(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        private void removeFromName<T>(ref string name) {
            foreach (dynamic x in (List<T>)database.getObjectFromDatabase<T>())
            {
                name = Regex.Replace(name, $@"\b{x.name}\b", string.Empty);
            }
        }

        private async Task<FanartTv.TV.Show> gettvID(string name) {
            // Set your Apikey
            API.Key = "e0151f36c7575f70eb43869fdc85e46c";

            return new FanartTv.TV.Show((await new TraktClient("6129e911f65e311e9b6b47826701c92c89efe788e9ba14696698fbd2feb8b45a", "1f981cf20baf5c306be6f75a102edc84757d5df3e1a5347e52121352ed4573bd").Shows.GetShowAsync(name, new TraktExtendedInfo().SetFull())).Value.Ids.Tvdb.ToString());
        }

        private void icoFromImage_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> arguments = (List<string>)e.Argument;
            string directory = arguments[0];
            string type = arguments[1];

            if (directory.Substring(directory.LastIndexOf(@"\") + 1).ToLower() != "tv") {
                var ImageFiles = GetFilesFrom(directory, new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" }, false);
                string iniPath = directory + @"\desktop.ini";
                string icoPath = directory + @"\folder.ico";
                if (File.Exists(icoPath)) File.Delete(icoPath);
                if (File.Exists(iniPath)) File.Delete(iniPath);

                Bitmap mainBitmap = null;

                if (ImageFiles.Length > 0)
                {
                    mainBitmap = new Bitmap(ImageFiles[0]);
                }
                else
                {
                    string name = directory.Substring(directory.LastIndexOf(@"\") + 1);
                    List<int> years = new List<int>();

                    string folderName = name;
                    int s_number = 0;

                    name = name.Replace(".", " ");

                    if (type == "tv")
                    {
                        try
                        {
                            if (name.Contains("第") && name.Contains("期"))
                            {
                                s_number = Convert.ToInt32(Regex.Match(Regex.Match(name, @"(?:第 \d+ 期|第 \d{2} 期)|\d+|\d{2}").ToString(), @"\d{2}|\d+").Groups[0].Value);
                            }
                            else
                            {
                                s_number = Convert.ToInt32(Regex.Match(Regex.Match(name, @"(?:Season \d+|Season \d{2})|S\d{2}E\d{2}|S\d{2}|S\d+|S\d+E\d+|\d+|\d{2}").ToString(), @"\d{2}|\d+").Groups[0].Value);
                            }
                        }
                        catch (Exception) { }
                    }
                    
                    removeFromName<quality>(ref name);
                    //removeFromName<audio_channel>(ref name);
                    removeFromName<audio_codec>(ref name);
                    removeFromName<encoder>(ref name);
                    removeFromName<source>(ref name);
                    removeFromName<video_codec>(ref name);
                    removeFromName<language>(ref name);

                    Regex regex = new Regex(@"\d{4}");

                    foreach (Match match in regex.Matches(name))
                    {
                        years.Add(Convert.ToInt32(match.Value));
                    }

                    years.Sort((a, b) => b.CompareTo(a));

                    try { name = Regex.Replace(name, $@"\b{years[0]}\b", string.Empty); } catch (Exception) { }

                    if (years.Count() == 0)
                        years.Add(0);

                    name = Regex.Replace(name, @"[^0-9a-zA-Z\s&一-龯ぁ-んァ-ン\w！：／・]", string.Empty);
                    name = new Regex("[ ]{2,}", RegexOptions.None).Replace(name, " ");
                    name = name.ToLower();

                    if (name.Contains("collection"))
                    {
                        type = "collection";
                        name = name.Replace("collection", string.Empty);
                        name = new Regex("[ ]{2,}", RegexOptions.None).Replace(name, " ");
                    }

                    Log("Simplified name:= " + name, "Msg");

                    MethodInvoker m = new MethodInvoker(() => { Mini_ProgressBar.Maximum = name.Length; Mini_ProgressBar.Value = 0; });
                    Mini_ProgressBar.Invoke(m);

                    while (name != string.Empty)
                    {
                        foreach (int year in years)
                        {
                            //Log("Searching for := " + folderName + " as:= " + name + " Year:= (" + year + ")", "Warning");
                            IRestResponse response = null;
                            if (type == "tv")
                            {
                                response = new RestClient("https://api.themoviedb.org/3/search/" + type + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94&query=" + System.Web.HttpUtility.UrlEncode(name) + "&page=1&include_adult=true&first_air_date_year=" + year.ToString()).Execute(new RestRequest(Method.GET));
                                //Log("https://api.themoviedb.org/3/search/" + type + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94&language=en-US&query=" + System.Web.HttpUtility.UrlEncode(name) + "&page=1&include_adult=true&first_air_date_year=" + year.ToString(), "Msg");
                            }
                            else
                            {
                                response = new RestClient("https://api.themoviedb.org/3/search/" + type + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94&query=" + System.Web.HttpUtility.UrlEncode(name) + "&page=1&include_adult=true&year=" + year.ToString()).Execute(new RestRequest(Method.GET));
                            }
                            dynamic responseContent = null;

                            if (type == "collection")
                            {
                                responseContent = JsonConvert.DeserializeObject<TMDB_collection>(response.Content);
                            }
                            else if (type == "movie")
                            {
                                responseContent = JsonConvert.DeserializeObject<TMDB_movie>(response.Content);
                            }
                            else if (type == "tv")
                            {
                                responseContent = JsonConvert.DeserializeObject<TMDB_tv>(response.Content);
                            }

                            if (responseContent.TotalResults > 0)
                            {
                                string closest_index = string.Empty;

                                if (responseContent.TotalResults == 1)
                                {
                                    closest_index = "0";
                                }
                                else
                                {

                                    double? most_match = 0.0;
                                    foreach (dynamic result in responseContent.Results)
                                    {
                                        var title = string.Empty;
                                        var year2 = string.Empty;
                                        if (type == "collection")
                                        {
                                            title = result.Name;
                                        }
                                        else if (type == "movie")
                                        {
                                            title = result.Title;
                                            try { year2 = result.ReleaseDate.Year.ToString(); } catch (Exception) { }
                                        }
                                        else if (type == "tv")
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
                                    //}
                                }
                                try
                                {
                                    if (closest_index != string.Empty)
                                    {
                                        if (type == "tv")
                                        {
                                            // Set your Apikey
                                            if (s_number > 0)
                                            {
                                                Log("https://api.themoviedb.org/3/tv/" + responseContent.Results[Convert.ToInt32(closest_index)].Id + "/season/" + s_number + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94", "Warning");
                                                IRestResponse seasonResponse = new RestClient("https://api.themoviedb.org/3/tv/" + responseContent.Results[Convert.ToInt32(closest_index)].Id + "/season/" + s_number + "?api_key=9a49cbab6d640fd9483fbdd2abe22b94").Execute(new RestRequest(Method.GET));
                                                try
                                                {
                                                    dynamic seasonResponseContent = Newtonsoft.Json.Linq.JObject.Parse(seasonResponse.Content);

                                                    mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + seasonResponseContent.poster_path).GetResponse().GetResponseStream());
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.ToString());
                                                    mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());
                                                }

                                            }
                                            else
                                            {
                                                mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());
                                            }

                                            Log("Poster Found.", "Success");
                                            goto posterFound;
                                        }
                                        else
                                        {
                                            mainBitmap = new Bitmap(WebRequest.Create("https://image.tmdb.org/t/p/w500" + responseContent.Results[Convert.ToInt32(closest_index)].PosterPath).GetResponse().GetResponseStream());
                                            Log("Poster Found.", "Success");
                                            goto posterFound;
                                        }
                                    }
                                }
                                catch (Exception ex) { }
                            }

                            if (name.Contains(" "))
                                name = name.Substring(0, name.LastIndexOf(" "));
                            else
                                name = string.Empty;
                            m = new MethodInvoker(() => Mini_ProgressBar.PerformStep());
                            Mini_ProgressBar.Invoke(m);
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
                    SaveAsIcon(convertedBitmap, directory + @"\folder.ico");

                    File.WriteAllLines(iniPath, new String[] { "[.ShellClassInfo]", "IconFile=" + directory + "folder.ico,0", "[ViewState]", "Mode=", "Vid=", "FolderType=Videos" });

                    hideFile(iniPath);
                    hideFile(icoPath);

                    SHFOLDERCUSTOMSETTINGS folderSettings = new SHFOLDERCUSTOMSETTINGS
                    {
                        dwMask = FolderCustomSettingsMask.IconFile,
                        pszIconFile = "folder.ico",
                        iIconIndex = 0
                    };

                    SHGetSetFolderCustomSettings(ref folderSettings, directory, FolderCustomSettingsRW.ForceWrite);

                    Log("Done.", "Success");
                }
                else
                {
                    Log("No Posters Found.", "Error");
                }
            }
        }

        private void icoFromImageQueuer_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> directories = ((String[])e.Argument).ToList();

            Status_Text.Text = "0/" + directories.Count();
            string type = "movie";

            if (MessageBox.Show("Is this a series folder?", "Type", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                type = "tv";
            }

            foreach (string directory in directories)
            {
                string the_directory = directory;
                while (icoFromImage.IsBusy) { }
                if ((directory.Substring(directory.LastIndexOf(@"\") + 1).ToLower().StartsWith("season ")) || (directory.Substring(directory.LastIndexOf(@"\") + 1).StartsWith("第") && directory.Substring(directory.LastIndexOf(@"\") + 1).EndsWith("期")) || Regex.IsMatch(directory.Substring(directory.LastIndexOf(@"\") + 1), @"^\d+|^\d{2}"))
                {
                    string season = directory.Substring(directory.LastIndexOf(@"\") + 1);
                    string series_name = directory.Substring(0, directory.LastIndexOf(@"\"));
                    series_name = series_name.Substring(series_name.LastIndexOf(@"\") + 1);
                    Directory.Move(directory, directory.Substring(0, directory.LastIndexOf(@"\") + 1) + series_name + " " + season);
                    the_directory = directory.Substring(0, directory.LastIndexOf(@"\") + 1) + series_name + " " + season;
                }
                icoFromImage.RunWorkerAsync(new List<string> { the_directory, type });
                Status_Text.Text = (directories.IndexOf(directory) + 1).ToString() + "/" + directories.Count();
                icoFromImageQueuer.ReportProgress((directories.IndexOf(directory)) + 1);
            }
        }

        private void icoFromImageQueuer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }
    }
}
