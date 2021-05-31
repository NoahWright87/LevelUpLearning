using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Common
{
    public partial class OptionDialog : Form
    {
        private OptionDialog(string prompt, params string[] choices)
        {
            InitializeComponent();

            lblPrompt.Text = prompt;
            cboChoices.Items.Clear();
            foreach (var choice in choices)
            {
                cboChoices.Items.Add(choice);
            }
        }
        public static int Show(IWin32Window owner, string prompt, params string[] choices)
        {
            var form = new OptionDialog(prompt, choices);
            var result = form.ShowDialog(owner);
            if (result == DialogResult.OK)
            {
                return form.cboChoices.SelectedIndex;
            }
            else
            {
                return -1;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (cboChoices.SelectedIndex >= 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("You must make a choice first!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cboChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDone.Enabled = cboChoices.SelectedIndex >= 0; 
        }
    }
}
