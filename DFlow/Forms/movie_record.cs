using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyPortal.services;

namespace LazyPortal
{
    public partial class movie_record : MetroFramework.Forms.MetroForm
    {
        //private readonly database database = new database();
        private movie Movie = new movie();
        private bool isNew = true;
        private readonly Dictionary<long?, string> qualityDataSource = new Dictionary<long?, string>();
        private readonly Dictionary<long?, string> sourceDataSource = new Dictionary<long?, string>();
        private readonly Dictionary<long?, string> encoderDataSource = new Dictionary<long?, string>();
        private readonly Dictionary<long?, string> videoCodecDataSource = new Dictionary<long?, string>();
        private readonly Dictionary<long?, string> audioCodecDataSource = new Dictionary<long?, string>();
        private readonly Dictionary<long?, string> audioChannelDataSource = new Dictionary<long?, string>();

        public movie_record()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            loadBackground.RunWorkerAsync();
        }

        public movie_record(long? select_movie = null)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            loadBackground.RunWorkerAsync(select_movie);
        }

        private void getDictionary_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!torrentDownload.IsHandleCreated) { }
                loadingImage.BeginInvoke(new Action(() => { loadingImage.Visible = true; }));
                foreach (quality quality in (List<quality>)database.getObjectFromDatabase<quality>())
                    qualityDataSource.Add(quality.id, quality.name);

                foreach (source source in (List<source>)database.getObjectFromDatabase<source>())
                    sourceDataSource.Add(source.id, source.name);

                foreach (encoder encoder in (List<encoder>)database.getObjectFromDatabase<encoder>())
                    encoderDataSource.Add(encoder.id, encoder.name);

                foreach (video_codec video_codec in (List<video_codec>)database.getObjectFromDatabase<video_codec>())
                    videoCodecDataSource.Add(video_codec.id, video_codec.name);

                foreach (audio_codec audio_codec in (List<audio_codec>)database.getObjectFromDatabase<audio_codec>())
                    audioCodecDataSource.Add(audio_codec.id, audio_codec.name);

                foreach (audio_channel audio_channel in (List<audio_channel>)database.getObjectFromDatabase<audio_channel>())
                    audioChannelDataSource.Add(audio_channel.id, audio_channel.name);

                qualityComboBox.DataSource = qualityDataSource.ToArray();
                sourceComboBox.DataSource = sourceDataSource.ToArray();
                encoderComboBox.DataSource = encoderDataSource.ToArray();
                videoCodecComboBox.DataSource = videoCodecDataSource.ToArray();
                audioCodecComboBox.DataSource = audioCodecDataSource.ToArray();
                audioChannelComboBox.DataSource = audioChannelDataSource.ToArray();

                //getting the movie info
                if (e.Argument == null)
                    Text = "New Movie";
                else
                {
                    Movie = ((List<movie>)database.getObjectFromDatabase<movie>(new movie() { id = (long)e.Argument }))[0];
                    Text = Movie.name;
                    nameText.Text = Movie.name;
                    sourceComboBox.SelectedItem = Movie.source;
                    qualityComboBox.SelectedItem = Movie.quality;
                    encoderComboBox.SelectedItem = Movie.encoder;
                    videoCodecComboBox.SelectedItem = Movie.video_codec;
                    IMDb_Text.Text = Movie.IMDb;
                    subtitleLink.Text = Movie.subtitle_link;
                    sizeText.Text = FormatSize(Movie.size);
                    typeText.Text = Movie.type;
                    audioCodecComboBox.SelectedItem = Movie.audio_codec;
                    audioChannelComboBox.SelectedItem = Movie.audio_channel;
                    videoBitrateText.Text = Movie.video_bitrate;
                    Refresh();
                    isNew = false;
                }
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally
            {
                loadingImage.BeginInvoke(new Action(() => { loadingImage.Visible = false; }));
            }
        }
        static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public static string FormatSize(decimal? bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }
        private void movie_record_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            GC.Collect();
        }
        public void setButtonColors()
        {
            try
            {
                //saveButton
                if ((string)saveButton.Tag == "green")
                { saveButton.BackColor = Properties.Settings.Default.green; saveButton.Tag = "blue"; }
                else if ((string)saveButton.Tag == "red")
                { saveButton.BackColor = Properties.Settings.Default.red; saveButton.Tag = "blue"; }
                else if ((string)saveButton.Tag == "blue")
                    saveButton.BackColor = Properties.Settings.Default.blue;
                //torrentUpload
                if ((string)torrentUpload.Tag == "green")
                { torrentUpload.BackColor = Properties.Settings.Default.green; torrentUpload.Tag = "blue"; }
                else if ((string)torrentUpload.Tag == "red")
                { torrentUpload.BackColor = Properties.Settings.Default.red; torrentUpload.Tag = "blue"; }
                else if ((string)torrentUpload.Tag == "blue")
                    torrentUpload.BackColor = Properties.Settings.Default.blue;
                //torrentDownload
                if ((string)torrentDownload.Tag == "green")
                { torrentDownload.BackColor = Properties.Settings.Default.green; torrentDownload.Tag = "blue"; }
                else if ((string)torrentDownload.Tag == "red")
                { torrentDownload.BackColor = Properties.Settings.Default.red; torrentDownload.Tag = "blue"; }
                else if ((string)torrentDownload.Tag == "blue")
                    torrentDownload.BackColor = Properties.Settings.Default.blue;
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Movie.name = nameText.Text;
                Movie.source = (long?)sourceComboBox.SelectedValue;
                Movie.quality = (long?)qualityComboBox.SelectedValue;
                Movie.encoder = (long?)encoderComboBox.SelectedValue;
                Movie.video_codec = (long?)videoCodecComboBox.SelectedValue;
                Movie.IMDb = IMDb_Text.Text;
                Movie.subtitle_link = subtitleLink.Text;
                Movie.type = typeText.Text;
                Movie.audio_codec = (long?)audioCodecComboBox.SelectedValue;
                Movie.audio_channel = (long?)audioChannelComboBox.SelectedValue;
                Movie.video_bitrate = videoBitrateText.Text;
                if (isNew)
                {
                    if (database.insertObjectToDatabase(Movie))
                        saveButton.Tag = "green";
                    else
                        saveButton.Tag = "red";
                }
                else
                {
                    if (database.updateObjectToDatabase(Movie))
                        saveButton.Tag = "green";
                    else
                        saveButton.Tag = "red";
                }

            }
            catch (Exception ex) { saveButton.Tag = "red"; Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally { setButtonColors(); }
        }

        private void fromFile_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex) { fromFile.Tag = "red"; Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally { setButtonColors(); }
        }

        private void torrentUpload_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog Open_File_Dialog = new OpenFileDialog() { Multiselect = false, Filter = "Torrent File|*.torrent" })
                {
                    if (Open_File_Dialog.ShowDialog() == DialogResult.OK)
                    { Movie.torrent = Convert.ToBase64String(File.ReadAllBytes(Open_File_Dialog.FileName)); torrentUpload.Tag = "green"; }
                }
            }
            catch (Exception ex) { torrentUpload.Tag = "red"; Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally { setButtonColors(); }
        }

        private void torrentDownload_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog Save_File_Dialog = new SaveFileDialog() { Filter = "Torrent File|*.torrent", FileName = Movie.name + ".torrent" })
                {
                    if (Save_File_Dialog.ShowDialog() == DialogResult.OK)
                    { File.WriteAllBytes(Save_File_Dialog.FileName, Convert.FromBase64String(Movie.torrent)); torrentDownload.Tag = "green"; }
                }
            }
            catch (Exception ex) { torrentDownload.Tag = "red"; Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally { setButtonColors(); }
        }
    }
}
