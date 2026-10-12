using LevelUpLearning.Core.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            InitUsers();
            if (DataController.Root.LastUser != null)
            {
                Login(DataController.Root.LastUser, true);
            }
        }
        private void InitUsers()
        {
            if (DataController.Root.Users.Any())
            {
                cboUser.Items.Clear();
                foreach (var user in DataController.Root.Users.Values)
                {
                    cboUser.Items.Add(user);
                }
                cboUser.SelectedIndex = -1;
            }
            else
            {
                btnNewUser_Click(null, null);
            }
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
                Login((UserData)cboUser.SelectedItem, chkRememberMe.Checked);
            }
            else
            {
                MessageBox.Show("You have to pick a user first!");
            }
        }

        private void Login(UserData user, bool rememberNextTime)
        {
            DataController.State.CurrentUser = user;
            if (rememberNextTime)
            {
                DataController.Root.LastUser = user;
            }
            else
            {
                DataController.Root.LastUser = null;
            }

            //Hide this form, open main menu form
            Hide();
            new frmMain().ShowDialog(this);
            //Show back up when main menu is closed
            Show();
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
