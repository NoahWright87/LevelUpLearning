using LevelUpLearning.Core.Data;
using LevelUpLearning.WinForms;
using LevelUpLearning.WinForms.Common;
using LevelUpLearning.WinForms.Math;
using LevelUpLearning.WinForms.Spelling;
using System;
using System.Drawing;
using System.Windows.Forms;

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
            UpdateLevels();
        }

        private void UpdateLevels()
        {
            btnSpellingTest.Text = $"Spelling{Environment.NewLine}Level: {DataController.State.CurrentUser.Character.LevelSpelling:0.0}";
            btnMathTest.Text = $"Math{Environment.NewLine}Level: {DataController.State.CurrentUser.Character.LevelMath:0.0}";
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
            UpdateLevels();
        }

        private void btnSpellingTestSetup_Click(object sender, EventArgs e)
        {
            int choice = OptionDialog.Show(this, "How would you like to set up the spelling lists?",
                "Quick Create", "Check for Downloadable Lists", "Advanced Setup");

            switch(choice)
            {
                case 0:
                    ShowSubForm(new frmSpellingListsQuickCreate());
                    break;
                case 1:
                    DownloadSpellingLists();
                    break;
                case 2:
                    ShowSubForm(new frmSpellingListsSetup());
                    break;
                default:
                    //Do nothing
                    break;
            }
        }
        private void DownloadSpellingLists()
        {
            //TODO: Eventually, there may be a ton of lists - maybe give the option to only import some?
            var response = GitHubUtility.GetSpellingLists();
            if (response.Files.Count <= 0)
            {
                MessageBox.Show("No files are available right now.  Check back later!");
            }
            else
            {
                GitHubUtility.DownloadFiles(response);
                int successes = 0;

                foreach (var file in response.Files)
                {
                    if (DataController.TryImportWordXml(file.Contents)) successes++;
                }

                string failMessage = (response.Files.Count > successes) ? "  Failures may be because a list with the same name already exists." : "";
                MessageBox.Show($"Imported {successes} of {response.Files.Count} file(s) from web source.{failMessage}", "Done",
                    MessageBoxButtons.OK, string.IsNullOrEmpty(failMessage) ? MessageBoxIcon.Exclamation : MessageBoxIcon.None);
            }
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

        private void btnMathTest_Click(object sender, EventArgs e)
        {
            ShowSubForm(new frmMathQuiz());
            UpdateLevels();
        }
    }
}
