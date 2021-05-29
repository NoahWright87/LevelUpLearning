using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Common
{
    public partial class frmTextPrompt : Form
    {
        private frmTextPrompt(string prompt)
        {
            InitializeComponent();

            lblPrompt.Text = prompt;
        }

        public static string GetAnswer(IWin32Window owner, string prompt)
        {
            var form = new frmTextPrompt(prompt);
            if (form.ShowDialog(owner) == DialogResult.OK)
            {
                return form.txtAnswer.Text;
            }
            else
            {
                return null;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
