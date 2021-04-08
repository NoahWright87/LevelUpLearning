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

            LoginChanged(); //React to the initial user login (if any)
        }

        private void OpenLoginForm()
        {
            //TODO: Open login form, hide this one
            Hide();
            //new frmLogin().ShowDialog(this);
            //TODO: What if nobody logs in?  Close this form?
            Show();
        }

        private void LoginChanged()
        {
            //Look in save data, see if there's a remembered login
            bool userNotLoggedIn = true; //TODO: Actually look in data
            if (userNotLoggedIn)
            {
                OpenLoginForm();
            }
            
            //TODO: Handle the case of no login happening, need to close form without action
            //TODO: Set the user's name somewhere on the form
        }

        private void btnSpellingTest_Click(object sender, EventArgs e)
        {
            //TODO: Launch the spelling test difficulty selector
        }

        private void btnSpellingTestSetup_Click(object sender, EventArgs e)
        {
            //TODO: Launch the form to create spelling lists
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //TODO: Log out, bring up the login form, close this form
        }

        //TODO: Handle closing the form
    }
}
