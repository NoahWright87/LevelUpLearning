using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            InitUsers();

            //TODO: Check for "remember me" user, skip this form if present
        }
        private void InitUsers()
        {
            cboUser.Items.Clear();
            foreach (var user in DataController.Root.Users.Values)
            {
                cboUser.Items.Add(user);
            }
            cboUser.SelectedIndex = -1;
        }

        private void UserDropdownChanged(object sender, EventArgs e)
        {
            //Disable the login button unless a username is actually typed
            btnLogin.Enabled = cboUser.SelectedIndex >= 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cboUser.SelectedIndex >= 0)
            {
                DataController.State.CurrentUser = (UserData)cboUser.SelectedItem;

                //Hide this form, open main menu form
                Hide();
                new frmMain().ShowDialog(this);
                //Show back up when main menu is closed
                Show();
            }
            else
            {
                MessageBox.Show("Pick a user!  If none are available, add one!");
                //TODO: Adjust this message once I force you to add a user
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            Hide();
            if (new frmAddUser().ShowDialog(this) == DialogResult.OK)
            {
                InitUsers();
            }
            Show();
        }
    }
}
