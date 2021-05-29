using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;
using LevelUpLearning.WinForms.Spelling;
using System.Drawing;

namespace LevelUpLearning.WinForm
{
    public partial class frmMain : Form
    {
        Version currentVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
        Version latestVersion;

        public frmMain()
        {
            InitializeComponent();

            if (IsUpdateAvailable())
            {
                //TODO: Prompt the user - but only once I have a way to let them ignore that prompt
                //PromptForUpdate();
            }

            InitLogin();
        }

        private void InitLogin()
        {
            lblUsername.Text = DataController.State.CurrentUser.DisplayName;
        }

        private bool IsUpdateAvailable()
        {
            var gitHub = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("LevelUpLearning"));
            var latest = gitHub.Repository.Release.GetLatest("NoahWright87", "LevelUpLearning").Result;
            latestVersion = new Version(latest.TagName.Trim('v'));

            if (latestVersion.CompareTo(currentVersion) > 0)
            {
                lblVersion.Text = $"{currentVersion} ({latestVersion} available)";
                lblVersion.ForeColor = Color.DarkRed;

                return true;
            }
            else
            {
                lblVersion.Text = currentVersion.ToString();
                lblVersion.ForeColor = Color.DarkGreen;

                return false;
            }
        }
        private void PromptForUpdate()
        {
            if (MessageBox.Show("A newer version is available on GitHub!  Go there now?", "Update Available", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(@"https://github.com/NoahWright87/LevelUpLearning/releases/latest");
            }
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
            new frmSpellingQuizPerformance().ShowDialog(this);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsUpdateAvailable())
            {
                PromptForUpdate();
            }
        }
    }
}
