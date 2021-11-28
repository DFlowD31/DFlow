namespace LazyPortal
{
    partial class movie_record
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
        private void InitializeComponent()
        {
            this.IMDb_Text = new MetroFramework.Controls.MetroTextBox();
            this.nameText = new MetroFramework.Controls.MetroTextBox();
            this.sourceComboBox = new MetroFramework.Controls.MetroComboBox();
            this.qualityComboBox = new MetroFramework.Controls.MetroComboBox();
            this.torrentUpload = new System.Windows.Forms.Button();
            this.torrentDownload = new System.Windows.Forms.Button();
            this.subtitleLink = new MetroFramework.Controls.MetroTextBox();
            this.encoderComboBox = new MetroFramework.Controls.MetroComboBox();
            this.sizeText = new MetroFramework.Controls.MetroTextBox();
            this.typeText = new MetroFramework.Controls.MetroTextBox();
            this.audioCodecComboBox = new MetroFramework.Controls.MetroComboBox();
            this.audioChannelComboBox = new MetroFramework.Controls.MetroComboBox();
            this.videoCodecComboBox = new MetroFramework.Controls.MetroComboBox();
            this.videoBitrateText = new MetroFramework.Controls.MetroTextBox();
            this.loadBackground = new System.ComponentModel.BackgroundWorker();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.fromFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // IMDb_Text
            // 
            // 
            // 
            // 
            this.IMDb_Text.CustomButton.Image = null;
            this.IMDb_Text.CustomButton.Location = new System.Drawing.Point(352, 1);
            this.IMDb_Text.CustomButton.Name = "";
            this.IMDb_Text.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.IMDb_Text.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.IMDb_Text.CustomButton.TabIndex = 1;
            this.IMDb_Text.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.IMDb_Text.CustomButton.UseSelectable = true;
            this.IMDb_Text.CustomButton.Visible = false;
            this.IMDb_Text.Lines = new string[0];
            this.IMDb_Text.Location = new System.Drawing.Point(23, 162);
            this.IMDb_Text.MaxLength = 32767;
            this.IMDb_Text.Name = "IMDb_Text";
            this.IMDb_Text.PasswordChar = '\0';
            this.IMDb_Text.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.IMDb_Text.SelectedText = "";
            this.IMDb_Text.SelectionLength = 0;
            this.IMDb_Text.SelectionStart = 0;
            this.IMDb_Text.ShortcutsEnabled = true;
            this.IMDb_Text.Size = new System.Drawing.Size(374, 23);
            this.IMDb_Text.TabIndex = 24;
            this.IMDb_Text.UseSelectable = true;
            this.IMDb_Text.WaterMark = "IMDb";
            this.IMDb_Text.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.IMDb_Text.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // nameText
            // 
            // 
            // 
            // 
            this.nameText.CustomButton.Image = null;
            this.nameText.CustomButton.Location = new System.Drawing.Point(732, 1);
            this.nameText.CustomButton.Name = "";
            this.nameText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.nameText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.nameText.CustomButton.TabIndex = 1;
            this.nameText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.nameText.CustomButton.UseSelectable = true;
            this.nameText.CustomButton.Visible = false;
            this.nameText.Lines = new string[0];
            this.nameText.Location = new System.Drawing.Point(23, 63);
            this.nameText.MaxLength = 32767;
            this.nameText.Name = "nameText";
            this.nameText.PasswordChar = '\0';
            this.nameText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nameText.SelectedText = "";
            this.nameText.SelectionLength = 0;
            this.nameText.SelectionStart = 0;
            this.nameText.ShortcutsEnabled = true;
            this.nameText.Size = new System.Drawing.Size(754, 23);
            this.nameText.TabIndex = 25;
            this.nameText.UseSelectable = true;
            this.nameText.WaterMark = "Movie Name";
            this.nameText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.nameText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.DisplayMember = "Value";
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.ItemHeight = 23;
            this.sourceComboBox.Location = new System.Drawing.Point(23, 92);
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.PromptText = "Source";
            this.sourceComboBox.Size = new System.Drawing.Size(374, 29);
            this.sourceComboBox.TabIndex = 26;
            this.sourceComboBox.UseSelectable = true;
            this.sourceComboBox.ValueMember = "Key";
            // 
            // qualityComboBox
            // 
            this.qualityComboBox.DisplayMember = "Value";
            this.qualityComboBox.FormattingEnabled = true;
            this.qualityComboBox.ItemHeight = 23;
            this.qualityComboBox.Location = new System.Drawing.Point(403, 92);
            this.qualityComboBox.Name = "qualityComboBox";
            this.qualityComboBox.PromptText = "Quality";
            this.qualityComboBox.Size = new System.Drawing.Size(374, 29);
            this.qualityComboBox.TabIndex = 27;
            this.qualityComboBox.UseSelectable = true;
            this.qualityComboBox.ValueMember = "Key";
            // 
            // torrentUpload
            // 
            this.torrentUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.torrentUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.torrentUpload.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.torrentUpload.ForeColor = System.Drawing.Color.White;
            this.torrentUpload.Location = new System.Drawing.Point(23, 284);
            this.torrentUpload.Name = "torrentUpload";
            this.torrentUpload.Size = new System.Drawing.Size(374, 30);
            this.torrentUpload.TabIndex = 28;
            this.torrentUpload.Text = "Torrent Upload";
            this.torrentUpload.UseVisualStyleBackColor = false;
            this.torrentUpload.Click += new System.EventHandler(this.torrentUpload_Click);
            // 
            // torrentDownload
            // 
            this.torrentDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.torrentDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.torrentDownload.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.torrentDownload.ForeColor = System.Drawing.Color.White;
            this.torrentDownload.Location = new System.Drawing.Point(403, 284);
            this.torrentDownload.Name = "torrentDownload";
            this.torrentDownload.Size = new System.Drawing.Size(374, 30);
            this.torrentDownload.TabIndex = 29;
            this.torrentDownload.Text = "Torrent Download";
            this.torrentDownload.UseVisualStyleBackColor = false;
            this.torrentDownload.Click += new System.EventHandler(this.torrentDownload_Click);
            // 
            // subtitleLink
            // 
            // 
            // 
            // 
            this.subtitleLink.CustomButton.Image = null;
            this.subtitleLink.CustomButton.Location = new System.Drawing.Point(352, 1);
            this.subtitleLink.CustomButton.Name = "";
            this.subtitleLink.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.subtitleLink.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.subtitleLink.CustomButton.TabIndex = 1;
            this.subtitleLink.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.subtitleLink.CustomButton.UseSelectable = true;
            this.subtitleLink.CustomButton.Visible = false;
            this.subtitleLink.Lines = new string[0];
            this.subtitleLink.Location = new System.Drawing.Point(403, 162);
            this.subtitleLink.MaxLength = 32767;
            this.subtitleLink.Name = "subtitleLink";
            this.subtitleLink.PasswordChar = '\0';
            this.subtitleLink.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.subtitleLink.SelectedText = "";
            this.subtitleLink.SelectionLength = 0;
            this.subtitleLink.SelectionStart = 0;
            this.subtitleLink.ShortcutsEnabled = true;
            this.subtitleLink.Size = new System.Drawing.Size(374, 23);
            this.subtitleLink.TabIndex = 30;
            this.subtitleLink.UseSelectable = true;
            this.subtitleLink.WaterMark = "Subtitle Link";
            this.subtitleLink.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.subtitleLink.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // encoderComboBox
            // 
            this.encoderComboBox.DisplayMember = "Value";
            this.encoderComboBox.FormattingEnabled = true;
            this.encoderComboBox.ItemHeight = 23;
            this.encoderComboBox.Location = new System.Drawing.Point(23, 127);
            this.encoderComboBox.Name = "encoderComboBox";
            this.encoderComboBox.PromptText = "Encoder";
            this.encoderComboBox.Size = new System.Drawing.Size(374, 29);
            this.encoderComboBox.TabIndex = 31;
            this.encoderComboBox.UseSelectable = true;
            this.encoderComboBox.ValueMember = "Key";
            // 
            // sizeText
            // 
            // 
            // 
            // 
            this.sizeText.CustomButton.Image = null;
            this.sizeText.CustomButton.Location = new System.Drawing.Point(352, 1);
            this.sizeText.CustomButton.Name = "";
            this.sizeText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.sizeText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.sizeText.CustomButton.TabIndex = 1;
            this.sizeText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.sizeText.CustomButton.UseSelectable = true;
            this.sizeText.CustomButton.Visible = false;
            this.sizeText.Lines = new string[0];
            this.sizeText.Location = new System.Drawing.Point(23, 191);
            this.sizeText.MaxLength = 32767;
            this.sizeText.Name = "sizeText";
            this.sizeText.PasswordChar = '\0';
            this.sizeText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.sizeText.SelectedText = "";
            this.sizeText.SelectionLength = 0;
            this.sizeText.SelectionStart = 0;
            this.sizeText.ShortcutsEnabled = true;
            this.sizeText.Size = new System.Drawing.Size(374, 23);
            this.sizeText.TabIndex = 32;
            this.sizeText.UseSelectable = true;
            this.sizeText.WaterMark = "Size";
            this.sizeText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.sizeText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // typeText
            // 
            // 
            // 
            // 
            this.typeText.CustomButton.Image = null;
            this.typeText.CustomButton.Location = new System.Drawing.Point(352, 1);
            this.typeText.CustomButton.Name = "";
            this.typeText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.typeText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.typeText.CustomButton.TabIndex = 1;
            this.typeText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.typeText.CustomButton.UseSelectable = true;
            this.typeText.CustomButton.Visible = false;
            this.typeText.Lines = new string[0];
            this.typeText.Location = new System.Drawing.Point(403, 191);
            this.typeText.MaxLength = 32767;
            this.typeText.Name = "typeText";
            this.typeText.PasswordChar = '\0';
            this.typeText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.typeText.SelectedText = "";
            this.typeText.SelectionLength = 0;
            this.typeText.SelectionStart = 0;
            this.typeText.ShortcutsEnabled = true;
            this.typeText.Size = new System.Drawing.Size(374, 23);
            this.typeText.TabIndex = 33;
            this.typeText.UseSelectable = true;
            this.typeText.WaterMark = "Type";
            this.typeText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.typeText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // audioCodecComboBox
            // 
            this.audioCodecComboBox.DisplayMember = "Value";
            this.audioCodecComboBox.FormattingEnabled = true;
            this.audioCodecComboBox.ItemHeight = 23;
            this.audioCodecComboBox.Location = new System.Drawing.Point(23, 220);
            this.audioCodecComboBox.Name = "audioCodecComboBox";
            this.audioCodecComboBox.PromptText = "Audio Codec";
            this.audioCodecComboBox.Size = new System.Drawing.Size(374, 29);
            this.audioCodecComboBox.TabIndex = 34;
            this.audioCodecComboBox.UseSelectable = true;
            this.audioCodecComboBox.ValueMember = "Key";
            // 
            // audioChannelComboBox
            // 
            this.audioChannelComboBox.DisplayMember = "Value";
            this.audioChannelComboBox.FormattingEnabled = true;
            this.audioChannelComboBox.ItemHeight = 23;
            this.audioChannelComboBox.Location = new System.Drawing.Point(403, 220);
            this.audioChannelComboBox.Name = "audioChannelComboBox";
            this.audioChannelComboBox.PromptText = "Audio Channel";
            this.audioChannelComboBox.Size = new System.Drawing.Size(374, 29);
            this.audioChannelComboBox.TabIndex = 35;
            this.audioChannelComboBox.UseSelectable = true;
            this.audioChannelComboBox.ValueMember = "Key";
            // 
            // videoCodecComboBox
            // 
            this.videoCodecComboBox.DisplayMember = "Value";
            this.videoCodecComboBox.FormattingEnabled = true;
            this.videoCodecComboBox.ItemHeight = 23;
            this.videoCodecComboBox.Location = new System.Drawing.Point(403, 127);
            this.videoCodecComboBox.Name = "videoCodecComboBox";
            this.videoCodecComboBox.PromptText = "Video Codec";
            this.videoCodecComboBox.Size = new System.Drawing.Size(374, 29);
            this.videoCodecComboBox.TabIndex = 36;
            this.videoCodecComboBox.UseSelectable = true;
            this.videoCodecComboBox.ValueMember = "Key";
            // 
            // videoBitrateText
            // 
            // 
            // 
            // 
            this.videoBitrateText.CustomButton.Image = null;
            this.videoBitrateText.CustomButton.Location = new System.Drawing.Point(732, 1);
            this.videoBitrateText.CustomButton.Name = "";
            this.videoBitrateText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.videoBitrateText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.videoBitrateText.CustomButton.TabIndex = 1;
            this.videoBitrateText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.videoBitrateText.CustomButton.UseSelectable = true;
            this.videoBitrateText.CustomButton.Visible = false;
            this.videoBitrateText.Lines = new string[0];
            this.videoBitrateText.Location = new System.Drawing.Point(23, 255);
            this.videoBitrateText.MaxLength = 32767;
            this.videoBitrateText.Name = "videoBitrateText";
            this.videoBitrateText.PasswordChar = '\0';
            this.videoBitrateText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.videoBitrateText.SelectedText = "";
            this.videoBitrateText.SelectionLength = 0;
            this.videoBitrateText.SelectionStart = 0;
            this.videoBitrateText.ShortcutsEnabled = true;
            this.videoBitrateText.Size = new System.Drawing.Size(754, 23);
            this.videoBitrateText.TabIndex = 37;
            this.videoBitrateText.UseSelectable = true;
            this.videoBitrateText.WaterMark = "Video Bitrate";
            this.videoBitrateText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.videoBitrateText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loadBackground
            // 
            this.loadBackground.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getDictionary_DoWork);
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
            this.loadingImage.Size = new System.Drawing.Size(754, 456);
            this.loadingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingImage.TabIndex = 38;
            this.loadingImage.TabStop = false;
            this.loadingImage.Visible = false;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(23, 320);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(374, 50);
            this.saveButton.TabIndex = 39;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // fromFile
            // 
            this.fromFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.fromFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fromFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fromFile.ForeColor = System.Drawing.Color.White;
            this.fromFile.Location = new System.Drawing.Point(403, 320);
            this.fromFile.Name = "fromFile";
            this.fromFile.Size = new System.Drawing.Size(374, 50);
            this.fromFile.TabIndex = 40;
            this.fromFile.Text = "From File";
            this.fromFile.UseVisualStyleBackColor = false;
            this.fromFile.Click += new System.EventHandler(this.fromFile_Click);
            // 
            // movie_record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 542);
            this.Controls.Add(this.loadingImage);
            this.Controls.Add(this.fromFile);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.videoBitrateText);
            this.Controls.Add(this.videoCodecComboBox);
            this.Controls.Add(this.audioChannelComboBox);
            this.Controls.Add(this.audioCodecComboBox);
            this.Controls.Add(this.typeText);
            this.Controls.Add(this.sizeText);
            this.Controls.Add(this.encoderComboBox);
            this.Controls.Add(this.subtitleLink);
            this.Controls.Add(this.torrentDownload);
            this.Controls.Add(this.torrentUpload);
            this.Controls.Add(this.qualityComboBox);
            this.Controls.Add(this.sourceComboBox);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.IMDb_Text);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = global::LazyPortal.Properties.Resources.icon;
            this.MaximizeBox = false;
            this.Name = "movie_record";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.movie_record_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal MetroFramework.Controls.MetroTextBox IMDb_Text;
        internal MetroFramework.Controls.MetroTextBox nameText;
        private MetroFramework.Controls.MetroComboBox sourceComboBox;
        private MetroFramework.Controls.MetroComboBox qualityComboBox;
        private System.Windows.Forms.Button torrentUpload;
        private System.Windows.Forms.Button torrentDownload;
        internal MetroFramework.Controls.MetroTextBox subtitleLink;
        private MetroFramework.Controls.MetroComboBox encoderComboBox;
        internal MetroFramework.Controls.MetroTextBox sizeText;
        internal MetroFramework.Controls.MetroTextBox typeText;
        private MetroFramework.Controls.MetroComboBox audioCodecComboBox;
        private MetroFramework.Controls.MetroComboBox audioChannelComboBox;
        private MetroFramework.Controls.MetroComboBox videoCodecComboBox;
        internal MetroFramework.Controls.MetroTextBox videoBitrateText;
        private System.ComponentModel.BackgroundWorker loadBackground;
        private System.Windows.Forms.PictureBox loadingImage;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button fromFile;
    }
}