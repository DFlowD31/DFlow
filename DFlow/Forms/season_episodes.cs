using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using LazyPortal.services;

namespace LazyPortal
{
    public partial class season_episodes : MetroFramework.Forms.MetroForm
    {
        //private readonly database database = new database();
        private readonly anime_season Season = new anime_season();
        private List<anime_episode> Episodes = new List<anime_episode>();
        //private List<quality> Qualities = new List<quality>();
        //private List<source> Sources = new List<source>();
        private readonly long? the_anime_id = 0;
        private readonly long? the_season_number = 0;
        private DataTable Episodes_DataSource = null;
        public season_episodes(long anime_id, long season_number)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            JPN_Season_Name.GotFocus += JPN_Season_Name_GotFocus;
            ENG_Season_Name.GotFocus += JPN_Season_Name_GotFocus;
            try
            {
                if (anime_id == 0)
                {
                    JPN_Season_Name.Text = "第 " + season_number + " 期";
                    ENG_Season_Name.Text = "Season " + season_number;
                }
                else
                {
                    Season = ((List<anime_season>)database.getObjectFromDatabase<anime_season>(new anime_season() { anime_id = anime_id, season_number = season_number }))[0];
                    JPN_Season_Name.Text = Season.season_japanese_name;
                    ENG_Season_Name.Text = Season.season_english_name;
                    the_anime_id = anime_id;
                    the_season_number = season_number;
                    dataGridView.ScrollBars = ScrollBars.None;
                    getEpisodes.RunWorkerAsync();
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber() + "from: Season episodes main", msgType.error); }
        }

        private void JPN_Season_Name_GotFocus(object sender, EventArgs e)
        {
            setButtonColors();
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            try { dataGridView.ScrollBars = ScrollBars.None; } catch (Exception) { }
            saveBackground.RunWorkerAsync();
        }

        public static bool IsNumeric(string str)
        {
            try
            {
                int foo = int.Parse(str.Trim());
                return true;
            }
            catch (FormatException) { return false; }
        }
        private void From_Excel_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (Open_File_Dialog.ShowDialog() == DialogResult.OK)
                { dataGridView.ScrollBars = ScrollBars.None; getFromExcel.RunWorkerAsync(); }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        private void GetEpisodes_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!dataGridView.IsHandleCreated) { }
                loadingImage.BeginInvoke(new Action(() => loadingImage.Visible = true));
                Episodes = (List<anime_episode>)database.getObjectFromDatabase<anime_episode>(new anime_episode() { anime_id = the_anime_id, season_number = the_season_number });
                try { Episodes_DataSource.Clear(); } catch (Exception) { }
                Episodes_DataSource = (DataTable)database.getObjectFromDatabase<anime_episode>(new anime_episode() { anime_id = the_anime_id, season_number = the_season_number }, true);
                Dictionary<long?, string> qualityDataSource = new Dictionary<long?, string>();
                foreach (quality quality in (List<quality>)database.getObjectFromDatabase<quality>())
                    qualityDataSource.Add(quality.id, quality.name);
                Dictionary<long?, string> sourceDataSource = new Dictionary<long?, string>();
                foreach (source source in (List<source>)database.getObjectFromDatabase<source>())
                    sourceDataSource.Add(source.id, source.name);
                Dictionary<long?, string> encoderDataSource = new Dictionary<long?, string>();
                foreach (encoder encoder in (List <encoder>)database.getObjectFromDatabase<encoder>())
                    encoderDataSource.Add(encoder.id, encoder.name);
                try { dataGridView.Columns.Clear(); } catch (Exception) { }
                try { dataGridView.DataSource = null; } catch (Exception) { }
                dataGridView.DataSource = Episodes_DataSource;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].Visible = false;
                dataGridView.Columns[2].HeaderText = "Episode Number";
                dataGridView.Columns[3].HeaderText = "English Name";
                dataGridView.Columns[4].HeaderText = "Japanese Name";
                dataGridView.Columns[5].Visible = false;
                dataGridView.Columns[6].Visible = false;
                dataGridView.Columns[7].Visible = false;
                dataGridView.Columns[8].Visible = false;
                dataGridView.Tag = "initializing";

                //Encoder
                dataGridView.Columns.Add(new DataGridViewComboBoxColumn() { Name = "encoder", HeaderText = "Encoder", DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox, DataSource = encoderDataSource.ToArray(), DisplayMember = "Value", ValueMember = "Key" });

                //Quality
                dataGridView.Columns.Add(new DataGridViewComboBoxColumn() { Name = "quality", HeaderText = "Quality", DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox, DataSource = qualityDataSource.ToArray(), DisplayMember = "Value", ValueMember = "Key" });

                //Source
                dataGridView.Columns.Add(new DataGridViewComboBoxColumn() { Name = "source", HeaderText = "Source", DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox, DataSource = sourceDataSource.ToArray(), DisplayMember = "Value", ValueMember = "Key" });

                //Torrent Upload
                dataGridView.Columns.Add(new DataGridViewDisableButtonColumn() { Name = "torrent_upload", HeaderText = "Torrent File Upload", Text = "Upload", UseColumnTextForButtonValue = true });

                //Torrent Download
                dataGridView.Columns.Add(new DataGridViewDisableButtonColumn() { Name = "torrent_download", HeaderText = "Torrent File Download", Text = "Download", UseColumnTextForButtonValue = true });
                
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (Convert.ToString(row.Cells[5].Value) != string.Empty) { ((DataGridViewComboBoxCell)row.Cells[9]).Value = encoderDataSource[(long?)row.Cells[5].Value]; }
                    if (Convert.ToString(row.Cells[6].Value) != string.Empty) { ((DataGridViewComboBoxCell)row.Cells[10]).Value = qualityDataSource[(long?)row.Cells[6].Value]; }
                    if (Convert.ToString(row.Cells[7].Value) != string.Empty) { ((DataGridViewComboBoxCell)row.Cells[11]).Value = sourceDataSource[(long?)row.Cells[7].Value]; }
                    if (Convert.ToString(row.Cells[8].Value) != string.Empty || Convert.ToString(row.Cells[7].Value) != string.Empty) { ((DataGridViewDisableButtonCell)row.Cells[12]).Enabled = false; }
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber() + " from: hmmmmmmm", msgType.error); }
            finally
            {
                startIndex.Text = "1";
                endIndex.Text = Season.episode_count.ToString();
                dataGridView.Tag = "";
                setButtonColors();
                loadingImage.BeginInvoke(new Action(() => loadingImage.Visible = false));
            }
        }

        private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((string)dataGridView.Tag != "initializing")
                {
                    if (e.ColumnIndex == 9) ((DataGridView)sender).Rows[e.RowIndex].Cells[5].Value = ((DataGridViewComboBoxCell)((DataGridView)sender).Rows[e.RowIndex].Cells[9]).Value;
                    else if (e.ColumnIndex == 10) ((DataGridView)sender).Rows[e.RowIndex].Cells[6].Value = ((DataGridViewComboBoxCell)((DataGridView)sender).Rows[e.RowIndex].Cells[10]).Value;
                    else if (e.ColumnIndex == 11) ((DataGridView)sender).Rows[e.RowIndex].Cells[7].Value = ((DataGridViewComboBoxCell)((DataGridView)sender).Rows[e.RowIndex].Cells[11]).Value;
                }
            }
            catch (Exception) { }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DataGridView)sender).IsCurrentCellDirty)
                    ((DataGridView)sender).CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception) { }
        }
        //https://api.jikan.moe/v3/anime/20/episodes/1
        private void GetFromExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            //    try
            //    {
            //        loadingImage.BeginInvoke(new Action(() => loadingImage.Visible = true));
            //        using (Workbook workBook = new Workbook())
            //        {
            //            workBook.LoadFromFile(Open_File_Dialog.FileName);
            //            Worksheet MySheet = workBook.Worksheets[0];
            //            int inSeason = 1;
            //            int inOverall = 1;
            //            int nameColumn = 2;
            //            int i = 1;
            //            while (!MySheet.Range[i, inOverall].Value.ToLower().Contains(Season.season_english_name.ToLower()) && !MySheet.Range[i, inOverall].Value.ToLower().Contains("season " + the_season_number) && !MySheet.Range[i, inOverall].Value.ToLower().Contains("season " + ((long)the_season_number).ToString("D2")) && i != MySheet.LastRow)
            //            { i += 1; }
            //            if (i != MySheet.LastRow)
            //            {
            //                while (!IsNumeric(MySheet.Range[i, inOverall].Value))
            //                { i += 1; }
            //                if (i != MySheet.LastRow)
            //                {
            //                    while (!MySheet.Range[i, inSeason].Value.ToLower().Contains("season") && !MySheet.Range[i, inSeason].Value.ToLower().Contains("arc") && MySheet.Range[i, inSeason].Value.Length < 100 && i != MySheet.LastRow)
            //                    {//row, column
            //                        if (IsNumeric(MySheet.Range[i, inSeason].Value))
            //                        {
            //                            if (IsNumeric(MySheet.Range[i, nameColumn].Value))
            //                            {
            //                                if (MySheet.Range[i - 1, nameColumn].Value.ToLower().Contains("season") || MySheet.Range[i - 1, nameColumn].Value.ToLower().Contains("arc"))
            //                                { inOverall = inSeason; inSeason = nameColumn; nameColumn += 1; }
            //                                else if (MySheet.Range[i - 1, nameColumn].Value.ToLower().Contains("series") || MySheet.Range[i - 1, nameColumn].Value.ToLower().Contains("no") || MySheet.Range[i - 1, nameColumn].Value.ToLower().Contains("overall"))
            //                                { inSeason = inOverall; inOverall = nameColumn; nameColumn += 1; }
            //                                else
            //                                    nameColumn += 1;
            //                            }
            //                            if (MySheet.Range[i, nameColumn].Value.Contains("\""))
            //                            {
            //                                long overallNumber = Convert.ToInt64(MySheet.Range[i, inOverall].Value);
            //                                decimal seasonNumber = Convert.ToDecimal(MySheet.Range[i, inSeason].Value);
            //                                string englishName = MySheet.Range[i, nameColumn].Value.Replace("\"", "");
            //                                if (MySheet.Range[i + 1, nameColumn].Value.StartsWith("\"") && MySheet.Range[i + 1, nameColumn].Value.EndsWith("\""))
            //                                { englishName = MySheet.Range[i + 1, nameColumn].Value.Replace("\"", ""); i += 1; }
            //                                else
            //                                    englishName = MySheet.Range[i, nameColumn].Value.Replace("\"", "");
            //                                string japaneseName = englishName;
            //                                if (!IsNumeric(MySheet.Range[i + 1, nameColumn - 1].Value))
            //                                    japaneseName = MySheet.Range[i + 1, nameColumn].Value.Substring(MySheet.Range[i + 1, nameColumn].Value.ToLower().IndexOf("japanese:") + 10).Replace(")", "");
            //                                int rowIndex = Episodes_DataSource.Rows.IndexOf(Episodes_DataSource.Select("episode_number=" + seasonNumber).FirstOrDefault());
            //                                if (rowIndex >= 0)
            //                                {
            //                                    Episodes_DataSource.Rows[rowIndex][3] = englishName;
            //                                    Episodes_DataSource.Rows[rowIndex][4] = japaneseName;
            //                                    //Episodes_DataSource.Rows[rowIndex][7] = "";
            //                                }
            //                                else
            //                                {
            //                                    Episodes_DataSource.Rows.Add(new object[] { the_anime_id, the_season_number, seasonNumber, englishName, japaneseName, (long)1, (long)1, "" });

            //                                    //Qualities
            //                                    ((DataGridViewComboBoxCell)dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[9]).Value = (long?)1;

            //                                    //Sources
            //                                    ((DataGridViewComboBoxCell)dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[10]).Value = (long?)1;
            //                                }
            //                            }
            //                        }
            //                        i += 1;
            //                    }
            //                }
            //            }
            //            MySheet.Dispose();
            //        }
            //    }
            //    catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber() + " from: Season Episodes", "Error"); }
            //    finally { loadingImage.Visible = false; }
        }

            private void MainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //ENG_Season_Name.Width = (dataGridView.Width - 6) / 2;
                //JPN_Season_Name.Left = ENG_Season_Name.Left + ENG_Season_Name.Width + 6;
                //JPN_Season_Name.Width = ENG_Season_Name.Width;
                //Save_Button.Width = ENG_Season_Name.Width;
                //From_Excel_Button.Left = Save_Button.Left + Save_Button.Width + 6;
                //From_Excel_Button.Width = ENG_Season_Name.Width;
            }
            catch (Exception) { }
        }

        private void Season_episodes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { Episodes_DataSource.Dispose(); } catch (Exception) { }
        }

        private void Season_episodes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            GC.Collect();
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).BeginEdit(true);
        }

        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).HitTest(e.X, e.Y).Type == DataGridViewHitTestType.None)
                { ((DataGridView)sender).EndEdit(); ((DataGridView)sender).ClearSelection(); }
            }
            catch (Exception) { }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && ((DataGridView)sender).SelectedRows.Count > 0 && !((DataGridView)sender).SelectedRows.Contains(((DataGridView)sender).Rows[((DataGridView)sender).Rows.Count - 1]))
                {
                    if (MessageBox.Show("Sure to delete record(s)?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in ((DataGridView)sender).SelectedRows)
                            Episodes_DataSource.Rows.RemoveAt(row.Index);
                    }
                }
            }
            catch (Exception) { }
        }

        private void SaveBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                loadingImage.BeginInvoke(new Action(() => loadingImage.Visible = true));
                //Formalizing names
                ENG_Season_Name.Text = ENG_Season_Name.Text.Replace("'", "\'");
                ENG_Season_Name.Text = ENG_Season_Name.Text.Replace(":", " - ");
                ENG_Season_Name.Text = ENG_Season_Name.Text.Replace(": ", " - ");
                ENG_Season_Name.Text = ENG_Season_Name.Text.Replace(" :", " - ");
                ENG_Season_Name.Text = ENG_Season_Name.Text.Replace(" : ", " - ");
                ENG_Season_Name.Text = ENG_Season_Name.Text.Replace(@"""", "");
                ENG_Season_Name.Text = (new CultureInfo("en-US", false).TextInfo).ToTitleCase(ENG_Season_Name.Text);
                JPN_Season_Name.Text = JPN_Season_Name.Text.Replace(":", " - ");
                JPN_Season_Name.Text = JPN_Season_Name.Text.Replace(": ", " - ");
                JPN_Season_Name.Text = JPN_Season_Name.Text.Replace(" :", " - ");
                JPN_Season_Name.Text = JPN_Season_Name.Text.Replace(" : ", " - ");
                JPN_Season_Name.Text = JPN_Season_Name.Text.Replace(@"""", "");
                JPN_Season_Name.Text = JPN_Season_Name.Text.Replace("'", "\'");
                JPN_Season_Name.Text = (new CultureInfo("en-US", false).TextInfo).ToTitleCase(JPN_Season_Name.Text);

                //Updating season
                Season.season_english_name = ENG_Season_Name.Text;
                Season.season_japanese_name = JPN_Season_Name.Text;
                Season.episode_count = dataGridView.Rows.Count - 1;
                if (database.updateObjectToDatabase(Season))
                {
                    int errorCount = 0;
                    foreach (DataRow row in Episodes_DataSource.Rows)
                    {
                        //english name
                        string englishName = (string)row[3];
                        englishName = englishName.Replace("'", "''");

                        //japanese name
                        string japaneseName = (string)row[4];
                        japaneseName = japaneseName.Replace("'", "''");

                        if (Episodes.Find(x => x.episode_number == (decimal)row[2]) != null)
                        {
                            if (!database.updateObjectToDatabase(new anime_episode() { anime_id = the_anime_id, season_number = the_season_number, episode_number = (decimal)row[2], english_name = englishName, japanese_name = japaneseName, encoder = (long)row[5], quality = (long)row[6], source = (long)row[7], torrent_file = (string)row[8] }))
                                errorCount += 1;
                        }
                        else
                        {
                            if (!database.insertObjectToDatabase(new anime_episode() { anime_id = the_anime_id, season_number = the_season_number, episode_number = (decimal)row[2], english_name = englishName, japanese_name = japaneseName, encoder = (long)row[5], quality = (long)row[6], source = (long)row[7], torrent_file = (string)row[8] }))
                                errorCount += 1;
                        }
                    }
                    if (errorCount > 0)
                        Save_Button.Tag = "red";
                    else
                        Save_Button.Tag = "green";
                    getEpisodes.RunWorkerAsync();
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }
        public void setButtonColors()
        {
            try
            {
                //Save Button
                if ((string)Save_Button.Tag == "green")
                { Save_Button.BackColor = Properties.Settings.Default.green; Save_Button.Tag = "blue"; }
                else if ((string)Save_Button.Tag == "red")
                { Save_Button.BackColor = Properties.Settings.Default.red; Save_Button.Tag = "blue"; }
                else if ((string)Save_Button.Tag == "blue")
                    Save_Button.BackColor = Properties.Settings.Default.blue;
                //From Excel
                if ((string)From_Excel_Button.Tag == "green")
                { From_Excel_Button.BackColor = Properties.Settings.Default.green; From_Excel_Button.Tag = "blue"; }
                else if ((string)From_Excel_Button.Tag == "red")
                { From_Excel_Button.BackColor = Properties.Settings.Default.red; From_Excel_Button.Tag = "blue"; }
                else if ((string)From_Excel_Button.Tag == "blue")
                    From_Excel_Button.BackColor = Properties.Settings.Default.blue;
                //batchUpload
                if ((string)batchUpload.Tag == "green")
                { batchUpload.BackColor = Properties.Settings.Default.green; batchUpload.Tag = "blue"; }
                else if ((string)batchUpload.Tag == "red")
                { batchUpload.BackColor = Properties.Settings.Default.red; batchUpload.Tag = "blue"; }
                else if ((string)batchUpload.Tag == "blue")
                    batchUpload.BackColor = Properties.Settings.Default.blue;
                //batchDownload
                if ((string)batchDownload.Tag == "green")
                { batchDownload.BackColor = Properties.Settings.Default.green; batchDownload.Tag = "blue"; }
                else if ((string)batchDownload.Tag == "red")
                { batchDownload.BackColor = Properties.Settings.Default.red; batchDownload.Tag = "blue"; }
                else if ((string)batchDownload.Tag == "blue")
                    batchDownload.BackColor = Properties.Settings.Default.blue;
                //rangeUpload
                if ((string)uploadRange.Tag == "green")
                { uploadRange.BackColor = Properties.Settings.Default.green; uploadRange.Tag = "blue"; }
                else if ((string)uploadRange.Tag == "red")
                { uploadRange.BackColor = Properties.Settings.Default.red; uploadRange.Tag = "blue"; }
                else if ((string)uploadRange.Tag == "blue")
                    uploadRange.BackColor = Properties.Settings.Default.blue;
                //rangeDownload
                if ((string)downloadRange.Tag == "green")
                { downloadRange.BackColor = Properties.Settings.Default.green; downloadRange.Tag = "blue"; }
                else if ((string)downloadRange.Tag == "red")
                { downloadRange.BackColor = Properties.Settings.Default.red; downloadRange.Tag = "blue"; }
                else if ((string)downloadRange.Tag == "blue")
                    downloadRange.BackColor = Properties.Settings.Default.blue;
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber() + " from: Season Episodes", msgType.error); }
        }

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).Tag = "bound complete";
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
                {
                    ((DataGridView)sender).BeginEdit(true);
                    ((ComboBox)((DataGridView)sender).EditingControl).DroppedDown = true;
                }
            }
            catch (Exception) { }
        }

        private void refreshGrid()
        {
            try
            {
                //Refreshing
                dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView.Refresh();
            }
            catch (Exception) { }
        }

        private void GetFromExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Refreshing
            refreshGrid();
            dataGridView.ScrollBars = ScrollBars.Vertical;
        }

        private void GetEpisodes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshGrid();
            dataGridView.ScrollBars = ScrollBars.Vertical;
            Text = Text.Substring(0, Text.IndexOf("->") + 3) + Season.season_english_name;
            if (Season.torrent_file != null && Season.torrent_file != "")
            { batchDownload.Enabled = true; batchDownload.BackColor = Properties.Settings.Default.blue; }
            else
            { batchDownload.Enabled = false; batchDownload.BackColor = Color.LightSlateGray; }
            Refresh();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 11)//upload
                {
                    Open_File_Dialog.Filter = "Torrent Files|*.torrent";
                    if (Open_File_Dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            ((DataGridView)sender).Rows[e.RowIndex].Cells[8].Value = Convert.ToBase64String(File.ReadAllBytes(Open_File_Dialog.FileName));
                        }
                        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    }
                }
                else if (e.ColumnIndex == 12 && (((DataGridViewDisableButtonCell)((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex]).Enabled == true))//download
                {
                    Save_File_Dialog.FileName = Text.Substring(0, Text.IndexOf("->") - 1) + " S" + Season.season_number?.ToString("D2") + "E" + ((decimal)((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value).ToString("00") + ".torrent";
                    Save_File_Dialog.Filter = "Torrent File|*.torrent";
                    if (Save_File_Dialog.ShowDialog() == DialogResult.OK)
                        File.WriteAllBytes(Save_File_Dialog.FileName, Convert.FromBase64String((string)((DataGridView)sender).Rows[e.RowIndex].Cells[8].Value));
                }
            }
            catch (Exception) { }
        }

        private void UploadRange_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(startIndex.Text) > 0 && Convert.ToInt64(endIndex.Text) >= Convert.ToInt64(startIndex.Text))
                {
                    Open_File_Dialog.Multiselect = true;
                    Open_File_Dialog.Filter = "Torrent File|*.torrent";
                    if (Open_File_Dialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string fName in Open_File_Dialog.FileNames)
                            dataGridView.Rows[Array.IndexOf(Open_File_Dialog.FileNames, fName)].Cells[7].Value = Convert.ToBase64String(File.ReadAllBytes(fName));
                        uploadRange.BackColor = Properties.Settings.Default.green;
                    }
                }
            }
            catch (Exception) { uploadRange.BackColor = Properties.Settings.Default.red; }
        }

        private void downloadRange_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(startIndex.Text) > 0 && Convert.ToInt64(endIndex.Text) >= Convert.ToInt64(startIndex.Text))
                {
                    using (FolderBrowserDialog fBrowser = new FolderBrowserDialog())
                    {
                        if (fBrowser.ShowDialog() == DialogResult.OK)
                        {
                            string path = fBrowser.SelectedPath + @"\";
                            bool toOverwrite = false;
                            if (!Directory.Exists(path + Text.Substring(0, Text.IndexOf("->") - 1) + " Season " + Season.season_number?.ToString("D2") + @" torrent files\"))
                            {
                                if (MessageBox.Show("Create subfolder for this season in this location?", "Create subfolder?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    path = path + Text.Substring(0, Text.IndexOf("->") - 1) + " Season " + Season.season_number?.ToString("D2") + @" torrent files\";
                                    Directory.CreateDirectory(path);
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Season torrent folder exists. Save in this folder?", "Folder exists", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    path = path + Text.Substring(0, Text.IndexOf("->") - 1) + " Season " + Season.season_number?.ToString("D2") + @" torrent files\";
                            }
                            if (Directory.GetFiles(path, Text.Substring(0, Text.IndexOf("->") - 1) + " S??E??.torrent").Length > 0)
                            {
                                if (MessageBox.Show("Overwrite existing files?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    toOverwrite = true;
                                else
                                    toOverwrite = false;
                            }
                            foreach (DataGridViewRow row in dataGridView.Rows)
                            {
                                if (row.Cells[7].Value != null && row.Cells[7].Value.ToString() != "")
                                {
                                    if (!toOverwrite)
                                    {
                                        if (Directory.GetFiles(path, Text.Substring(0, Text.IndexOf("->") - 1) + " S" + Season.season_number?.ToString("D2") + "E" + ((decimal)row.Cells[2].Value).ToString("00") + ".torrent").Length > 0)
                                            File.WriteAllBytes(path + Text.Substring(0, Text.IndexOf("->") - 1) + " S" + Season.season_number?.ToString("D2") + "E" + ((decimal)row.Cells[2].Value).ToString("00") + "(" + (Directory.GetFiles(path, Text.Substring(0, Text.IndexOf("->") - 1) + " S" + Season.season_number?.ToString("D2") + "E" + ((decimal)row.Cells[2].Value).ToString("00") + ".torrent").Length + 1) + ").torrent", Convert.FromBase64String(row.Cells[7].Value.ToString()));
                                        else
                                            File.WriteAllBytes(path + Text.Substring(0, Text.IndexOf("->") - 1) + " S" + Season.season_number?.ToString("D2") + "E" + ((decimal)row.Cells[2].Value).ToString("00") + ".torrent", Convert.FromBase64String(row.Cells[7].Value.ToString()));
                                    }
                                    else
                                        File.WriteAllBytes(path + Text.Substring(0, Text.IndexOf("->") - 1) + " S" + Season.season_number?.ToString("D2") + "E" + ((decimal)row.Cells[2].Value).ToString("00") + ".torrent", Convert.FromBase64String(row.Cells[7].Value.ToString()));
                                }

                            }
                            downloadRange.BackColor = Properties.Settings.Default.green;
                        }
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); downloadRange.BackColor = Properties.Settings.Default.red; }
        }

        private void startIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                    e.Handled = true;
            }
            catch (Exception) { }
        }

        private void endIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                    e.Handled = true;
            }
            catch (Exception) { }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                Save_Button.Enabled = false;
                Save_Button.BackColor = Color.LightSlateGray;
            }
            catch (Exception) { }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Save_Button.Enabled = true;
                Save_Button.BackColor = Properties.Settings.Default.blue;
            }
            catch (Exception) { }
        }

        private void batchUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Open_File_Dialog.Multiselect = false;
                Open_File_Dialog.Filter = "Torrent File|*.torrent";
                if (Open_File_Dialog.ShowDialog() == DialogResult.OK)
                {
                    Season.torrent_file = Convert.ToBase64String(File.ReadAllBytes(Open_File_Dialog.FileName));
                    if (database.updateObjectToDatabase(Season))
                        batchUpload.Tag = "green";
                    else
                        batchUpload.Tag = "red";
                }
            }
            catch (Exception) { batchUpload.BackColor = Properties.Settings.Default.red; }
        }

        private void batchDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Season.torrent_file != null && Season.torrent_file != "")
                {
                    Save_File_Dialog.FileName = Text.Substring(0, Text.IndexOf("->") - 1) + Season.season_english_name + " (batch)" + ".torrent";
                    Save_File_Dialog.Filter = "Torrent File|*.torrent";
                    if (Save_File_Dialog.ShowDialog() == DialogResult.OK)
                        File.WriteAllBytes(Save_File_Dialog.FileName, Convert.FromBase64String(Season.torrent_file));
                    batchDownload.BackColor = Properties.Settings.Default.green;
                }
            }
            catch (Exception) { batchDownload.BackColor = Properties.Settings.Default.red; }
        }
    }
}
