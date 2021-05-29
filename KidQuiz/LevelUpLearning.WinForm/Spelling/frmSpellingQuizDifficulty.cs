using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;
using System.Linq;

namespace LevelUpLearning.WinForm
{
    public partial class frmSpellingQuizDifficulty : Form
    {
        private readonly string DifficultyPrefix;
        private readonly string HintLettersPrefix;
        private readonly string HintChangePrefix;
        private readonly string TargetStreakPrefix;
        private readonly string StreakPenaltyPrefix;
        private readonly string ScoreMultiplierPrefix;

        public frmSpellingQuizDifficulty()
        {
            InitializeComponent();

            //Save the current value of the labels so we can append to them later
            DifficultyPrefix = lblDifficulty.Text;
            HintLettersPrefix = lblHintLetters.Text;
            HintChangePrefix = lblHintChange.Text;
            TargetStreakPrefix = lblTargetStreak.Text;
            StreakPenaltyPrefix = lblStreakPenalty.Text;
            ScoreMultiplierPrefix = lblScoreMultiplier.Text;

            InitializeWordLists();

            //barDifficulty.Value = 0; //TODO: Default to last setting
            barDifficulty_Scroll(null, null);
        }

        public static bool ShowDialog(out SpellingQuizSettings settings)
        {

            var form = new frmSpellingQuizDifficulty();
            if (form.ShowDialog() == DialogResult.OK)
            {
                settings = new SpellingQuizSettings()
                {
                    NumHintLetters = form.barHintLetters.Value,
                    NumHintLettersChange = form.barHintChange.Value,
                    StreakPenalty = form.barStreakPenalty.Value,
                    TargetStreak = form.barTargetStreak.Value,
                    ShowBlanks = form.chkShowBlanks.Checked,
                    SelectedLists = form.lstWordLists.CheckedItems.Cast<SpellingWordList>().ToList()
                };

                return true;
            }
            else
            {
                settings = null;
                return false;
            }
        }

        private void InitializeWordLists()
        {
            //TODO: Include performance in the display of these items (as ☆ and ★)
            lstWordLists.Items.Clear();
            foreach (var list in DataController.Root.Spelling.WordLists.Values)
            {
                lstWordLists.Items.Add(list);
            }
        }

        private void barDifficulty_Scroll(object sender, EventArgs e)
        {
            switch (barDifficulty.Value)
            {
                case 0:
                    lblDifficulty.Text = $"{DifficultyPrefix} Intro";
                    UpdateDifficultySettings(SpellingQuizSettings.Intro);
                    break;
                case 1:
                    lblDifficulty.Text = $"{DifficultyPrefix} Easy";
                    UpdateDifficultySettings(SpellingQuizSettings.Easy);
                    break;
                case 2:
                    lblDifficulty.Text = $"{DifficultyPrefix} Medium";
                    UpdateDifficultySettings(SpellingQuizSettings.Medium);
                    break;
                case 3:
                    lblDifficulty.Text = $"{DifficultyPrefix} Hard";
                    UpdateDifficultySettings(SpellingQuizSettings.Hard);
                    break;
                case 4:
                default:
                    lblDifficulty.Text = $"{DifficultyPrefix} Custom";
                    grpDifficultyDetails.Enabled = true;
                    break;
            }
        }
        private void UpdateDifficultySettings(SpellingQuizSettings difficulty)
        {
            grpDifficultyDetails.Enabled = false;
            chkShowBlanks.Checked = difficulty.ShowBlanks;
            barHintLetters.Value = Math.Min(barHintLetters.Maximum, difficulty.NumHintLetters);
            barHintChange.Value = Math.Min(barHintChange.Maximum, difficulty.NumHintLettersChange);
            barTargetStreak.Value = Math.Min(barTargetStreak.Maximum, difficulty.TargetStreak);
            barStreakPenalty.Value = Math.Min(barStreakPenalty.Maximum, difficulty.StreakPenalty);
            UpdateLabels();
        }
        private void UpdateLabels()
        {
            lblHintLetters.Text = $"{HintLettersPrefix}{Environment.NewLine}{barHintLetters.Value}";
            lblHintChange.Text = $"{HintChangePrefix}{Environment.NewLine}{barHintChange.Value}";
            lblTargetStreak.Text = $"{TargetStreakPrefix}{Environment.NewLine}{barTargetStreak.Value}";
            lblStreakPenalty.Text = $"{StreakPenaltyPrefix}{Environment.NewLine}{barStreakPenalty.Value}";

            double scoreMultiplier = SpellingQuizSettings.CalculateMultiplier(chkShowBlanks.Checked, barHintLetters.Value,
                barHintChange.Value, barTargetStreak.Value, barStreakPenalty.Value);
            lblScoreMultiplier.Text = $"{ScoreMultiplierPrefix} {scoreMultiplier:0.00%}";
        }
        private void CustomDifficultyChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (lstWordLists.SelectedIndices.Count <= 0)
            {
                MessageBox.Show("You must select at least one list!");
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
