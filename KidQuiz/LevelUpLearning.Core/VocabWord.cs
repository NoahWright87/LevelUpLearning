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
        public float PercentCorrect => (NumAttempts <= 0 ? 0 : (float)NumCorrect / NumAttempts);

        public Dictionary<string, int> PreviousAttempts;

        public VocabWord(string Word, string ExampleSentence)
        {
            this.Word = Word;
            this.ExampleSentence = ExampleSentence;

            PreviousAttempts = new Dictionary<string, int>();
        }

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

        //TODO: Consider storing this sessions's attempts and performance, then combining those into 
        //  a lifetime performance stat that is stored separately
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
                //TODO: Looking back, I'm not sure this is the best way to handle this
                //  I feel like this class should (at most) store the raw performance data, and decisions about what to do with it
                //  (including how to calculate a 'streak' should be done elsewhere.  Give this one some thought
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
