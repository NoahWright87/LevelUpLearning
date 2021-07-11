using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmSpellingQuizDifficultyAdventure : Form
    {
        private readonly string DifficultyPrefix;
        private readonly string HintLettersPrefix;
        private readonly string NumDealsPrefix;
        private readonly string WordsPerDealPrefix;
        private readonly string NumRepsPrefix;
        private readonly string TotalPrefix;

        public frmSpellingQuizDifficultyAdventure()
        {
            InitializeComponent();

            //Save the current value of the labels so we can append to them later
            DifficultyPrefix = lblDifficulty.Text;
            HintLettersPrefix = lblHintLetters.Text;
            NumDealsPrefix = lblDeals.Text;
            WordsPerDealPrefix = lblWordsPerDeal.Text;
            NumRepsPrefix = lblReps.Text;
            TotalPrefix = lblTotal.Text;

            //barDifficulty.Value = 0; //TODO: Default to last setting
            barDifficulty_Scroll(null, null);
        }

        public static bool ShowDialog(out SpellingAdventureSettings settings)
        {

            var form = new frmSpellingQuizDifficultyAdventure();
            if (form.ShowDialog() == DialogResult.OK)
            {
                settings = new SpellingAdventureSettings()
                {
                    DifficultyRange = form.barDifficultyRange.Value,
                    RepsPerWord = form.barReps.Value,
                    TotalDeals = form.barNumDeals.Value,
                    WordsPerDeal = form.barWordsPerDeal.Value,
                    MinHints = form.barHintMin.Value,
                    MaxHints = form.barHintMax.Value
                };

                return true;
            }
            else
            {
                settings = null;
                return false;
            }
        }

        private void barDifficulty_Scroll(object sender, EventArgs e)
        {
            switch (barDifficulty.Value)
            {
                case 0:
                    lblDifficulty.Text = $"{DifficultyPrefix} Quick";
                    UpdateDifficultySettings(SpellingAdventureSettings.Quick);
                    break;
                case 1:
                    lblDifficulty.Text = $"{DifficultyPrefix} Normal";
                    UpdateDifficultySettings(SpellingAdventureSettings.Normal);
                    break;
                case 2:
                    lblDifficulty.Text = $"{DifficultyPrefix} Brutal";
                    UpdateDifficultySettings(SpellingAdventureSettings.Brutal);
                    break;
                case 3:
                    lblDifficulty.Text = $"{DifficultyPrefix} Random";
                    UpdateDifficultySettings(SpellingAdventureSettings.Random);
                    break;
                case 4:
                default:
                    lblDifficulty.Text = $"{DifficultyPrefix} Custom";
                    grpDifficultyDetails.Enabled = true;
                    break;
            }
        }
        private void UpdateDifficultySettings(SpellingAdventureSettings difficulty)
        {
            grpDifficultyDetails.Enabled = false;
            barDifficultyRange.Value = difficulty.DifficultyRange;
            barReps.Value = difficulty.RepsPerWord;
            barNumDeals.Value = difficulty.TotalDeals;
            barWordsPerDeal.Value = difficulty.WordsPerDeal;
            barHintMin.Value = difficulty.MinHints;
            barHintMax.Value = difficulty.MaxHints;

            UpdateLabels();
        }
        private void UpdateLabels()
        {
            var hintLetters = (barHintMin.Value == barHintMax.Value) ? barHintMin.Value.ToString() : $"{barHintMin.Value} - {barHintMax.Value}";

            lblDifficultyRange.Text = $"{DifficultyPrefix}{Environment.NewLine}{barDifficultyRange.Value}";
            lblDeals.Text = $"{NumDealsPrefix}{Environment.NewLine}{barNumDeals.Value}";
            lblWordsPerDeal.Text = $"{WordsPerDealPrefix}{Environment.NewLine}{barWordsPerDeal.Value}";
            lblReps.Text = $"{NumRepsPrefix}{Environment.NewLine}{barReps.Value}";
            lblHintLetters.Text = $"{HintLettersPrefix}{Environment.NewLine}{hintLetters}";

            int totalWords = barNumDeals.Value * barWordsPerDeal.Value * barReps.Value;
            lblTotal.Text = $"{TotalPrefix} {totalWords}";
        }
        private void CustomDifficultyChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HintsChange(object sender, EventArgs e)
        {
            barHintMax.Minimum = barHintMin.Value;
            barHintMin.Maximum = barHintMax.Value;
            UpdateLabels();
        }
        private void ValuesChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show($@"Pick one of the preset difficulty settings, or change the sliders to make a customer difficulty.

The quiz will pick words close to your spelling level.  Difficulty Range increases how far outside your level it will pick.
A random selection of words will be 'dealt' to you.
- Number of deals is how many times new words will be picked
- Words per draw is how many words are picked each time
- Reps per word is how many times you'll do the same words before picking new ones
Words will have a random number of hint letters.  Set the min and max number of hints to change how many hints you get.");
        }
    }
}
