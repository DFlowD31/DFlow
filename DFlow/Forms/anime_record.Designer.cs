namespace LazyPortal
{
    partial class anime_record
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
            this.Anime_List = new System.Windows.Forms.ListBox();
            this.Add_Season = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Season_List = new System.Windows.Forms.ListBox();
            this.JPN_Name = new MetroFramework.Controls.MetroTextBox();
            this.ENG_Name = new MetroFramework.Controls.MetroTextBox();
            this.loadBackground = new System.ComponentModel.BackgroundWorker();
            this.loadingImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // Anime_List
            // 
            this.Anime_List.DisplayMember = "Value";
            this.Anime_List.FormattingEnabled = true;
            this.Anime_List.Location = new System.Drawing.Point(397, 63);
            this.Anime_List.Name = "Anime_List";
            this.Anime_List.Size = new System.Drawing.Size(331, 433);
            this.Anime_List.TabIndex = 26;
            this.Anime_List.ValueMember = "Key";
            this.Anime_List.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Anime_List_MouseDown);
            // 
            // Add_Season
            // 
            this.Add_Season.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Add_Season.FlatAppearance.BorderSize = 0;
            this.Add_Season.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add_Season.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Add_Season.ForeColor = System.Drawing.Color.White;
            this.Add_Season.Location = new System.Drawing.Point(360, 92);
            this.Add_Season.Name = "Add_Season";
            this.Add_Season.Size = new System.Drawing.Size(31, 342);
            this.Add_Season.TabIndex = 25;
            this.Add_Season.Tag = "blue";
            this.Add_Season.Text = "+";
            this.Add_Season.UseVisualStyleBackColor = false;
            this.Add_Season.Click += new System.EventHandler(this.Add_Season_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Save_Button.FlatAppearance.BorderSize = 0;
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Save_Button.ForeColor = System.Drawing.Color.White;
            this.Save_Button.Location = new System.Drawing.Point(23, 445);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(368, 51);
            this.Save_Button.TabIndex = 24;
            this.Save_Button.Tag = "blue";
            this.Save_Button.Text = "Create";
            this.Save_Button.UseVisualStyleBackColor = false;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Season_List
            // 
            this.Season_List.DisplayMember = "Value";
            this.Season_List.FormattingEnabled = true;
            this.Season_List.Location = new System.Drawing.Point(23, 92);
            this.Season_List.Name = "Season_List";
            this.Season_List.Size = new System.Drawing.Size(331, 342);
            this.Season_List.TabIndex = 22;
            this.Season_List.ValueMember = "Key";
            this.Season_List.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Season_List_MouseDoubleClick);
            this.Season_List.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Season_List_MouseDown);
            // 
            // JPN_Name
            // 
            // 
            // 
            // 
            this.JPN_Name.CustomButton.Image = null;
            this.JPN_Name.CustomButton.Location = new System.Drawing.Point(159, 1);
            this.JPN_Name.CustomButton.Name = "";
            this.JPN_Name.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.JPN_Name.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.JPN_Name.CustomButton.TabIndex = 1;
            this.JPN_Name.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.JPN_Name.CustomButton.UseSelectable = true;
            this.JPN_Name.CustomButton.Visible = false;
            this.JPN_Name.Lines = new string[0];
            this.JPN_Name.Location = new System.Drawing.Point(210, 63);
            this.JPN_Name.MaxLength = 32767;
            this.JPN_Name.Name = "JPN_Name";
            this.JPN_Name.PasswordChar = '\0';
            this.JPN_Name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.JPN_Name.SelectedText = "";
            this.JPN_Name.SelectionLength = 0;
            this.JPN_Name.SelectionStart = 0;
            this.JPN_Name.ShortcutsEnabled = true;
            this.JPN_Name.Size = new System.Drawing.Size(181, 23);
            this.JPN_Name.TabIndex = 21;
            this.JPN_Name.UseSelectable = true;
            this.JPN_Name.WaterMark = "Japanese Name";
            this.JPN_Name.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.JPN_Name.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // ENG_Name
            // 
            // 
            // 
            // 
            this.ENG_Name.CustomButton.Image = null;
            this.ENG_Name.CustomButton.Location = new System.Drawing.Point(159, 1);
            this.ENG_Name.CustomButton.Name = "";
            this.ENG_Name.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.ENG_Name.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ENG_Name.CustomButton.TabIndex = 1;
            this.ENG_Name.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ENG_Name.CustomButton.UseSelectable = true;
            this.ENG_Name.CustomButton.Visible = false;
            this.ENG_Name.Lines = new string[0];
            this.ENG_Name.Location = new System.Drawing.Point(23, 63);
            this.ENG_Name.MaxLength = 32767;
            this.ENG_Name.Name = "ENG_Name";
            this.ENG_Name.PasswordChar = '\0';
            this.ENG_Name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ENG_Name.SelectedText = "";
            this.ENG_Name.SelectionLength = 0;
            this.ENG_Name.SelectionStart = 0;
            this.ENG_Name.ShortcutsEnabled = true;
            this.ENG_Name.Size = new System.Drawing.Size(181, 23);
            this.ENG_Name.TabIndex = 20;
            this.ENG_Name.UseSelectable = true;
            this.ENG_Name.WaterMark = "English Name";
            this.ENG_Name.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ENG_Name.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loadBackground
            // 
            this.loadBackground.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadBackground_DoWork);
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
            this.loadingImage.Size = new System.Drawing.Size(887, 433);
            this.loadingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingImage.TabIndex = 27;
            this.loadingImage.TabStop = false;
            // 
            // anime_record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.Anime_List);
            this.Controls.Add(this.Add_Season);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.Season_List);
            this.Controls.Add(this.JPN_Name);
            this.Controls.Add(this.ENG_Name);
            this.Controls.Add(this.loadingImage);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = global::LazyPortal.Properties.Resources.icon;
            this.MaximizeBox = false;
            this.Name = "anime_record";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Anime Records";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Anime_record_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox Anime_List;
        private System.Windows.Forms.Button Add_Season;
        private System.Windows.Forms.Button Save_Button;
        internal System.Windows.Forms.ListBox Season_List;
        internal MetroFramework.Controls.MetroTextBox JPN_Name;
        internal MetroFramework.Controls.MetroTextBox ENG_Name;
        private System.ComponentModel.BackgroundWorker loadBackground;
        private System.Windows.Forms.PictureBox loadingImage;
    }
}