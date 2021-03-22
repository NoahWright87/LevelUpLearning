using LevelUpLearning.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LevelUpLearning.SpeechWindows;

namespace LevelUpLearning.WinForm
{
    public partial class frmQuiz : Form
    {
        string CurrentWord;

        Dictionary<string, VocabWord> SpellingWords;
        Stack<string> WordStack;

        DifficultySettings settings;

        Random r; //TODO: This should be in the background

        public frmQuiz() : this("") { }
        public frmQuiz(string words) : this(words, new DifficultySettings()) { }
        public frmQuiz(string words, DifficultySettings settings)
        {
            InitializeComponent();

            SpellingWords = new Dictionary<string, VocabWord>();
            foreach (string line in words.Split('\n'))
            {
                string[] lineParts = line.Trim().Split('\t');

                if (lineParts.Length >= 2)
                {
                    SpellingWords.Add(lineParts[0], new VocabWord(lineParts[0], lineParts[1]));
                }
            }

            this.settings = settings;

            r = new Random();
            WordStack = new Stack<string>();
            PickNewWord_WithStreaks();
        }

        public void PickNewWord_WithStreaks()
        {
            //Determine how many words are left
            int wordsLeft = 0;
            foreach (VocabWord word in SpellingWords.Values)
            {
                 wordsLeft += settings.TargetStreak - word.CurrentStreak;
            }
            if (wordsLeft > 0)
            {
                lblWordsLeft.Text = "Remaining: " + wordsLeft;
            }

            if (WordStack.Count <= 0)
            {
                foreach (string word in SpellingWords.OrderBy(x => r.NextDouble()).Where(x => x.Value.CurrentStreak < settings.TargetStreak).Select(x => x.Key))
                {
                    WordStack.Push(word);
                }
            }
            if (WordStack.Count <= 0)
            {
                lblWordsLeft.Text = "";
                btnStats_Click(null, null);
            }
            else
            {
                CurrentWord = WordStack.Pop();
                int calculatedDifficulty = settings.Difficulty + settings.DifficultyPerStreak * SpellingWords[CurrentWord].CurrentStreak;

                if (calculatedDifficulty < 0)
                {
                    lblHint.Text = "";
                }
                else
                {
                    lblHint.Text = GetHintFor(CurrentWord, calculatedDifficulty);
                }

                txtInput.Clear();
                txtInput.Focus();

                SayWord();
            }
        }

        public string GetHintFor(string word, int difficulty)
        {
            if (difficulty > word.Length) return "";

            word = word.ToUpper();
            StringBuilder newWord = new StringBuilder();
            List<int> positionsLeft = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                positionsLeft.Add(i);
            }

            for (int i = 0; i < difficulty; i++)
            {
                //newWord[r.Next(word.Length)] = '_';
                positionsLeft.RemoveAt(r.Next(positionsLeft.Count));
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (positionsLeft.Contains(i))
                {
                    newWord.Append(word[i] + " ");
                }
                else
                {
                    newWord.Append("_ ");
                }
            }

            return newWord.ToString();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() == "")
            {
                SayWord();
            }
            else
            {
                if (SpellingWords[CurrentWord].RecordAttempt(txtInput.Text.Trim(), settings.StreakStyle))
                {
                    CustomMessageBox.Show("Correct!", "Correct!", Color.LightGreen);
                }
                else
                {
                    SpeechUtil.ExplainMisspelling(SpellingWords[CurrentWord].Word, txtInput.Text.Trim());
                    CustomMessageBox.Show(string.Format("WRONG!\r\nCorrect spelling is: {0}", CurrentWord), "WRONG!", Color.Coral);
                }

                PickNewWord_WithStreaks();
            }
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            SayWord();
        }

        private void SayWord()
        {
            var currentWord = SpellingWords[CurrentWord];
            SpeechUtil.PromptToSpellWord(currentWord.Word, currentWord.ExampleSentence);
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            string statsMessage = "Stats:";

            foreach (VocabWord vw in SpellingWords.Values.OrderBy(x => x.PercentCorrect))
            {
                statsMessage += "\r\n" + vw.GetStatsMessage();// String.Format("\r\n{0}: {1} / {2} ({3:0.00}%)", vw.Word, vw.NumCorrect, vw.NumAttempts, (float)vw.NumCorrect / vw.NumAttempts);
            }

            MessageBox.Show(statsMessage);
        }
    }
}
