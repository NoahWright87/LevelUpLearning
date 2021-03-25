namespace LevelUpLearning.Core
{
    /// <summary>
    /// This stores a collection of settings that change the overall difficulty of a spelling exercise.
    /// </summary>
    public class DifficultySettings
    {
        /// <summary>
        /// Friendly name for the difficulty (e.g.: Easy, Medium, Hard)
        /// </summary>
        public string Name;
        /// <summary>
        /// Streak style, indicating how harshly you're punished for mistakes
        /// </summary>
        public StreakStyle StreakStyle;
        /// <summary>
        /// The amount of letters obscured from the vocab word
        /// </summary>
        public int Difficulty;
        /// <summary>
        /// How much the difficulty increases as your streak increments
        /// </summary>
        public int DifficultyPerStreak;
        /// <summary>
        /// VocabWord must reach this streak before it stops being tested
        /// </summary>
        public int TargetStreak;
    }
}
