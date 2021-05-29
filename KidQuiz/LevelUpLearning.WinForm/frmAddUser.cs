using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("You must enter something into all fields.");
            }
            else
            {
                UserData newUser = new UserData()
                {
                    UserName = txtUsername.Text.Trim(),
                    DisplayName = txtDisplayName.Text.Trim()
                };

                DataController.Root.Users.Add(newUser.UserName, newUser);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
