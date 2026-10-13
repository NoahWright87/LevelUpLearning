using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Common
{
    public partial class TextDialog : Form
    {
        private TextDialog(string prompt)
        {
            InitializeComponent();

            lblPrompt.Text = prompt;
        }

        public static string Show(IWin32Window owner, string prompt)
        {
            var form = new TextDialog(prompt);
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
