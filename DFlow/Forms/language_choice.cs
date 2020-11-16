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
    public partial class language_choice : MetroFramework.Forms.MetroForm
    {
        public language_choice()
        {
            InitializeComponent();
        }

        private void English_Click(object sender, EventArgs e)
        {
            Main.chosen_language = "English";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Japanese_Click(object sender, EventArgs e)
        {
            Main.chosen_language = "Japanese";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
