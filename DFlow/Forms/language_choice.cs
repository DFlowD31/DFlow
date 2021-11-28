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
    public partial class language_choice : MetroFramework.Forms.MetroForm
    {
        public language_choice()
        {
            InitializeComponent();
        }

        private void English_Click(object sender, EventArgs e)
        {
            choice.chosenLanguage = services.language.english;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Japanese_Click(object sender, EventArgs e)
        {
            choice.chosenLanguage = services.language.japanese;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
