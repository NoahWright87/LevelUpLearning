using LevelUpLearning.Core.Data;
using LevelUpLearning.Core.Extensions;
using LevelUpLearning.SpeechWindows;
using LevelUpLearning.WinForms.Spelling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmSpellingQuiz : Form
    {
        string CurrentWord;

        Dictionary<string, SpellingWordProgress> SpellingWordProgress;
        int WordsRemaining => SpellingWordProgress.Values.Sum(w => settings.TargetStreak - w.NumCorrect);
        int WordsToFinish => SpellingWordProgress.Count * settings.TargetStreak;
        double ProgressPercentage => (1 - (double)WordsRemaining / WordsToFinish);

        int Adventure_WordsCompleted = 0;
        int Adventure_WordsToFinish => settingsAdventure.RepsPerWord
            * settingsAdventure.TotalDeals
            * settingsAdventure.WordsPerDeal;
        double Adventure_ProgressPercentage => ((double)Adventure_WordsCompleted / Adventure_WordsToFinish);

        Stack<string> WordStack = new Stack<string>();
        SpellingQuizSettings settings;
        SpellingAdventureSettings settingsAdventure;

        double UserSpellingLevel => DataController.State.CurrentUser.Character.LevelSpelling;
        bool IsAdventureMode = false;
        double Adventure_LevelChange = 0;
        int Adventure_DealsLeft = 0;
        int Adventure_RepsLeft = 0;
        int Adventure_CurrentHints = 0;
        SpellingWord Adventure_CurrentWord;
        List<SpellingWord> Adventure_AvailableWords;
        Stack<SpellingWord> Adventure_RemainingWords;

        public frmSpellingQuiz()
        {
            //TODO: Pull up a settings form as well?
            if (MessageBox.Show("Do you want to try the new automatic progression system?", "Auto-Mode?", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                if (frmSpellingQuizDifficultyAdventure.ShowDialog(out SpellingAdventureSettings adventureSettings))
                {
                    this.IsAdventureMode = true;
                    InitializeComponent();
                    this.settingsAdventure = adventureSettings;
                    Adventure_DealsLeft = settingsAdventure.TotalDeals;
                    Adventure_DealCards();
                }
                else
                {
                    Close();
                }
            }
            else if (frmSpellingQuizDifficulty.ShowDialog(out SpellingQuizSettings settings))
            {
                this.IsAdventureMode = false;

                InitializeComponent();
                this.settings = settings;

                SpellingWordProgress = new Dictionary<string, SpellingWordProgress>();
                foreach (var word in settings.SelectedLists.SelectMany(list => list.Words))
                {
                    SpellingWordProgress.Add(word.Word, new SpellingWordProgress() { Word = word });
                }

                PickNewWord();
            }
            else
            {
                Close();
            }
        }

        
        private void Adventure_DealCards()
        {
            var minWordLevel = UserSpellingLevel - settingsAdventure.DifficultyRange;
            var maxWordLevel = UserSpellingLevel + settingsAdventure.DifficultyRange;

            Adventure_AvailableWords = DataController.Root.Spelling.WordLists.SelectMany(wl => wl.Value.Words)
                .Where(word => word.Word.Difficulty().IsWithin(minWordLevel, maxWordLevel))
                .Take(settingsAdventure.WordsPerDeal).ToList();

            Adventure_DealsLeft--;
            Adventure_RepsLeft = settingsAdventure.RepsPerWord;
            Adventure_ShuffleCards();
        }
        private void Adventure_ShuffleCards()
        {
            Adventure_RemainingWords = new Stack<SpellingWord>(Adventure_AvailableWords
                .OrderBy(x => DataController.Random.Next()));

            Adventure_RepsLeft--;

            Adventure_DrawCard();
        }
        private void Adventure_DrawCard()
        {
            if (!Adventure_RemainingWords.Any())
            {
                if (Adventure_RepsLeft > 0) Adventure_ShuffleCards();
                else if (Adventure_DealsLeft > 0) Adventure_DealCards();
                else Adventure_Finish();
            }
            else
            {
                Adventure_CurrentWord = Adventure_RemainingWords.Pop();
                Adventure_UpdateLabels();
                Adventure_SayWord();
            }
        }
        private void Adventure_Finish()
        {
            double previousLevel = UserSpellingLevel;
            double newLevel = UserSpellingLevel + Adventure_LevelChange;

            MessageBox.Show($"All done!  Spelling level changed from {previousLevel} to {newLevel}");

            DataController.State.CurrentUser.Character.LevelSpelling = newLevel;

            Close();
        }

        private void Adventure_UpdateLabels()
        {
            Adventure_CurrentHints = DataController.Random.Next(settingsAdventure.MinHints, settingsAdventure.MaxHints);
            lblHint.Text = Adventure_CurrentWord.GetPrompt(Adventure_CurrentHints, true);
            
            lblProgress.Text = $"{Adventure_ProgressPercentage:0.00%}";
            barRemaining.Value = (int)(Adventure_ProgressPercentage * barRemaining.Maximum);

            txtInput.Clear();
            txtInput.Focus();
        }

        private void PickNewWord()
        {
            int wordsLeft = WordsRemaining;
            if (wordsLeft <= 0)
            {
                Finish();
            }
            else
            {
                if (!WordStack.Any()) ShuffleWordStack();
                CurrentWord = WordStack.Pop();
                UpdateLabels();
                SayWord();
            }
        }
        private void ShuffleWordStack()
        {
            foreach (var val in SpellingWordProgress.Values
                                    .Where(w => w.NumCorrect < settings.TargetStreak) //Still need to be spelled
                                    .OrderBy(x => DataController.Random.Next())) //Sorted randomly
            {
                WordStack.Push(val.Word.Word);
            }
        }
        private void UpdateLabels()
        {
            lblHint.Text = SpellingWordProgress[CurrentWord].GetPrompt(settings).ToUpper();
            lblProgress.Text = $"{ProgressPercentage:0.00%}";
            barRemaining.Value = (int)(ProgressPercentage * barRemaining.Maximum);

            txtInput.Clear();
            txtInput.Focus();
        }
        private void Finish()
        {
            foreach (var list in settings.SelectedLists)
            {
                int totalAttempts = 0;
                int totalCorrects = 0;

                foreach (var w in list.Words)
                {
                    var wordPerf = SpellingWordProgress[w.Word];
                    totalAttempts += wordPerf.NumAttempts;
                    totalCorrects += wordPerf.NumCorrect;
                }
                
                DataController.Root.Spelling.SaveUserPerformance(DataController.State.CurrentUser, list, settings, totalAttempts, totalCorrects);
            }

            foreach (var word in SpellingWordProgress.Values)
            {
                DataController.Root.Spelling.SaveUserPerformance(DataController.State.CurrentUser, word.Word, word.NumAttempts, word.NumCorrect);
            }

            DataController.SaveRoot();

            SpeechUtil.ShutUpAndSay($"All done!  Let's see how you did, {DataController.State.CurrentUser.DisplayName}");
            new frmSpellingQuizPerformance(settings.SelectedLists.FirstOrDefault()?.ListName).ShowDialog(this);

            Close();
        }

        private void Adventure_RecordAnswer(bool isCorrect)
        {
            Adventure_WordsCompleted++;
            double trueDifficulty = Adventure_CurrentWord.Word.Difficulty() - Adventure_CurrentHints;
            double difference = trueDifficulty - UserSpellingLevel;

            //Flip the formulas if things were wrong
            if (!isCorrect) difference *= -1;

            //Base score is .1 or 1 / 10
            double numerator = 1;
            double denominator = 10;

            if (difference > 0)
            {
                numerator += difference;
            }
            else
            {
                denominator += difference;
            }

            double scoreChange = (numerator / denominator) * (isCorrect ? 1 : -1);
            Adventure_LevelChange += scoreChange;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                btnRepeat_Click(sender, e);
            }
            else if (Adventure_CurrentWord != null)
            {
                bool isCorrect = Adventure_CurrentWord.Word.Equals(txtInput.Text.Trim(), StringComparison.OrdinalIgnoreCase);
                Adventure_RecordAnswer(isCorrect);
                ShowAnswerResults(Adventure_CurrentWord.Word, txtInput.Text.Trim(), isCorrect);
                Adventure_DrawCard();
            }
            else
            {
                //TODO: If this value is low, let them know they were close
                int degreeOfDifference = txtInput.Text.DegreeOfDifferenceFrom(SpellingWordProgress[CurrentWord].Word.Word);

                var isCorrect = SpellingWordProgress[CurrentWord].RecordAttempt(txtInput.Text.Trim(), settings);
                ShowAnswerResults(SpellingWordProgress[CurrentWord].Word.Word, txtInput.Text.Trim(), isCorrect);
                
                PickNewWord();
            }
        }

        private void ShowAnswerResults(string correctSpelling, string yourSpelling, bool isCorrect)
        {
            if (isCorrect)
            {
                SpeechUtil.Congrats();
                CustomMessageBox.Show($"You spelled '{correctSpelling}' correctly!", "Correct!", Color.LightGreen);
            }
            else
            {
                SpeechUtil.ExplainMisspelling(correctSpelling, yourSpelling);
                CustomMessageBox.Show($@"Wrong! :(
You spelled: {yourSpelling}
Correct spelling: {correctSpelling}", "Wrong", Color.Coral);
            }
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            if (Adventure_CurrentWord != null) Adventure_SayWord();
            else SayWord();
        }

        private void SayWord()
        {
            if (!string.IsNullOrEmpty(CurrentWord))
            {
                SpeechUtil.PromptToSpellWord(SpellingWordProgress[CurrentWord].Word);
            }
        }
        private void Adventure_SayWord()
        {
            if (Adventure_CurrentWord != null)
            {
                SpeechUtil.PromptToSpellWord(Adventure_CurrentWord);
            }
        }
    }

    internal class SpellingWordProgress
    {
        public SpellingWord Word { get; set; }
        public int NumCorrect { get; private set; }
        public int NumAttempts { get; private set; }
        public int CurrentStreak { get; private set; }
        public float PercentCorrect => (NumAttempts <= 0 ? 0 : (float)NumCorrect / NumAttempts);

        public Dictionary<string, int> PreviousAttempts = new Dictionary<string, int>();

        public string GetStatsMessage()
        {
            string previousAttemptsMessage = PreviousAttempts.Count > 0 ? $"{Environment.NewLine}Incorrect spellings:" : "";
            foreach (string s in PreviousAttempts.Keys)
            {
                previousAttemptsMessage += $"{Environment.NewLine}{s} ({PreviousAttempts[s]})";
            }

            if (NumAttempts <= 0) return "";

            return $"{Word}: {NumCorrect} / {NumAttempts} ({PercentCorrect * 100}%){previousAttemptsMessage}";
        }

        public bool RecordAttempt(string attempt, SpellingQuizSettings settings)
        {
            NumAttempts++;
            if (Word.Word.Equals(attempt, StringComparison.InvariantCultureIgnoreCase))
            {
                NumCorrect++;
                CurrentStreak++;
                return true;
            }
            else
            {
                CurrentStreak = Math.Max(0, CurrentStreak - settings.StreakPenalty);

                //TODO: Calculate and record partial correctness

                if (!PreviousAttempts.ContainsKey(attempt))
                {
                    PreviousAttempts.Add(attempt, 0);
                }
                PreviousAttempts[attempt] = PreviousAttempts[attempt] + 1;
                return false;
            }
        }

        public string GetPrompt(SpellingQuizSettings settings)
        {
            int hintLetters = Math.Min(Word.Word.Length, settings.NumHintLetters) - CurrentStreak * settings.NumHintLettersChange;
            return Word.GetPrompt(hintLetters, settings.ShowBlanks);
        }
    }
}
