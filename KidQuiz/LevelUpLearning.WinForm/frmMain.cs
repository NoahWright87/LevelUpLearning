using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            InitLogin();
        }

        private void InitLogin()
        {
            lblUsername.Text = DataController.State.CurrentUser.DisplayName;
        }

        private void btnSpellingTest_Click(object sender, EventArgs e)
        {
            new frmSpellingQuiz().ShowDialog();
        }

        private void btnSpellingTestSetup_Click(object sender, EventArgs e)
        {
            ShowSubForm(new frmSpellingListsSetup());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private DialogResult ShowSubForm(Form otherForm)
        {
            Hide();
            var result = otherForm.ShowDialog(this);
            Show();
            return result;
        }

        private void btnSpellingStats_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon!");
        }
    }
}
