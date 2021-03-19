using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KidQuiz
{
    public partial class frmDifficulty : Form
    {
        DifficultySettings[] DifficultySettingsOptions;

        String wordsFile = "words.txt";

        public frmDifficulty()
        {
            InitializeComponent();

            DifficultySettingsOptions = new DifficultySettings[]
            {
                new DifficultySettings()
                {
                    Name = "Monday (Easy)",
                    Difficulty = 0,
                    DifficultyPerStreak = 1,
                    StreakStyle = frmQuiz.StreakStyle.MaintainStreak,
                    TargetStreak = 2
                },
                new DifficultySettings()
                {
                    Name = "Tuesday (Easy-ish)",
                    Difficulty = 2,
                    DifficultyPerStreak = 1,
                    StreakStyle = frmQuiz.StreakStyle.MaintainStreak,
                    TargetStreak = 2
                },
                new DifficultySettings()
                {
                    Name = "Wednesday (hard-ish)",
                    Difficulty = 3,
                    DifficultyPerStreak = 2,
                    StreakStyle = frmQuiz.StreakStyle.IncrementalLoss,
                    TargetStreak = 2
                },
                new DifficultySettings()
                {
                    Name = "Thursday (hard)",
                    Difficulty = 100,
                    DifficultyPerStreak = 100,
                    StreakStyle = frmQuiz.StreakStyle.TotalLoss,
                    TargetStreak = 2
                }
            };

            cboOptions.Items.Clear();
            foreach (DifficultySettings setting in DifficultySettingsOptions)
            {
                cboOptions.Items.Add(setting.Name);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (cboOptions.SelectedIndex < 0)
            {
                MessageBox.Show("Select a difficulty first!");
            }
            else
            {
                this.Hide();

                String wordsFromFile = "";
                if (System.IO.File.Exists("words.txt"))
                {
                    wordsFromFile = System.IO.File.ReadAllText("words.txt");
                }

                new frmQuiz(wordsFromFile, DifficultySettingsOptions[cboOptions.SelectedIndex]).ShowDialog(this);
                this.Show();
            }
        }

        private void btnEditWords_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", wordsFile);
        }
    }
}
