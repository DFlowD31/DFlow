using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using LazyPortal.Classes;
using LazyPortal.services;

namespace LazyPortal
{

    public partial class Main : MetroFramework.Forms.MetroForm
    {

        [Obsolete]
        public Main()
        {
            InitializeComponent();
            Destination.Text = Properties.Settings.Default.merge_destination;
            if (database.Check_Connection())
                Log("Connection successful...", msgType.success, true, true, false);
            else
                Log("Connection unsuccessful...", msgType.error, true, true, false);
            checkTMDB();
        }

        private void checkTMDB()
        {
            Task.Run(() =>
            {
                if (new RestClient("https://api.themoviedb.org/3/authentication/token/new?api_key=9a49cbab6d640fd9483fbdd2abe22b94") { Proxy = SimpleWebProxy.Default }.Execute(new RestRequest(Method.GET)) != null)
                    Log("TMDB connection successful...", msgType.success);
                else
                    Log("TMDB connection unsuccessful...", msgType.error);
            });
        }

        #region InvokeMethods

        public void changeStatus(string text, uint type)
        {
            if (InvokeRequired)
            {
                Status_Text.BeginInvoke(new Action(() =>
                {
                    Status_Text.Text = text;
                    Status_Text.Visible = true;
                    Status_Text.ForeColor = Color.FromArgb((int)type);
                }));
            }
            else
            {
                Status_Text.Text = text;
                Status_Text.Visible = true;
                Status_Text.ForeColor = Color.FromArgb((int)type);
            }
        }

        public Control getCtlByName(string name)
        {
            if (InvokeRequired)
                return (Control)Invoke(new Func<Control>(() => getCtlByName(name)));
            else
                return Controls.Find(name, true)[0];
        }

        public void enableMain(bool status)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { enableMain(status); };
                Invoke(safeAction);
            }
            else
                Enabled = status;
        }

        public void changeButtonText(string text, Control ctl, uint type = 0x00000000)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { changeButtonText(text, ctl, type); };
                ctl.Invoke(safeAction);
            }
            else
            {
                ctl.Text = text;
                if (type != 0)
                    ctl.BackColor = Color.FromArgb((int)type);
            }
        }

        public void Log(string str, uint Type, bool no_line = false, bool first_msg = false, bool InvokeRequired = true)
        {
            try
            {
                if (InvokeRequired)
                {
                    Log_Text.BeginInvoke(new Action(() =>
                    {
                        //try
                        //{
                        if (!no_line && !first_msg)
                            Log_Text.AppendText(Environment.NewLine);
                        Log_Text.AppendText(str);
                        if (str.Split('\n').Length > 1)
                            Log_Text.Select(Log_Text.TextLength - str.Length + str.Split('\n').Length - 1, str.Length + str.Split('\n').Length);
                        else
                            Log_Text.Select(Log_Text.TextLength - str.Length, str.Length);

                        Log_Text.SelectionColor = Color.FromArgb((int)Type);
                        Log_Text.ScrollToCaret();
                        //}
                        //catch (Exception e) { MessageBox.Show(e.ToString()); }
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

                    Log_Text.SelectionColor = Color.FromArgb((int)Type);
                    Log_Text.ScrollToCaret();
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
        }

        #region ProgressBar

        public int getProgressBarValue(ProgressBar pBar)
        {
            if (InvokeRequired)
                return (int)Invoke(new Func<int>(() => getProgressBarValue(pBar)));
            else
                return pBar.Value;
        }
        public void updateProgressBar(int progress, ProgressBar pBar)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { updateProgressBar(progress, pBar); };
                pBar.Invoke(safeAction);
            }
            else
                pBar.Value = progress;
        }
        public void incrementProgressBar(int progress, ProgressBar pBar)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { incrementProgressBar(progress, pBar); };
                pBar.Invoke(safeAction);
            }
            else
                pBar.Value += progress;
        }
        public void maxProgress(int maxValue, ProgressBar pBar)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { maxProgress(maxValue, pBar); };
                pBar.Invoke(safeAction);
            }
            else
                pBar.Maximum = maxValue;
        }

        #endregion

        public FolderBrowserDialog getFolderBrowserDialog()
        {
            if (InvokeRequired)
                return (FolderBrowserDialog)Invoke(new Func<FolderBrowserDialog>(() => getFolderBrowserDialog()));
            else
            {
                if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                    return Folder_Browser_Dialog;
                else
                    return null;
            }
        }

        #endregion

        private void Timer_Button_Click(object sender, EventArgs e)
        {
            Shutdown_Timer.Enabled = !Shutdown_Timer.Enabled;
            if (!Shutdown_Timer.Enabled)
                Log("Timer Started...", msgType.message);
            else
                Log("Timer Stopped...", msgType.message);
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
            if (Date_Time_Picker.Value <= DateTime.Now)
            {
                Shutdown_Timer.Enabled = false;
                Process.Start("shutdown", "/s /t 0");
            }
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

        private void Movie_Folder_Click(object sender, EventArgs e)
        {
            if (new choice_box().ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(choice.series.ToString());
            }
        }
        private void Movie_File_Click(object sender, EventArgs e)
        {
            try
            {
                checkTMDB();
                //if (Open_File_Dialog.ShowDialog() == DialogResult.OK) {
                //    var fileName = Open_File_Dialog.SafeFileName;
                //    var filePath = Open_File_Dialog.FileName;

                //    if (fileName.Substring(fileName.LastIndexOf(".") + 1) == "mkv") {

                //        var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                //        var doc = MatroskaSerializer.Deserialize(file);

                //        if (doc.Segment.Attachments != null)
                //        {
                //            MessageBox.Show("Has attachment.");
                //        }
                //        else {
                //            MessageBox.Show("No attachment");
                //        }
                //        //var file = new FileStream("propedit.bat", FileMode.CreateNew, FileAccess.ReadWrite);

                //        //file.Write(@"""C:\Program Files\MKVToolnix\mkvpropedit.exe""");
                //    }
                //}
                //var file = new FileStream(@"D:\Torrent Downloads\Tom and Jerry (2021)  [1080p x265 10bit S81 Joy].mkv", FileMode.Open, FileAccess.Read);
                //var doc = MatroskaSerializer.Deserialize(file);

                ////MessageBox.Show(doc.Segment.Tracks.TrackEntries.ToString());
                //foreach (var track in doc.Segment.Tracks.TrackEntries) {
                //    if (track.TrackType == 17)
                //    {
                //        //Log(track.LanguageIETF.ToString(), "Message");
                //    }
                //    else if (track.TrackType == 1) {
                //        Log(track.Video.DisplayWidth + ":" + track.Video.DisplayHeight, "Message");
                //    }
                //    if (track.AttachmentLink.HasValue) {
                //        Log(track.AttachmentLink.Value.ToString(), "Message");
                //    }
                //}

                //@"""C:\Program Files\MKVToolNix\mkvmerge.exe"" --ui-language en --output ^""D:\MKV Convertions\Tom and Jerry (2021) [1080p x265 10bit S81 Joy].mkv^"" --language 0:und --display-dimensions 0:1920x1040 --language 1:en

                //Log(doc.Segment.Tracks.TrackEntries[0].Name.ToString(), "Message");
                //MessageBox.Show("Done");
            }
            catch (Exception ex) { Log(ex.ToString(), msgType.error); }
        }

        private void Merge_Button_Click(object sender, EventArgs e)
        {
            if (!MKVToolNix.mergeInProgress)
                MKVToolNix.MergeMovies();
            else
                MKVToolNix.cancellationPending = true;
        }

        private void Poster_Click(object sender, EventArgs e)
        {
            Poster.SetPoster();
        }

        private void Anime_Browse_Click(object sender, EventArgs e)
        {
            new anime_record().Show();
        }

        private void Music_Button_Click(object sender, EventArgs e)
        {
            Naming.RenameMusic();
        }

        private void Anime_Button_Click(object sender, EventArgs e)
        {
            Naming.RenameAnime();
        }
    }
}