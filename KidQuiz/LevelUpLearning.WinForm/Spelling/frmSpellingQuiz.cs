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

        Stack<string> WordStack = new Stack<string>();
        SpellingQuizSettings settings;

        public frmSpellingQuiz()
        {
            if (frmSpellingQuizDifficulty.ShowDialog(out SpellingQuizSettings settings))
            {
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                SayWord();
            }
            else
            {
                //TODO: If this value is low, let them know they were close
                int degreeOfDifference = txtInput.Text.DegreeOfDifferenceFrom(SpellingWordProgress[CurrentWord].Word.Word);
                
                if (SpellingWordProgress[CurrentWord].RecordAttempt(txtInput.Text.Trim(), settings))
                {
                    SpeechUtil.Congrats();
                    CustomMessageBox.Show("Correct!", "Correct!", Color.LightGreen);
                }
                else
                {
                    SpeechUtil.ExplainMisspelling(SpellingWordProgress[CurrentWord].Word.Word, txtInput.Text.Trim());
                    CustomMessageBox.Show(string.Format("WRONG!\r\nCorrect spelling is: {0}", CurrentWord), "WRONG!", Color.Coral);
                }

                PickNewWord();
            }
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            SayWord();
        }

        private void SayWord()
        {
            if (!string.IsNullOrEmpty(CurrentWord))
            {

                SpeechUtil.PromptToSpellWord(SpellingWordProgress[CurrentWord].Word);
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            //string statsMessage = "Stats:";

            //foreach (VocabWord vw in SpellingWords_old.Values.OrderBy(x => x.PercentCorrect))
            //{
            //    statsMessage += "\r\n" + vw.GetStatsMessage();// String.Format("\r\n{0}: {1} / {2} ({3:0.00}%)", vw.Word, vw.NumCorrect, vw.NumAttempts, (float)vw.NumCorrect / vw.NumAttempts);
            //}

            //MessageBox.Show(statsMessage);
            MessageBox.Show("I think I'll disable this button");
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
            if (hintLetters <= 0)
            {
                if (settings.ShowBlanks)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < Word.Word.Length; i++)
                    {
                        sb.Append("_ ");
                    }
                    return sb.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                var indices = Enumerable.Range(0, Word.Word.Length).ToList().OrderBy(x => DataController.Random.Next()).Take(hintLetters).ToList();

                var sb = new StringBuilder();
                for (int i = 0; i < Word.Word.Length; i++)
                {
                    if (indices.Contains(i)) sb.Append($"{Word.Word[i]} "); 
                    else sb.Append("_ ");
                }

                return sb.ToString();
            }
        }
    }
}
