using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LazyPortal.services;

namespace LazyPortal
{
    public partial class choice_box : MetroFramework.Forms.MetroForm
    {
        public choice_box()
        {
            InitializeComponent();
            choice3Panel.Visible = false;
            submitButton.Top -= 95;
            cancelButton.Top -= 95;
        }

        public choice_box(bool is_series, bool is_container, bool third, string thirdText = "")
        {
            seriesPanel.Visible = is_series;
            containerPanel.Visible = is_container;
            choice3Panel.Visible = third;
            if (third)
                choice3Label.Text = thirdText;

            InitializeComponent();
        }

        private void choice_box_Load(object sender, EventArgs e)
        {
            Program.Main_Form.enableMain(false);
            ActiveControl = submitButton;
        }

        private void seriesYes_Click(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(0, 174, 219);
            seriesNo.BackColor = Color.Silver;
            choice.series = true;
        }

        private void seriesNo_Click(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(0, 174, 219);
            seriesYes.BackColor = Color.Silver;
            choice.series = false;
        }

        private void containerYes_Click(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(0, 174, 219);
            containerNo.BackColor = Color.Silver;
            choice.container = true;
        }

        private void containerNo_Click(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(0, 174, 219);
            containerYes.BackColor = Color.Silver;
            choice.container = false;
        }

        private void choice3Yes_Click(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(0, 174, 219);
            choice3No.BackColor = Color.Silver;
            choice.third = true;
        }

        private void choice3No_Click(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(0, 174, 219);
            choice3Yes.BackColor = Color.Silver;
            choice.third = false;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void choice_box_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Main_Form.enableMain(true);
        }
    }
}
