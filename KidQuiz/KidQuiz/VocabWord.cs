using System;
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace KidQuiz
{
    public class VocabWord
    {
        private readonly string ssmlFormatString = @"<break strength=""x-weak"" /><prosody rate=""slow"" pitch=""x-low""><emphasis level=""strong"">{0}</emphasis></prosody><break strength=""x-weak"" />";

        public string Word { get; private set; }
        public string ExampleSentence { get; private set; }
        public PromptBuilder pb;

        public int NumCorrect { get; private set; }
        public int NumAttempts { get; private set; }
        public int CurrentStreak { get; private set; }
        public float PercentCorrect
        {
            get
            {
                if (NumAttempts <= 0) return 0;
                else return ((float)NumCorrect / NumAttempts);
            }
        }

        string prompt;

        public Dictionary<string, int> PreviousAttempts;

        public VocabWord(string Word, string ExampleSentence)
        {
            this.Word = Word;
            this.ExampleSentence = ExampleSentence;

            string wordWithSsml = string.Format(ssmlFormatString, Word);
            prompt = string.Format("Spell {0}.  {1}.", wordWithSsml, ExampleSentence.Replace(Word, wordWithSsml));
            pb = new PromptBuilder();
            pb.AppendSsmlMarkup(prompt);

            this.PreviousAttempts = new Dictionary<string, int>();
        }

        public void Speak(SpeechSynthesizer synth)
        {
            synth.SpeakAsync(pb);
        }

        public void SpeakMisspelling(SpeechSynthesizer synth, string incorrectSpelling)
        {


            string msg = string.Format("The word was {0}.  You spelled {1}.",
                                       string.Format(ssmlFormatString, this.Word),
                                       string.Format(ssmlFormatString, incorrectSpelling));

            var x = new PromptBuilder();
            x.AppendSsmlMarkup(msg);

            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(x);
        }

        public string GetStatsMessage()
        {
            string previousAttempts = "";
            foreach (string s in PreviousAttempts.Keys)
            {
                previousAttempts += string.Format("{0} ({1}), ", s, PreviousAttempts[s]);
            }

            if (this.NumAttempts <= 0) return "";

            return string.Format("{0}: {1} / {2} ({3:0.00}%).{5}{4}", this.Word, this.NumCorrect, this.NumAttempts, (float)this.NumCorrect / this.NumAttempts * 100, previousAttempts,
                this.NumAttempts > this.NumCorrect ? "  Incorrect spellings: " : "");
        }

        public bool RecordAttempt(string attempt, frmQuiz.StreakStyle style)
        {
            NumAttempts++;
            if (Word.Equals(attempt, StringComparison.InvariantCultureIgnoreCase))
            {
                NumCorrect++;
                CurrentStreak++;
                return true;
            }
            else
            {
                switch (style)
                {
                    case frmQuiz.StreakStyle.MaintainStreak:
                        //Do nothing
                        break;
                    case frmQuiz.StreakStyle.IncrementalLoss:
                        if (CurrentStreak > 0) CurrentStreak--;
                        break;
                    case frmQuiz.StreakStyle.TotalLoss:
                        CurrentStreak = 0;
                        break;
                }
                
                if (!PreviousAttempts.ContainsKey(attempt))
                {
                    PreviousAttempts.Add(attempt, 0);
                }
                PreviousAttempts[attempt] = PreviousAttempts[attempt] + 1;
                return false;
            }
        }
    }

}
