using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using LazyPortal.Classes;
using LazyPortal.services;
using System.Text.RegularExpressions;
using System.Threading;
using Matroska;
using TraktNet;
using System.Net.Http;
using TraktNet.Responses;
using TraktNet.Objects.Get.Shows;
using TraktNet.Objects.Authentication;
using LazyPortal;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Linq;

namespace LazyPortal
{

    public partial class Main : MetroFramework.Forms.MetroForm
    {

        [Obsolete]
        public Main()
        {
            InitializeComponent();
            if (!Directory.Exists(Properties.Settings.Default.merge_destination))
                Directory.CreateDirectory(Properties.Settings.Default.merge_destination);
            Destination.Text = Properties.Settings.Default.merge_destination;

            if (database.Check_Connection())
                log("Connection successful...", msgType.success, true, true, false);
            else
                log("Connection unsuccessful...", msgType.error, true, true, false);
            check_TMDB();
        }

        private void check_TMDB()
        {
            Task.Run(() =>
            {
                if (new RestClient("https://api.themoviedb.org/3/authentication/token/new?api_key=9a49cbab6d640fd9483fbdd2abe22b94").ExecuteAsync(new RestRequest("", Method.Get)) != null)
                    log("TMDB connection successful...", msgType.success);
                else
                    log("TMDB connection unsuccessful...", msgType.error);
            });
        }

        #region InvokeMethods

        public void change_status(string text, uint type)
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

        public Control get_control_by_name(string name)
        {
            if (InvokeRequired)
                return (Control)Invoke(new Func<Control>(() => get_control_by_name(name)));
            else
                return Controls.Find(name, true)[0];
        }

        public void enable_main(bool status)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { enable_main(status); };
                Invoke(safeAction);
            }
            else
                Enabled = status;
        }

        public void change_button_text(string text, Control ctl, uint type = 0x00000000)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { change_button_text(text, ctl, type); };
                ctl.Invoke(safeAction);
            }
            else
            {
                ctl.Text = text;
                if (type != 0)
                    ctl.BackColor = Color.FromArgb((int)type);
            }
        }

        public void log(string str, uint Type, bool no_line = false, bool first_msg = false, bool InvokeRequired = true)
        {
            try
            {
                if (InvokeRequired)
                {
                    log_richtextbox.BeginInvoke(new Action(() =>
                    {
                        if (!no_line && !first_msg)
                            log_richtextbox.AppendText(Environment.NewLine);
                        log_richtextbox.AppendText(str);
                        if (str.Split('\n').Length > 1)
                            log_richtextbox.Select(log_richtextbox.TextLength - str.Length + str.Split('\n').Length - 1, str.Length + str.Split('\n').Length);
                        else
                            log_richtextbox.Select(log_richtextbox.TextLength - str.Length, str.Length);

                        log_richtextbox.SelectionColor = Color.FromArgb((int)Type);
                        log_richtextbox.ScrollToCaret();
                    }));
                }
                else
                {
                    if (!no_line && !first_msg)
                        log_richtextbox.AppendText(Environment.NewLine);
                    log_richtextbox.AppendText(str);
                    if (str.Split('\n').Length > 1)
                        log_richtextbox.Select(log_richtextbox.TextLength - str.Length + str.Split('\n').Length - 1, str.Length + str.Split('\n').Length);
                    else
                        log_richtextbox.Select(log_richtextbox.TextLength - str.Length, str.Length);

                    log_richtextbox.SelectionColor = Color.FromArgb((int)Type);
                    log_richtextbox.ScrollToCaret();
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
        }

        #region ProgressBar

        public int get_progressbar_value(ProgressBar pBar)
        {
            if (InvokeRequired)
                return (int)Invoke(new Func<int>(() => get_progressbar_value(pBar)));
            else
                return pBar.Value;
        }
        public void update_progressbar(int progress, ProgressBar pBar)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { update_progressbar(progress, pBar); };
                pBar.Invoke(safeAction);
            }
            else
                pBar.Value = progress;
        }
        public void increment_progressbar(int progress, ProgressBar pBar)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { increment_progressbar(progress, pBar); };
                pBar.Invoke(safeAction);
            }
            else
                pBar.Value += progress;
        }
        public void max_progressbar(int maxValue, ProgressBar pBar)
        {
            if (InvokeRequired)
            {
                Action safeAction = delegate { max_progressbar(maxValue, pBar); };
                pBar.Invoke(safeAction);
            }
            else
                pBar.Maximum = maxValue;
        }

        #endregion

        public FolderBrowserDialog get_folderbrowserdialog()
        {
            if (InvokeRequired)
                return (FolderBrowserDialog)Invoke(new Func<FolderBrowserDialog>(() => get_folderbrowserdialog()));
            else
            {
                if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
                    return Folder_Browser_Dialog;
                else
                    return null;
            }
        }

        public OpenFileDialog get_openfiledialog(string xfilter = "")
        {
            if (InvokeRequired)
            {
                return (OpenFileDialog)Invoke(new Func<OpenFileDialog>(() => get_openfiledialog(xfilter)));
            }
            else
            {
                if (xfilter != "")
                    Open_File_Dialog.Filter = xfilter;
                else
                    Open_File_Dialog.Filter = null;

                if (Open_File_Dialog.ShowDialog() == DialogResult.OK)
                    return Open_File_Dialog;
                else
                    return null;
            }
        }

        #endregion

        private void timer_btn_Click(object sender, EventArgs e)
        {
            main_timer.Enabled = !main_timer.Enabled;
            if (main_timer.Enabled)
            {
                timer_btn.Text = "Stop";
                log("Timer Started...", msgType.message);
            }
            else
            {
                timer_btn.Text = "Start";
                log("Timer Stopped...", msgType.message);
            }
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void merge_destination_browse_btn_Click(object sender, EventArgs e)
        {
            if (Folder_Browser_Dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.merge_destination = Folder_Browser_Dialog.SelectedPath;
                Destination.Text = Properties.Settings.Default.merge_destination;
            }
        }

        private void main_timer_Tick(object sender, EventArgs e)
        {
            if (!poster_background_worker.IsBusy)
                poster_background_worker.RunWorkerAsync();
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

        private void new_function_test_btn_Click(object sender, EventArgs e)
        {
            Task.Run(() => MKVToolNix.DefaultSubtitles());
        }
        private void movie_file_poster_btn_Click(object sender, EventArgs e)
        {
            Task.Run(() => Poster.SetFileThumbnail());
        }

        private void Merge_Button_Click(object sender, EventArgs e)
        {
            if (!MKVToolNix.mergeInProgress)
                MKVToolNix.MergeMovies();
            else
                MKVToolNix.cancellationPending = true;
        }

        private void movie_folder_poster_btn_Click(object sender, EventArgs e)
        {
            Task.Run(() => Poster.SetPoster());
        }

        private void anime_browse_btn_Click(object sender, EventArgs e)
        {
            new anime_record().Show();
        }

        private void music_rename_btn_Click(object sender, EventArgs e)
        {
            Naming.RenameMusic();
        }

        private void anime_rename_btn_Click(object sender, EventArgs e)
        {
            Naming.RenameAnime();
        }

        private void main_timer_background_worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                DirectoryInfo torrent_download_folder = new DirectoryInfo(@"D:\Torrent Downloads");

                foreach (DirectoryInfo sub_directories in torrent_download_folder.GetDirectories())
                {
                    switch (sub_directories.Name.ToLower())
                    {
                        case "new folder":
                            log(@"Folder name was ""New Folder""", msgType.message);
                            break;
                        case var folder_name when new Regex(@"(s\d+e\d+)|(\s+第\d+話)").IsMatch(folder_name):
                            log(folder_name + " is for season episode", msgType.message);
                            break;
                        default:
                            if (sub_directories.GetFiles("folder.ico").Length == 0)
                            {
                                log(sub_directories.Name + " does not have a thumbnail.", msgType.message);
                                
                                Task set_poster_task;

                                switch (sub_directories.Name.ToLower())
                                {
                                    case var folder_name when new Regex(@"(\s+season\s+)|(\s+s\d+)").IsMatch(folder_name):
                                        set_poster_task = Task.Run(() => Poster.SetPoster(sub_directories.FullName, mediaTypes.tv));
                                        break;
                                    case var folder_name when new Regex(@"erai-raws").IsMatch(folder_name):
                                        set_poster_task = Task.Run(() => Poster.SetPoster(sub_directories.FullName, mediaTypes.tv));
                                        break;
                                    default:
                                        set_poster_task = Task.Run(() => Poster.SetPoster(sub_directories.FullName, mediaTypes.movie));
                                        break;
                                }

                                set_poster_task.Wait();
                            }
                            break;
                    }
                }

                Task set_thumbnail_task;
                foreach (FileInfo files in torrent_download_folder.GetFiles())
                {
                    switch (files.Extension)
                    {
                        
                        case ".mkv":
                            if (MatroskaSerializer.Deserialize(new FileStream(files.FullName, FileMode.Open, FileAccess.Read)).Segment.Attachments.AttachedFiles.Find(x => (x.FileMimeType == "image/png" || x.FileMimeType == "image/jpg") && x.FileName.Contains("cover")) == null)
                            {
                                log(@"Thumbnailing """ + files.Name + @"""", msgType.message);
                                set_thumbnail_task = Task.Run(() => Poster.SetFileThumbnail(files.FullName));
                                set_thumbnail_task.Wait();
                            } 
                            break;
                        case ".mp4":
                            log(@"Need to muxe """ + files.Name + @"""", msgType.message);
                            set_thumbnail_task = Task.Run(() => Poster.SetFileThumbnail(files.FullName));
                            set_thumbnail_task.Wait();
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void manga_button_Click(object sender, EventArgs e)
        {
            try
            {
                new manga_browser().Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}