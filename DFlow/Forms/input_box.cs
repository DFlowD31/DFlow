using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DFlow
{
    public partial class input_box : MetroFramework.Forms.MetroForm
    {
        public input_box()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            Main.inputed_text = Input_Text.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Input_Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Submit.PerformClick();
        }
    }
}
