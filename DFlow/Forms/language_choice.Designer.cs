namespace DFlow
{
    partial class language_choice
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
            this.Japanese = new System.Windows.Forms.Button();
            this.English = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Japanese
            // 
            this.Japanese.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Japanese.FlatAppearance.BorderSize = 0;
            this.Japanese.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Japanese.ForeColor = System.Drawing.Color.White;
            this.Japanese.Location = new System.Drawing.Point(147, 63);
            this.Japanese.Name = "Japanese";
            this.Japanese.Size = new System.Drawing.Size(118, 50);
            this.Japanese.TabIndex = 4;
            this.Japanese.Text = "Japanese";
            this.Japanese.UseVisualStyleBackColor = false;
            this.Japanese.Click += new System.EventHandler(this.Japanese_Click);
            // 
            // English
            // 
            this.English.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.English.FlatAppearance.BorderSize = 0;
            this.English.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.English.ForeColor = System.Drawing.Color.White;
            this.English.Location = new System.Drawing.Point(23, 63);
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(118, 50);
            this.English.TabIndex = 3;
            this.English.Text = "English";
            this.English.UseVisualStyleBackColor = false;
            this.English.Click += new System.EventHandler(this.English_Click);
            // 
            // language_choice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(288, 519);
            this.Controls.Add(this.Japanese);
            this.Controls.Add(this.English);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = global::DFlow.Properties.Resources.icon;
            this.MaximizeBox = false;
            this.Name = "language_choice";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Choose Your Language";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Japanese;
        internal System.Windows.Forms.Button English;
    }
}