using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    //TODO: This code looks incomplete - I need a way to create words from this page
    //  If I remember correctly, I didn't finish this and went with the lazy method of a tab-delimited file editted manually
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
            //TODO: Launch the spelling test difficulty selector
            new frmQuiz().ShowDialog();
        }

        private void btnSpellingTestSetup_Click(object sender, EventArgs e)
        {
            ShowSubForm(new frmSpellingListsSetup());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close(); //TODO: Save things?
        }

        //TODO: Handle closing the form

        private DialogResult ShowSubForm(Form otherForm)
        {
            Hide();
            var result = otherForm.ShowDialog(this);
            Show();
            return result;
        }
    }
}
