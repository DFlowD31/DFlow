using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using LazyPortal.services;

namespace LazyPortal
{
    public partial class anime_record : MetroFramework.Forms.MetroForm
    {
        private long? selected_Anime_ID = null;
        private List<anime> Animes = new List<anime>();
        //private readonly database database = new database();
        private List<anime_season> Seasons = new List<anime_season>();
        private readonly Dictionary<decimal?, string> Season_List_DataSource = new Dictionary<decimal?, string>();
        private readonly Dictionary<Nullable<Int64>, string> Anime_List_DataSource = new Dictionary<Nullable<Int64>, string>();


        public anime_record()
        {
            InitializeComponent();
            loadBackground.RunWorkerAsync();
        }
        public anime_record(long? select_anime = null)
        {
            InitializeComponent();
            loadBackground.RunWorkerAsync(select_anime);
        }

        private static long ToInt64(string value)
        {
            if (value == null || value == "")
                return 0;
            return long.Parse(value, (IFormatProvider)CultureInfo.CurrentCulture);
        }
        private void Add_Season_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(selected_Anime_ID.ToString());
                long latest_number = ToInt64(database.Get_From_Database("SELECT `season_number` FROM `anime_seasons` WHERE `anime_id` = " + selected_Anime_ID + " ORDER BY `season_number` DESC")) + 1;
                if (database.insertObjectToDatabase(new anime_season() { anime_id = selected_Anime_ID, season_number = latest_number, season_english_name = "Season " + latest_number.ToString("D2"), season_japanese_name = "第 " + latest_number + " 期" }))
                { ((Button)sender).Tag = "green"; Refresh_Seasons(selected_Anime_ID); }
                else
                    ((Button)sender).Tag = "red";
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (((Button)sender).Text == "Save")
                {
                    if (database.updateObjectToDatabase(Animes.Find(x => x.id == selected_Anime_ID)))
                    { ((Button)sender).Tag = "green"; loadBackground.RunWorkerAsync(); }
                    else
                        ((Button)sender).Tag = "red";
                }
                else if (((Button)sender).Text == "Create" && ENG_Name.Text != "" && JPN_Name.Text != "")
                {
                    if (database.insertObjectToDatabase(new anime() { english_name = ENG_Name.Text, japanese_name = JPN_Name.Text }))
                    { ((Button)sender).Tag = "green"; loadBackground.RunWorkerAsync(); }
                    else
                        ((Button)sender).Tag = "red";
                }
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        public void Refresh_Seasons(long? anime_id)
        {
            try
            {
                loadingImage.BeginInvoke(new Action(() => { loadingImage.Visible = true; }));
                Season_List_DataSource.Clear();
                Seasons = (List<anime_season>)database.getObjectFromDatabase<anime_season>(new anime_season() { anime_id = anime_id });
                if (Seasons.Count > 0)
                {
                    foreach (anime_season season in Seasons)
                        Season_List_DataSource.Add(season.season_number, season.season_english_name);
                    Season_List.BeginInvoke(new Action(() =>
                    {
                        Season_List.DataSource = new BindingSource(Season_List_DataSource, null);
                        Season_List.ValueMember = "Key";
                        Season_List.DisplayMember = "Value";
                        Season_List.ClearSelected();
                    }));
                }
                else
                    Season_List.DataSource = null;
                selected_Anime_ID = anime_id;
                Save_Button.Text = "Save";
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally { setButtonColors(); loadingImage.BeginInvoke(new Action(() => { loadingImage.Visible = false; })); }
        }

        private void Season_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                new season_episodes((long)selected_Anime_ID, Convert.ToInt64(((ListBox)sender).SelectedValue)) { Text = Animes.Find(x => x.id == Convert.ToInt64(Anime_List.SelectedValue)).english_name + " -> " + Seasons.Find(x => x.season_number == Convert.ToInt64(((ListBox)sender).SelectedValue)).season_english_name }.Show();
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        private void LoadBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!Anime_List.IsHandleCreated) { }
                loadingImage.BeginInvoke(new Action(() => { loadingImage.Visible = true; }));
                Animes = (List<anime>)database.getObjectFromDatabase<anime>();
                Anime_List_DataSource.Clear();
                if (Animes.Count > 0)
                {
                    foreach (anime anime in Animes)
                        Anime_List_DataSource.Add(anime.id, anime.english_name);
                    Anime_List.BeginInvoke(new Action(() =>
                    {
                        Anime_List.DataSource = new BindingSource(Anime_List_DataSource, null);
                        Anime_List.ValueMember = "Key";
                        Anime_List.DisplayMember = "Value";
                        //int selected_index = Anime_List_DataSource.Values.ToList().IndexOf(Anime_List_DataSource.Where(pair => pair.Key == (long?)e.Argument).Select(pair => pair.Value).FirstOrDefault());
                        if (e.Argument != null)
                            setAnime((long?)e.Argument);
                        else
                            Anime_List.ClearSelected();
                    }));
                }
                else
                    Anime_List.DataSource = null;
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
            finally
            {
                setButtonColors();
                loadingImage.BeginInvoke(new Action(() => { loadingImage.Visible = false; }));
            }
        }

        public void setButtonColors()
        {
            try
            {
                if ((string)Save_Button.Tag == "green")
                { Save_Button.BackColor = Properties.Settings.Default.green; Save_Button.Tag = "blue"; }
                else if ((string)Save_Button.Tag == "red")
                { Save_Button.BackColor = Properties.Settings.Default.red; Save_Button.Tag = "blue"; }
                else
                    Save_Button.BackColor = Properties.Settings.Default.blue;
                if ((string)Add_Season.Tag == "green")
                { Add_Season.BackColor = Properties.Settings.Default.green; Add_Season.Tag = "blue"; }
                else if ((string)Add_Season.Tag == "red")
                { Add_Season.BackColor = Properties.Settings.Default.red; Add_Season.Tag = "blue"; }
                else
                    Add_Season.BackColor = Properties.Settings.Default.blue;
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        public void setAnime(long? select_ID)
        {
            try
            {
                ENG_Name.Text = Animes.Find(x => x.id == select_ID).english_name;
                JPN_Name.Text = Animes.Find(x => x.id == select_ID).japanese_name;
                Refresh_Seasons(select_ID);
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        private void Anime_List_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (((ListBox)sender).IndexFromPoint(new Point(e.X, e.Y)) > -1)
                    setAnime((long?)((ListBox)sender).SelectedValue);
                else
                {
                    ((ListBox)sender).ClearSelected();
                    Season_List.DataSource = null;
                    selected_Anime_ID = null;
                    ENG_Name.Text = "";
                    JPN_Name.Text = "";
                    Save_Button.Text = "Create";
                    setButtonColors();
                }
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }

        private void Anime_record_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            GC.Collect();
        }

        private void Season_List_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (((ListBox)sender).IndexFromPoint(new Point(e.X, e.Y)) <= -1)
                {
                    ((ListBox)sender).ClearSelected();
                    setButtonColors();
                }
            }
            catch (Exception ex) { Program.Main_Form.log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), msgType.error); }
        }
    }
}