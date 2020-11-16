namespace DFlow
{
    partial class input_box
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
            this.Input_Text = new MetroFramework.Controls.MetroTextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Input_Text
            // 
            // 
            // 
            // 
            this.Input_Text.CustomButton.Image = null;
            this.Input_Text.CustomButton.Location = new System.Drawing.Point(508, 1);
            this.Input_Text.CustomButton.Name = "";
            this.Input_Text.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.Input_Text.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Input_Text.CustomButton.TabIndex = 1;
            this.Input_Text.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Input_Text.CustomButton.UseSelectable = true;
            this.Input_Text.CustomButton.Visible = false;
            this.Input_Text.Lines = new string[0];
            this.Input_Text.Location = new System.Drawing.Point(23, 63);
            this.Input_Text.MaxLength = 32767;
            this.Input_Text.Name = "Input_Text";
            this.Input_Text.PasswordChar = '\0';
            this.Input_Text.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Input_Text.SelectedText = "";
            this.Input_Text.SelectionLength = 0;
            this.Input_Text.SelectionStart = 0;
            this.Input_Text.ShortcutsEnabled = true;
            this.Input_Text.Size = new System.Drawing.Size(530, 23);
            this.Input_Text.Style = MetroFramework.MetroColorStyle.Blue;
            this.Input_Text.TabIndex = 4;
            this.Input_Text.UseSelectable = true;
            this.Input_Text.UseStyleColors = true;
            this.Input_Text.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Input_Text.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.Input_Text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_Text_KeyDown);
            // 
            // Submit
            // 
            this.Submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.Submit.FlatAppearance.BorderSize = 0;
            this.Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Submit.ForeColor = System.Drawing.Color.White;
            this.Submit.Location = new System.Drawing.Point(23, 92);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(530, 50);
            this.Submit.TabIndex = 3;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = false;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // input_box
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.Input_Text);
            this.Controls.Add(this.Submit);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = global::DFlow.Properties.Resources.icon;
            this.MaximizeBox = false;
            this.Name = "input_box";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Input";
            this.ResumeLayout(false);

        }

        #endregion

        internal MetroFramework.Controls.MetroTextBox Input_Text;
        internal System.Windows.Forms.Button Submit;
    }
}