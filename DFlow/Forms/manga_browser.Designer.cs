namespace LazyPortal
{
    partial class manga_browser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(manga_browser));
            this.main_background_worker = new System.ComponentModel.BackgroundWorker();
            this.main_grid = new System.Windows.Forms.FlowLayoutPanel();
            this.load_image = new System.Windows.Forms.PictureBox();
            this.chapter_listview = new MetroFramework.Controls.MetroListView();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.status_text = new MetroFramework.Controls.MetroLabel();
            this.ProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.defaultLocation = new MetroFramework.Controls.MetroTextBox();
            this.load_background_worker = new System.ComponentModel.BackgroundWorker();
            this.main_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.load_image)).BeginInit();
            this.SuspendLayout();
            // 
            // main_background_worker
            // 
            this.main_background_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.main_background_worker_DoWork);
            this.main_background_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.main_background_worker_RunWorkerCompleted);
            // 
            // main_grid
            // 
            this.main_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.main_grid.BackColor = System.Drawing.Color.White;
            this.main_grid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.main_grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.main_grid.Controls.Add(this.load_image);
            this.main_grid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.main_grid.Location = new System.Drawing.Point(20, 111);
            this.main_grid.MinimumSize = new System.Drawing.Size(760, 309);
            this.main_grid.Name = "main_grid";
            this.main_grid.Size = new System.Drawing.Size(760, 344);
            this.main_grid.TabIndex = 0;
            // 
            // load_image
            // 
            this.load_image.Image = global::LazyPortal.Properties.Resources._150x150;
            this.load_image.Location = new System.Drawing.Point(3, 3);
            this.load_image.Name = "load_image";
            this.load_image.Size = new System.Drawing.Size(753, 334);
            this.load_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.load_image.TabIndex = 0;
            this.load_image.TabStop = false;
            this.load_image.Visible = false;
            // 
            // chapter_listview
            // 
            this.chapter_listview.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.chapter_listview.AutoArrange = false;
            this.chapter_listview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chapter_listview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chapter_listview.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.chapter_listview.FullRowSelect = true;
            this.chapter_listview.GridLines = true;
            this.chapter_listview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.chapter_listview.Location = new System.Drawing.Point(20, 461);
            this.chapter_listview.MinimumSize = new System.Drawing.Size(2, 75);
            this.chapter_listview.MultiSelect = false;
            this.chapter_listview.Name = "chapter_listview";
            this.chapter_listview.OwnerDraw = true;
            this.chapter_listview.Size = new System.Drawing.Size(760, 75);
            this.chapter_listview.TabIndex = 1;
            this.chapter_listview.TileSize = new System.Drawing.Size(754, 46);
            this.chapter_listview.UseCompatibleStateImageBehavior = false;
            this.chapter_listview.UseSelectable = true;
            this.chapter_listview.View = System.Windows.Forms.View.Details;
            this.chapter_listview.SizeChanged += new System.EventHandler(this.chapter_listview_SizeChanged);
            this.chapter_listview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chapter_listview_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Status :- ";
            // 
            // status_text
            // 
            this.status_text.AutoSize = true;
            this.status_text.Location = new System.Drawing.Point(86, 60);
            this.status_text.Name = "status_text";
            this.status_text.Size = new System.Drawing.Size(0, 0);
            this.status_text.TabIndex = 3;
            this.status_text.UseCustomForeColor = true;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(20, 82);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(760, 23);
            this.ProgressBar.TabIndex = 4;
            // 
            // defaultLocation
            // 
            this.defaultLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.defaultLocation.CustomButton.Image = null;
            this.defaultLocation.CustomButton.Location = new System.Drawing.Point(98, 1);
            this.defaultLocation.CustomButton.Name = "";
            this.defaultLocation.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.defaultLocation.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.defaultLocation.CustomButton.TabIndex = 1;
            this.defaultLocation.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.defaultLocation.CustomButton.UseSelectable = true;
            this.defaultLocation.CustomButton.Visible = false;
            this.defaultLocation.Lines = new string[] {
        "D:\\Manga"};
            this.defaultLocation.Location = new System.Drawing.Point(660, 53);
            this.defaultLocation.MaxLength = 32767;
            this.defaultLocation.Name = "defaultLocation";
            this.defaultLocation.PasswordChar = '\0';
            this.defaultLocation.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.defaultLocation.SelectedText = "";
            this.defaultLocation.SelectionLength = 0;
            this.defaultLocation.SelectionStart = 0;
            this.defaultLocation.ShortcutsEnabled = true;
            this.defaultLocation.Size = new System.Drawing.Size(120, 23);
            this.defaultLocation.TabIndex = 5;
            this.defaultLocation.Text = "D:\\Manga";
            this.defaultLocation.UseSelectable = true;
            this.defaultLocation.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.defaultLocation.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.defaultLocation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.defaultLocation_KeyUp);
            // 
            // load_background_worker
            // 
            this.load_background_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.load_background_worker_DoWork);
            // 
            // manga_browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 556);
            this.Controls.Add(this.defaultLocation);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.status_text);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chapter_listview);
            this.Controls.Add(this.main_grid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 485);
            this.Name = "manga_browser";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "W";
            this.Text = "Manga";
            this.Load += new System.EventHandler(this.manga_browser_Load);
            this.main_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.load_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker main_background_worker;
        private System.Windows.Forms.FlowLayoutPanel main_grid;
        private MetroFramework.Controls.MetroListView chapter_listview;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel status_text;
        private MetroFramework.Controls.MetroProgressBar ProgressBar;
        private MetroFramework.Controls.MetroTextBox defaultLocation;
        private System.ComponentModel.BackgroundWorker load_background_worker;
        private System.Windows.Forms.PictureBox load_image;
    }
}