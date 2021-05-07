using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            //TODO: Check for "remember me" user, skip this form if present

            //TODO: Init dropdown list with previous users
        }

        private void UserDropdownChanged(object sender, EventArgs e)
        {
            //Disable the login button unless a username is actually typed
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(cboUser.Text);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //TODO: Save user into list of users if they weren't there previously?

            //TODO: Set the current user to what was entered

            //Hide this form, open main menu form
            Hide();
            new frmMain().ShowDialog(this);
            //Show back up when main menu is closed
            Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //TODO: Close this form, exit the program
            //TODO: Any final saving needs to be done here
        }
    }
}
