namespace LazyPortal
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Obsolete]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.anime_browse_btn = new System.Windows.Forms.Button();
            this.Log_Panel = new System.Windows.Forms.Panel();
            this.log_richtextbox = new System.Windows.Forms.RichTextBox();
            this.movie_folder_poster_btn = new System.Windows.Forms.Button();
            this.ProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.Mini_ProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.Destination = new MetroFramework.Controls.MetroTextBox();
            this.anime_rename_btn = new System.Windows.Forms.Button();
            this.music_rename_btn = new System.Windows.Forms.Button();
            this.merge_destination_browse_btn = new System.Windows.Forms.Button();
            this.Merge_Background = new System.ComponentModel.BackgroundWorker();
            this.main_timer = new System.Windows.Forms.Timer(this.components);
            this.Notification_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Status_Text = new System.Windows.Forms.Label();
            this.merge_movie_btn = new System.Windows.Forms.Button();
            this.new_function_test_btn = new System.Windows.Forms.Button();
            this.timer_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.Folder_Browser_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.Date_Time_Picker = new System.Windows.Forms.DateTimePicker();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            this.Open_File_Dialog = new System.Windows.Forms.OpenFileDialog();
            this.movie_file_poster_btn = new System.Windows.Forms.Button();
            this.poster_background_worker = new System.ComponentModel.BackgroundWorker();
            this.Log_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // anime_browse_btn
            // 
            this.anime_browse_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.anime_browse_btn.FlatAppearance.BorderSize = 0;
            this.anime_browse_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anime_browse_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.anime_browse_btn.ForeColor = System.Drawing.Color.White;
            this.anime_browse_btn.Location = new System.Drawing.Point(111, 150);
            this.anime_browse_btn.Name = "anime_browse_btn";
            this.anime_browse_btn.Size = new System.Drawing.Size(82, 52);
            this.anime_browse_btn.TabIndex = 52;
            this.anime_browse_btn.Text = "Anime Browse";
            this.anime_browse_btn.UseVisualStyleBackColor = false;
            this.anime_browse_btn.Click += new System.EventHandler(this.anime_browse_btn_Click);
            // 
            // Log_Panel
            // 
            this.Log_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Log_Panel.Controls.Add(this.log_richtextbox);
            this.Log_Panel.Location = new System.Drawing.Point(289, 63);
            this.Log_Panel.Name = "Log_Panel";
            this.Log_Panel.Padding = new System.Windows.Forms.Padding(1);
            this.Log_Panel.Size = new System.Drawing.Size(611, 319);
            this.Log_Panel.TabIndex = 50;
            // 
            // log_richtextbox
            // 
            this.log_richtextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log_richtextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log_richtextbox.ForeColor = System.Drawing.Color.DarkOrange;
            this.log_richtextbox.Location = new System.Drawing.Point(1, 1);
            this.log_richtextbox.Margin = new System.Windows.Forms.Padding(1);
            this.log_richtextbox.Name = "log_richtextbox";
            this.log_richtextbox.ReadOnly = true;
            this.log_richtextbox.Size = new System.Drawing.Size(609, 317);
            this.log_richtextbox.TabIndex = 33;
            this.log_richtextbox.Text = "";
            // 
            // movie_folder_poster_btn
            // 
            this.movie_folder_poster_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.movie_folder_poster_btn.FlatAppearance.BorderSize = 0;
            this.movie_folder_poster_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.movie_folder_poster_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.movie_folder_poster_btn.ForeColor = System.Drawing.Color.White;
            this.movie_folder_poster_btn.Location = new System.Drawing.Point(23, 92);
            this.movie_folder_poster_btn.Name = "movie_folder_poster_btn";
            this.movie_folder_poster_btn.Size = new System.Drawing.Size(127, 52);
            this.movie_folder_poster_btn.TabIndex = 51;
            this.movie_folder_poster_btn.Text = "Folder Poster";
            this.movie_folder_poster_btn.UseVisualStyleBackColor = false;
            this.movie_folder_poster_btn.Click += new System.EventHandler(this.movie_folder_poster_btn_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.FontSize = MetroFramework.MetroProgressBarSize.Small;
            this.ProgressBar.FontWeight = MetroFramework.MetroProgressBarWeight.Regular;
            this.ProgressBar.HideProgressText = false;
            this.ProgressBar.Location = new System.Drawing.Point(23, 325);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(260, 25);
            this.ProgressBar.Style = MetroFramework.MetroColorStyle.Blue;
            this.ProgressBar.TabIndex = 49;
            this.ProgressBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ProgressBar.UseCustomBackColor = true;
            // 
            // Mini_ProgressBar
            // 
            this.Mini_ProgressBar.FontSize = MetroFramework.MetroProgressBarSize.Small;
            this.Mini_ProgressBar.FontWeight = MetroFramework.MetroProgressBarWeight.Regular;
            this.Mini_ProgressBar.HideProgressText = false;
            this.Mini_ProgressBar.Location = new System.Drawing.Point(23, 294);
            this.Mini_ProgressBar.Name = "Mini_ProgressBar";
            this.Mini_ProgressBar.Size = new System.Drawing.Size(260, 25);
            this.Mini_ProgressBar.Style = MetroFramework.MetroColorStyle.Blue;
            this.Mini_ProgressBar.TabIndex = 48;
            this.Mini_ProgressBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Mini_ProgressBar.UseCustomBackColor = true;
            // 
            // Destination
            // 
            // 
            // 
            // 
            this.Destination.CustomButton.Image = null;
            this.Destination.CustomButton.Location = new System.Drawing.Point(181, 2);
            this.Destination.CustomButton.Name = "";
            this.Destination.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.Destination.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Destination.CustomButton.TabIndex = 1;
            this.Destination.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Destination.CustomButton.UseSelectable = true;
            this.Destination.CustomButton.Visible = false;
            this.Destination.Lines = new string[0];
            this.Destination.Location = new System.Drawing.Point(23, 208);
            this.Destination.MaxLength = 32767;
            this.Destination.Name = "Destination";
            this.Destination.PasswordChar = '\0';
            this.Destination.ReadOnly = true;
            this.Destination.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Destination.SelectedText = "";
            this.Destination.SelectionLength = 0;
            this.Destination.SelectionStart = 0;
            this.Destination.ShortcutsEnabled = true;
            this.Destination.Size = new System.Drawing.Size(201, 22);
            this.Destination.Style = MetroFramework.MetroColorStyle.Blue;
            this.Destination.TabIndex = 47;
            this.Destination.UseSelectable = true;
            this.Destination.UseStyleColors = true;
            this.Destination.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Destination.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // anime_rename_btn
            // 
            this.anime_rename_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.anime_rename_btn.FlatAppearance.BorderSize = 0;
            this.anime_rename_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anime_rename_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.anime_rename_btn.ForeColor = System.Drawing.Color.White;
            this.anime_rename_btn.Location = new System.Drawing.Point(199, 150);
            this.anime_rename_btn.Name = "anime_rename_btn";
            this.anime_rename_btn.Size = new System.Drawing.Size(84, 52);
            this.anime_rename_btn.TabIndex = 46;
            this.anime_rename_btn.Text = "Anime Rename";
            this.anime_rename_btn.UseVisualStyleBackColor = false;
            this.anime_rename_btn.Click += new System.EventHandler(this.anime_rename_btn_Click);
            // 
            // music_rename_btn
            // 
            this.music_rename_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.music_rename_btn.FlatAppearance.BorderSize = 0;
            this.music_rename_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.music_rename_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.music_rename_btn.ForeColor = System.Drawing.Color.White;
            this.music_rename_btn.Location = new System.Drawing.Point(23, 150);
            this.music_rename_btn.Name = "music_rename_btn";
            this.music_rename_btn.Size = new System.Drawing.Size(82, 52);
            this.music_rename_btn.TabIndex = 45;
            this.music_rename_btn.Text = "Music Rename";
            this.music_rename_btn.UseVisualStyleBackColor = false;
            this.music_rename_btn.Click += new System.EventHandler(this.music_rename_btn_Click);
            // 
            // merge_destination_browse_btn
            // 
            this.merge_destination_browse_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.merge_destination_browse_btn.FlatAppearance.BorderSize = 0;
            this.merge_destination_browse_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.merge_destination_browse_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.merge_destination_browse_btn.ForeColor = System.Drawing.Color.White;
            this.merge_destination_browse_btn.Location = new System.Drawing.Point(230, 208);
            this.merge_destination_browse_btn.Name = "merge_destination_browse_btn";
            this.merge_destination_browse_btn.Size = new System.Drawing.Size(53, 22);
            this.merge_destination_browse_btn.TabIndex = 44;
            this.merge_destination_browse_btn.Text = "Browse";
            this.merge_destination_browse_btn.UseVisualStyleBackColor = false;
            this.merge_destination_browse_btn.Click += new System.EventHandler(this.merge_destination_browse_btn_Click);
            // 
            // Merge_Background
            // 
            this.Merge_Background.WorkerSupportsCancellation = true;
            // 
            // main_timer
            // 
            this.main_timer.Interval = 1000;
            this.main_timer.Tick += new System.EventHandler(this.main_timer_Tick);
            // 
            // Notification_Icon
            // 
            this.Notification_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("Notification_Icon.Icon")));
            this.Notification_Icon.Text = "DFlow";
            this.Notification_Icon.Visible = true;
            this.Notification_Icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Notification_Icon_MouseDoubleClick);
            // 
            // Status_Text
            // 
            this.Status_Text.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Status_Text.Location = new System.Drawing.Point(23, 356);
            this.Status_Text.Margin = new System.Windows.Forms.Padding(3);
            this.Status_Text.Name = "Status_Text";
            this.Status_Text.Size = new System.Drawing.Size(260, 26);
            this.Status_Text.TabIndex = 43;
            this.Status_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Status_Text.Visible = false;
            // 
            // merge_movie_btn
            // 
            this.merge_movie_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.merge_movie_btn.FlatAppearance.BorderSize = 0;
            this.merge_movie_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.merge_movie_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.merge_movie_btn.ForeColor = System.Drawing.Color.White;
            this.merge_movie_btn.Location = new System.Drawing.Point(23, 236);
            this.merge_movie_btn.Name = "merge_movie_btn";
            this.merge_movie_btn.Size = new System.Drawing.Size(170, 52);
            this.merge_movie_btn.TabIndex = 42;
            this.merge_movie_btn.Text = "Merge Movies";
            this.merge_movie_btn.UseVisualStyleBackColor = false;
            this.merge_movie_btn.Click += new System.EventHandler(this.Merge_Button_Click);
            // 
            // new_function_test_btn
            // 
            this.new_function_test_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.new_function_test_btn.FlatAppearance.BorderSize = 0;
            this.new_function_test_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.new_function_test_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.new_function_test_btn.ForeColor = System.Drawing.Color.White;
            this.new_function_test_btn.Location = new System.Drawing.Point(199, 236);
            this.new_function_test_btn.Name = "new_function_test_btn";
            this.new_function_test_btn.Size = new System.Drawing.Size(84, 52);
            this.new_function_test_btn.TabIndex = 41;
            this.new_function_test_btn.Text = "New function test";
            this.new_function_test_btn.UseVisualStyleBackColor = false;
            this.new_function_test_btn.Click += new System.EventHandler(this.new_function_test_btn_Click);
            // 
            // timer_btn
            // 
            this.timer_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.timer_btn.FlatAppearance.BorderSize = 0;
            this.timer_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timer_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.timer_btn.ForeColor = System.Drawing.Color.White;
            this.timer_btn.Location = new System.Drawing.Point(156, 63);
            this.timer_btn.Name = "timer_btn";
            this.timer_btn.Size = new System.Drawing.Size(61, 23);
            this.timer_btn.TabIndex = 38;
            this.timer_btn.Text = "Start Timer";
            this.timer_btn.UseVisualStyleBackColor = false;
            this.timer_btn.Click += new System.EventHandler(this.timer_btn_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.exit_btn.ForeColor = System.Drawing.Color.White;
            this.exit_btn.Location = new System.Drawing.Point(223, 63);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(61, 23);
            this.exit_btn.TabIndex = 40;
            this.exit_btn.Text = "Exit";
            this.exit_btn.UseVisualStyleBackColor = false;
            this.exit_btn.Click += new System.EventHandler(this.exit_btn_Click);
            // 
            // Date_Time_Picker
            // 
            this.Date_Time_Picker.CustomFormat = "yyyy-MM-dd hh:mm";
            this.Date_Time_Picker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Date_Time_Picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Time_Picker.Location = new System.Drawing.Point(23, 63);
            this.Date_Time_Picker.Name = "Date_Time_Picker";
            this.Date_Time_Picker.Size = new System.Drawing.Size(127, 23);
            this.Date_Time_Picker.TabIndex = 37;
            // 
            // loadingImage
            // 
            this.loadingImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loadingImage.Image = global::LazyPortal.Properties.Resources._64x64;
            this.loadingImage.ImageLocation = "";
            this.loadingImage.Location = new System.Drawing.Point(23, 63);
            this.loadingImage.Name = "loadingImage";
            this.loadingImage.Size = new System.Drawing.Size(1138, 481);
            this.loadingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingImage.TabIndex = 34;
            this.loadingImage.TabStop = false;
            this.loadingImage.Visible = false;
            // 
            // movie_file_poster_btn
            // 
            this.movie_file_poster_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.movie_file_poster_btn.FlatAppearance.BorderSize = 0;
            this.movie_file_poster_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.movie_file_poster_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.movie_file_poster_btn.ForeColor = System.Drawing.Color.White;
            this.movie_file_poster_btn.Location = new System.Drawing.Point(156, 92);
            this.movie_file_poster_btn.Name = "movie_file_poster_btn";
            this.movie_file_poster_btn.Size = new System.Drawing.Size(127, 52);
            this.movie_file_poster_btn.TabIndex = 53;
            this.movie_file_poster_btn.Text = "File Poster";
            this.movie_file_poster_btn.UseVisualStyleBackColor = false;
            this.movie_file_poster_btn.Click += new System.EventHandler(this.movie_file_poster_btn_Click);
            // 
            // poster_background_worker
            // 
            this.poster_background_worker.WorkerReportsProgress = true;
            this.poster_background_worker.WorkerSupportsCancellation = true;
            this.poster_background_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.main_timer_background_worker_DoWork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 567);
            this.Controls.Add(this.movie_file_poster_btn);
            this.Controls.Add(this.anime_browse_btn);
            this.Controls.Add(this.new_function_test_btn);
            this.Controls.Add(this.Log_Panel);
            this.Controls.Add(this.movie_folder_poster_btn);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.Mini_ProgressBar);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.anime_rename_btn);
            this.Controls.Add(this.music_rename_btn);
            this.Controls.Add(this.merge_destination_browse_btn);
            this.Controls.Add(this.Status_Text);
            this.Controls.Add(this.merge_movie_btn);
            this.Controls.Add(this.timer_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.Date_Time_Picker);
            this.Controls.Add(this.loadingImage);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "LazyPortal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Log_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button anime_browse_btn;
        internal System.Windows.Forms.Panel Log_Panel;
        public System.Windows.Forms.RichTextBox log_richtextbox;
        private System.Windows.Forms.Button movie_folder_poster_btn;
        internal MetroFramework.Controls.MetroProgressBar ProgressBar;
        internal MetroFramework.Controls.MetroProgressBar Mini_ProgressBar;
        internal MetroFramework.Controls.MetroTextBox Destination;
        private System.Windows.Forms.Button anime_rename_btn;
        private System.Windows.Forms.Button music_rename_btn;
        internal System.Windows.Forms.Button merge_destination_browse_btn;
        private System.ComponentModel.BackgroundWorker Merge_Background;
        private System.Windows.Forms.Timer main_timer;
        private System.Windows.Forms.NotifyIcon Notification_Icon;
        private System.Windows.Forms.Label Status_Text;
        private System.Windows.Forms.Button merge_movie_btn;
        private System.Windows.Forms.Button new_function_test_btn;
        private System.Windows.Forms.Button timer_btn;
        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.FolderBrowserDialog Folder_Browser_Dialog;
        internal System.Windows.Forms.DateTimePicker Date_Time_Picker;
        private System.Windows.Forms.PictureBox loadingImage;
        private System.Windows.Forms.OpenFileDialog Open_File_Dialog;
        private System.Windows.Forms.Button movie_file_poster_btn;
        private System.ComponentModel.BackgroundWorker poster_background_worker;
    }
}

