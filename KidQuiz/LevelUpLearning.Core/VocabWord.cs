using System;
using System.Collections.Generic;

namespace LevelUpLearning.Core
{
    public class VocabWord
    {
        public string Word { get; private set; }
        public string ExampleSentence { get; private set; }

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

            this.PreviousAttempts = new Dictionary<string, int>();
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

        public bool RecordAttempt(string attempt, StreakStyle style)
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
                    case StreakStyle.MaintainStreak:
                        //Do nothing
                        break;
                    case StreakStyle.IncrementalLoss:
                        if (CurrentStreak > 0) CurrentStreak--;
                        break;
                    case StreakStyle.TotalLoss:
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
