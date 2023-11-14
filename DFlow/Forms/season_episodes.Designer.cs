namespace LazyPortal
{
    partial class season_episodes
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.From_Excel_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.JPN_Season_Name = new MetroFramework.Controls.MetroTextBox();
            this.ENG_Season_Name = new MetroFramework.Controls.MetroTextBox();
            this.Open_File_Dialog = new System.Windows.Forms.OpenFileDialog();
            this.getEpisodes = new System.ComponentModel.BackgroundWorker();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            this.getFromExcel = new System.ComponentModel.BackgroundWorker();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.saveBackground = new System.ComponentModel.BackgroundWorker();
            this.Save_File_Dialog = new System.Windows.Forms.SaveFileDialog();
            this.startIndex = new System.Windows.Forms.TextBox();
            this.endIndex = new System.Windows.Forms.TextBox();
            this.uploadRange = new System.Windows.Forms.Button();
            this.downloadRange = new System.Windows.Forms.Button();
            this.batchDownload = new System.Windows.Forms.Button();
            this.batchUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // From_Excel_Button
            // 
            this.From_Excel_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.From_Excel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.From_Excel_Button.FlatAppearance.BorderSize = 0;
            this.From_Excel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.From_Excel_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.From_Excel_Button.ForeColor = System.Drawing.Color.White;
            this.From_Excel_Button.Location = new System.Drawing.Point(458, 445);
            this.From_Excel_Button.Name = "From_Excel_Button";
            this.From_Excel_Button.Size = new System.Drawing.Size(429, 51);
            this.From_Excel_Button.TabIndex = 26;
            this.From_Excel_Button.Text = "From Excel";
            this.From_Excel_Button.UseVisualStyleBackColor = false;
            this.From_Excel_Button.Click += new System.EventHandler(this.From_Excel_Button_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Save_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Save_Button.FlatAppearance.BorderSize = 0;
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Save_Button.ForeColor = System.Drawing.Color.White;
            this.Save_Button.Location = new System.Drawing.Point(23, 445);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(429, 51);
            this.Save_Button.TabIndex = 25;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = false;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // JPN_Season_Name
            // 
            this.JPN_Season_Name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // 
            // 
            this.JPN_Season_Name.CustomButton.Image = null;
            this.JPN_Season_Name.CustomButton.Location = new System.Drawing.Point(407, 1);
            this.JPN_Season_Name.CustomButton.Name = "";
            this.JPN_Season_Name.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.JPN_Season_Name.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.JPN_Season_Name.CustomButton.TabIndex = 1;
            this.JPN_Season_Name.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.JPN_Season_Name.CustomButton.UseSelectable = true;
            this.JPN_Season_Name.CustomButton.Visible = false;
            this.JPN_Season_Name.Lines = new string[0];
            this.JPN_Season_Name.Location = new System.Drawing.Point(458, 63);
            this.JPN_Season_Name.MaxLength = 32767;
            this.JPN_Season_Name.Name = "JPN_Season_Name";
            this.JPN_Season_Name.PasswordChar = '\0';
            this.JPN_Season_Name.PromptText = "Season Japanese Name";
            this.JPN_Season_Name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.JPN_Season_Name.SelectedText = "";
            this.JPN_Season_Name.SelectionLength = 0;
            this.JPN_Season_Name.SelectionStart = 0;
            this.JPN_Season_Name.ShortcutsEnabled = true;
            this.JPN_Season_Name.Size = new System.Drawing.Size(429, 23);
            this.JPN_Season_Name.TabIndex = 24;
            this.JPN_Season_Name.UseSelectable = true;
            this.JPN_Season_Name.WaterMark = "Season Japanese Name";
            this.JPN_Season_Name.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.JPN_Season_Name.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // ENG_Season_Name
            // 
            // 
            // 
            // 
            this.ENG_Season_Name.CustomButton.Image = null;
            this.ENG_Season_Name.CustomButton.Location = new System.Drawing.Point(407, 1);
            this.ENG_Season_Name.CustomButton.Name = "";
            this.ENG_Season_Name.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.ENG_Season_Name.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ENG_Season_Name.CustomButton.TabIndex = 1;
            this.ENG_Season_Name.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ENG_Season_Name.CustomButton.UseSelectable = true;
            this.ENG_Season_Name.CustomButton.Visible = false;
            this.ENG_Season_Name.Lines = new string[0];
            this.ENG_Season_Name.Location = new System.Drawing.Point(23, 63);
            this.ENG_Season_Name.MaxLength = 32767;
            this.ENG_Season_Name.Name = "ENG_Season_Name";
            this.ENG_Season_Name.PasswordChar = '\0';
            this.ENG_Season_Name.PromptText = "Season English Name";
            this.ENG_Season_Name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ENG_Season_Name.SelectedText = "";
            this.ENG_Season_Name.SelectionLength = 0;
            this.ENG_Season_Name.SelectionStart = 0;
            this.ENG_Season_Name.ShortcutsEnabled = true;
            this.ENG_Season_Name.Size = new System.Drawing.Size(429, 23);
            this.ENG_Season_Name.TabIndex = 23;
            this.ENG_Season_Name.UseSelectable = true;
            this.ENG_Season_Name.WaterMark = "Season English Name";
            this.ENG_Season_Name.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ENG_Season_Name.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Open_File_Dialog
            // 
            this.Open_File_Dialog.FileName = "OpenFileDialog1";
            // 
            // getEpisodes
            // 
            this.getEpisodes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetEpisodes_DoWork);
            this.getEpisodes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetEpisodes_RunWorkerCompleted);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.White;
            this.dataGridView.Location = new System.Drawing.Point(23, 121);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(864, 318);
            this.dataGridView.TabIndex = 31;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellClick);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.DataGridView_CurrentCellDirtyStateChanged);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridView_DataBindingComplete);
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_DataError);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_KeyDown);
            this.dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseDown);
            // 
            // loadingImage
            // 
            this.loadingImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.loadingImage.Image = global::LazyPortal.Properties.Resources._64x64;
            this.loadingImage.Location = new System.Drawing.Point(23, 63);
            this.loadingImage.Name = "loadingImage";
            this.loadingImage.Size = new System.Drawing.Size(864, 433);
            this.loadingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingImage.TabIndex = 32;
            this.loadingImage.TabStop = false;
            // 
            // getFromExcel
            // 
            this.getFromExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetFromExcel_DoWork);
            this.getFromExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GetFromExcel_RunWorkerCompleted);
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 1;
            this.mainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // saveBackground
            // 
            this.saveBackground.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SaveBackground_DoWork);
            // 
            // startIndex
            // 
            this.startIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startIndex.Location = new System.Drawing.Point(461, 92);
            this.startIndex.Name = "startIndex";
            this.startIndex.Size = new System.Drawing.Size(102, 23);
            this.startIndex.TabIndex = 33;
            this.startIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.startIndex_KeyPress);
            // 
            // endIndex
            // 
            this.endIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endIndex.Location = new System.Drawing.Point(569, 92);
            this.endIndex.Name = "endIndex";
            this.endIndex.Size = new System.Drawing.Size(102, 23);
            this.endIndex.TabIndex = 34;
            this.endIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.endIndex_KeyPress);
            // 
            // uploadRange
            // 
            this.uploadRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.uploadRange.FlatAppearance.BorderSize = 0;
            this.uploadRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadRange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.uploadRange.ForeColor = System.Drawing.Color.White;
            this.uploadRange.Location = new System.Drawing.Point(677, 92);
            this.uploadRange.Name = "uploadRange";
            this.uploadRange.Size = new System.Drawing.Size(102, 23);
            this.uploadRange.TabIndex = 35;
            this.uploadRange.Text = "Upload";
            this.uploadRange.UseVisualStyleBackColor = false;
            this.uploadRange.Click += new System.EventHandler(this.UploadRange_Click);
            // 
            // downloadRange
            // 
            this.downloadRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.downloadRange.FlatAppearance.BorderSize = 0;
            this.downloadRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadRange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.downloadRange.ForeColor = System.Drawing.Color.White;
            this.downloadRange.Location = new System.Drawing.Point(785, 92);
            this.downloadRange.Name = "downloadRange";
            this.downloadRange.Size = new System.Drawing.Size(102, 23);
            this.downloadRange.TabIndex = 36;
            this.downloadRange.Text = "Download";
            this.downloadRange.UseVisualStyleBackColor = false;
            this.downloadRange.Click += new System.EventHandler(this.downloadRange_Click);
            // 
            // batchDownload
            // 
            this.batchDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.batchDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.batchDownload.FlatAppearance.BorderSize = 0;
            this.batchDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.batchDownload.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.batchDownload.ForeColor = System.Drawing.Color.White;
            this.batchDownload.Location = new System.Drawing.Point(131, 92);
            this.batchDownload.Name = "batchDownload";
            this.batchDownload.Size = new System.Drawing.Size(102, 23);
            this.batchDownload.TabIndex = 38;
            this.batchDownload.Text = "Batch Download";
            this.batchDownload.UseVisualStyleBackColor = false;
            this.batchDownload.Click += new System.EventHandler(this.batchDownload_Click);
            // 
            // batchUpload
            // 
            this.batchUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.batchUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.batchUpload.FlatAppearance.BorderSize = 0;
            this.batchUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.batchUpload.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.batchUpload.ForeColor = System.Drawing.Color.White;
            this.batchUpload.Location = new System.Drawing.Point(23, 92);
            this.batchUpload.Name = "batchUpload";
            this.batchUpload.Size = new System.Drawing.Size(102, 23);
            this.batchUpload.TabIndex = 37;
            this.batchUpload.Text = "Batch Upload";
            this.batchUpload.UseVisualStyleBackColor = false;
            this.batchUpload.Click += new System.EventHandler(this.batchUpload_Click);
            // 
            // season_episodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 519);
            this.Controls.Add(this.batchDownload);
            this.Controls.Add(this.batchUpload);
            this.Controls.Add(this.downloadRange);
            this.Controls.Add(this.uploadRange);
            this.Controls.Add(this.endIndex);
            this.Controls.Add(this.startIndex);
            this.Controls.Add(this.From_Excel_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.JPN_Season_Name);
            this.Controls.Add(this.ENG_Season_Name);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.loadingImage);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = global::LazyPortal.Properties.Resources.icon;
            this.Name = "season_episodes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Season_episodes_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Season_episodes_FormClosed);
            this.Load += new System.EventHandler(this.season_episodes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button From_Excel_Button;
        private System.Windows.Forms.Button Save_Button;
        internal MetroFramework.Controls.MetroTextBox JPN_Season_Name;
        internal MetroFramework.Controls.MetroTextBox ENG_Season_Name;
        internal System.Windows.Forms.OpenFileDialog Open_File_Dialog;
        private System.ComponentModel.BackgroundWorker getEpisodes;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.PictureBox loadingImage;
        private System.ComponentModel.BackgroundWorker getFromExcel;
        private System.Windows.Forms.Timer mainTimer;
        private System.ComponentModel.BackgroundWorker saveBackground;
        private System.Windows.Forms.SaveFileDialog Save_File_Dialog;
        private System.Windows.Forms.TextBox startIndex;
        private System.Windows.Forms.TextBox endIndex;
        private System.Windows.Forms.Button uploadRange;
        private System.Windows.Forms.Button downloadRange;
        private System.Windows.Forms.Button batchDownload;
        private System.Windows.Forms.Button batchUpload;
    }
}