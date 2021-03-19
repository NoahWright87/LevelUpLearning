using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace KidQuiz
{
    public partial class frmQuiz : Form
    {
        string CurrentWord;

        Dictionary<string, VocabWord> SpellingWords;
        //List<String> SpellingWords_OLD;
        Stack<string> WordStack;

        //int loop = 0;
        //int MAX_LOOPS = 1;
        //int FAIL_LOOPS = 4;

        public enum StreakStyle
        {
            MaintainStreak,
            IncrementalLoss,
            TotalLoss
        }

        //StreakStyle streakStyle = StreakStyle.IncrementalLoss;
        //int difficulty = 100;
        //int difficultyPerStreak = 2;
        //int targetStreak = 2;
        DifficultySettings settings;

        //Dictionary<String, QuizStats> Stats;
        Random r;

        SpeechSynthesizer synth;

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


            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Rate = 0;// -5;
            synth.SelectVoice("Microsoft Zira Desktop");

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

            //CurrentWord = SpellingWords[r.Next(SpellingWords.Count)];
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

                //SpellingWords[CurrentWord].NumAttempts++;

                //if (txtInput.Text.Trim().Equals(CurrentWord, StringComparison.InvariantCultureIgnoreCase))
                if (SpellingWords[CurrentWord].RecordAttempt(txtInput.Text.Trim(), settings.StreakStyle))
                {
                    //SpellingWords[CurrentWord].NumCorrect++;
                    
                    CustomMessageBox.Show("Correct!", "Correct!", Color.LightGreen);
                }
                else
                {
                    SpellingWords[CurrentWord].SpeakMisspelling(synth, txtInput.Text.Trim());
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
            synth.SpeakAsyncCancelAll();
            //PromptBuilder pb = new PromptBuilder();
            //pb.AppendSsmlMarkup(String.Format("Spell the word: <prosody rate=\"slow\" pitch=\"x-low\">{0}</prosody>", CurrentWord));
            //synth.SpeakAsync(pb);
            SpellingWords[CurrentWord].Speak(synth);

            //PromptBuilder pb = new PromptBuilder();
            //pb.StartParagraph();
            //pb.StartSentence();

            //pb.AppendText("Spell the word:");

            //pb.StartStyle(new PromptStyle()
            //{
            //    Rate = PromptRate.Slow,
            //    Emphasis = PromptEmphasis.Strong,
            //    Volume = PromptVolume.ExtraLoud
            //});
            

            //pb.AppendText(CurrentWord);

            //pb.EndStyle();

            //pb.EndSentence();
            //pb.EndParagraph();


            //String speech = String.Format("Spell the word: {0}", CurrentWord);

            //Prompt x = new Prompt("", SynthesisTextFormat.Ssml);

            //synth.SpeakAsync(pb);

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
