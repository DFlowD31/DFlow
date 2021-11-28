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
            this.Anime_Browse = new System.Windows.Forms.Button();
            this.Log_Panel = new System.Windows.Forms.Panel();
            this.Log_Text = new System.Windows.Forms.RichTextBox();
            this.Poster_btn = new System.Windows.Forms.Button();
            this.ProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.Mini_ProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.Destination = new MetroFramework.Controls.MetroTextBox();
            this.Anime_Button = new System.Windows.Forms.Button();
            this.Music_Button = new System.Windows.Forms.Button();
            this.Browse_Button = new System.Windows.Forms.Button();
            this.Merge_Background = new System.ComponentModel.BackgroundWorker();
            this.Shutdown_Timer = new System.Windows.Forms.Timer(this.components);
            this.Notification_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Status_Text = new System.Windows.Forms.Label();
            this.Merge_Button = new System.Windows.Forms.Button();
            this.Movie_Folder = new System.Windows.Forms.Button();
            this.Timer_Button = new System.Windows.Forms.Button();
            this.Exit_Button = new System.Windows.Forms.Button();
            this.Folder_Browser_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.Date_Time_Picker = new System.Windows.Forms.DateTimePicker();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            this.Open_File_Dialog = new System.Windows.Forms.OpenFileDialog();
            this.Movie_File = new System.Windows.Forms.Button();
            this.Log_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // Anime_Browse
            // 
            this.Anime_Browse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Anime_Browse.FlatAppearance.BorderSize = 0;
            this.Anime_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Anime_Browse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Anime_Browse.ForeColor = System.Drawing.Color.White;
            this.Anime_Browse.Location = new System.Drawing.Point(111, 150);
            this.Anime_Browse.Name = "Anime_Browse";
            this.Anime_Browse.Size = new System.Drawing.Size(82, 52);
            this.Anime_Browse.TabIndex = 52;
            this.Anime_Browse.Text = "Anime Browse";
            this.Anime_Browse.UseVisualStyleBackColor = false;
            this.Anime_Browse.Click += new System.EventHandler(this.Anime_Browse_Click);
            // 
            // Log_Panel
            // 
            this.Log_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Log_Panel.Controls.Add(this.Log_Text);
            this.Log_Panel.Location = new System.Drawing.Point(289, 63);
            this.Log_Panel.Name = "Log_Panel";
            this.Log_Panel.Padding = new System.Windows.Forms.Padding(1);
            this.Log_Panel.Size = new System.Drawing.Size(611, 319);
            this.Log_Panel.TabIndex = 50;
            // 
            // Log_Text
            // 
            this.Log_Text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Log_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log_Text.ForeColor = System.Drawing.Color.DarkOrange;
            this.Log_Text.Location = new System.Drawing.Point(1, 1);
            this.Log_Text.Margin = new System.Windows.Forms.Padding(1);
            this.Log_Text.Name = "Log_Text";
            this.Log_Text.ReadOnly = true;
            this.Log_Text.Size = new System.Drawing.Size(609, 317);
            this.Log_Text.TabIndex = 33;
            this.Log_Text.Text = "";
            // 
            // Poster_btn
            // 
            this.Poster_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Poster_btn.FlatAppearance.BorderSize = 0;
            this.Poster_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Poster_btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Poster_btn.ForeColor = System.Drawing.Color.White;
            this.Poster_btn.Location = new System.Drawing.Point(156, 63);
            this.Poster_btn.Name = "Poster_btn";
            this.Poster_btn.Size = new System.Drawing.Size(127, 23);
            this.Poster_btn.TabIndex = 51;
            this.Poster_btn.Text = "Poster";
            this.Poster_btn.UseVisualStyleBackColor = false;
            this.Poster_btn.Click += new System.EventHandler(this.Poster_Click);
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
            // Anime_Button
            // 
            this.Anime_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Anime_Button.FlatAppearance.BorderSize = 0;
            this.Anime_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Anime_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Anime_Button.ForeColor = System.Drawing.Color.White;
            this.Anime_Button.Location = new System.Drawing.Point(199, 150);
            this.Anime_Button.Name = "Anime_Button";
            this.Anime_Button.Size = new System.Drawing.Size(84, 52);
            this.Anime_Button.TabIndex = 46;
            this.Anime_Button.Text = "Anime Rename";
            this.Anime_Button.UseVisualStyleBackColor = false;
            this.Anime_Button.Click += new System.EventHandler(this.Anime_Button_Click);
            // 
            // Music_Button
            // 
            this.Music_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Music_Button.FlatAppearance.BorderSize = 0;
            this.Music_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Music_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Music_Button.ForeColor = System.Drawing.Color.White;
            this.Music_Button.Location = new System.Drawing.Point(23, 150);
            this.Music_Button.Name = "Music_Button";
            this.Music_Button.Size = new System.Drawing.Size(82, 52);
            this.Music_Button.TabIndex = 45;
            this.Music_Button.Text = "Music Rename";
            this.Music_Button.UseVisualStyleBackColor = false;
            this.Music_Button.Click += new System.EventHandler(this.Music_Button_Click);
            // 
            // Browse_Button
            // 
            this.Browse_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Browse_Button.FlatAppearance.BorderSize = 0;
            this.Browse_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Browse_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Browse_Button.ForeColor = System.Drawing.Color.White;
            this.Browse_Button.Location = new System.Drawing.Point(230, 208);
            this.Browse_Button.Name = "Browse_Button";
            this.Browse_Button.Size = new System.Drawing.Size(53, 22);
            this.Browse_Button.TabIndex = 44;
            this.Browse_Button.Text = "Browse";
            this.Browse_Button.UseVisualStyleBackColor = false;
            this.Browse_Button.Click += new System.EventHandler(this.Browse_Button_Click);
            // 
            // Merge_Background
            // 
            this.Merge_Background.WorkerSupportsCancellation = true;
            // 
            // Shutdown_Timer
            // 
            this.Shutdown_Timer.Interval = 1000;
            this.Shutdown_Timer.Tick += new System.EventHandler(this.Timer_Tick);
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
            // Merge_Button
            // 
            this.Merge_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Merge_Button.FlatAppearance.BorderSize = 0;
            this.Merge_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Merge_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Merge_Button.ForeColor = System.Drawing.Color.White;
            this.Merge_Button.Location = new System.Drawing.Point(23, 236);
            this.Merge_Button.Name = "Merge_Button";
            this.Merge_Button.Size = new System.Drawing.Size(170, 52);
            this.Merge_Button.TabIndex = 42;
            this.Merge_Button.Text = "Merge Movies";
            this.Merge_Button.UseVisualStyleBackColor = false;
            this.Merge_Button.Click += new System.EventHandler(this.Merge_Button_Click);
            // 
            // Movie_Folder
            // 
            this.Movie_Folder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Movie_Folder.FlatAppearance.BorderSize = 0;
            this.Movie_Folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Movie_Folder.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.Movie_Folder.ForeColor = System.Drawing.Color.White;
            this.Movie_Folder.Location = new System.Drawing.Point(199, 236);
            this.Movie_Folder.Name = "Movie_Folder";
            this.Movie_Folder.Size = new System.Drawing.Size(84, 23);
            this.Movie_Folder.TabIndex = 41;
            this.Movie_Folder.Text = "Folder";
            this.Movie_Folder.UseVisualStyleBackColor = false;
            this.Movie_Folder.Click += new System.EventHandler(this.Movie_Folder_Click);
            // 
            // Timer_Button
            // 
            this.Timer_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Timer_Button.FlatAppearance.BorderSize = 0;
            this.Timer_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Timer_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Timer_Button.ForeColor = System.Drawing.Color.White;
            this.Timer_Button.Location = new System.Drawing.Point(23, 92);
            this.Timer_Button.Name = "Timer_Button";
            this.Timer_Button.Size = new System.Drawing.Size(127, 52);
            this.Timer_Button.TabIndex = 38;
            this.Timer_Button.Text = "Start Timer";
            this.Timer_Button.UseVisualStyleBackColor = false;
            this.Timer_Button.Click += new System.EventHandler(this.Timer_Button_Click);
            // 
            // Exit_Button
            // 
            this.Exit_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Exit_Button.FlatAppearance.BorderSize = 0;
            this.Exit_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Exit_Button.ForeColor = System.Drawing.Color.White;
            this.Exit_Button.Location = new System.Drawing.Point(156, 92);
            this.Exit_Button.Name = "Exit_Button";
            this.Exit_Button.Size = new System.Drawing.Size(127, 52);
            this.Exit_Button.TabIndex = 40;
            this.Exit_Button.Text = "Exit";
            this.Exit_Button.UseVisualStyleBackColor = false;
            this.Exit_Button.Click += new System.EventHandler(this.Exit_Button_Click);
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
            // Movie_File
            // 
            this.Movie_File.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Movie_File.FlatAppearance.BorderSize = 0;
            this.Movie_File.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Movie_File.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.Movie_File.ForeColor = System.Drawing.Color.White;
            this.Movie_File.Location = new System.Drawing.Point(199, 265);
            this.Movie_File.Name = "Movie_File";
            this.Movie_File.Size = new System.Drawing.Size(84, 23);
            this.Movie_File.TabIndex = 53;
            this.Movie_File.Text = "File";
            this.Movie_File.UseVisualStyleBackColor = false;
            this.Movie_File.Click += new System.EventHandler(this.Movie_File_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 567);
            this.Controls.Add(this.Movie_File);
            this.Controls.Add(this.Anime_Browse);
            this.Controls.Add(this.Movie_Folder);
            this.Controls.Add(this.Log_Panel);
            this.Controls.Add(this.Poster_btn);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.Mini_ProgressBar);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.Anime_Button);
            this.Controls.Add(this.Music_Button);
            this.Controls.Add(this.Browse_Button);
            this.Controls.Add(this.Status_Text);
            this.Controls.Add(this.Merge_Button);
            this.Controls.Add(this.Timer_Button);
            this.Controls.Add(this.Exit_Button);
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

        internal System.Windows.Forms.Button Anime_Browse;
        internal System.Windows.Forms.Panel Log_Panel;
        public System.Windows.Forms.RichTextBox Log_Text;
        private System.Windows.Forms.Button Poster_btn;
        internal MetroFramework.Controls.MetroProgressBar ProgressBar;
        internal MetroFramework.Controls.MetroProgressBar Mini_ProgressBar;
        internal MetroFramework.Controls.MetroTextBox Destination;
        private System.Windows.Forms.Button Anime_Button;
        private System.Windows.Forms.Button Music_Button;
        internal System.Windows.Forms.Button Browse_Button;
        private System.ComponentModel.BackgroundWorker Merge_Background;
        private System.Windows.Forms.Timer Shutdown_Timer;
        private System.Windows.Forms.NotifyIcon Notification_Icon;
        private System.Windows.Forms.Label Status_Text;
        private System.Windows.Forms.Button Merge_Button;
        private System.Windows.Forms.Button Movie_Folder;
        private System.Windows.Forms.Button Timer_Button;
        private System.Windows.Forms.Button Exit_Button;
        private System.Windows.Forms.FolderBrowserDialog Folder_Browser_Dialog;
        internal System.Windows.Forms.DateTimePicker Date_Time_Picker;
        private System.Windows.Forms.PictureBox loadingImage;
        private System.Windows.Forms.OpenFileDialog Open_File_Dialog;
        private System.Windows.Forms.Button Movie_File;
    }
}

