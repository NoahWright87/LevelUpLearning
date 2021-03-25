namespace LevelUpLearning.Core
{
    /// <summary>
    /// Indicates how failure is handled when determining a winning streak.
    /// </summary>
    public enum StreakStyle
    {
        /// <summary>
        /// Getting a word incorrect does nothing to the word's streak
        /// </summary>
        MaintainStreak,
        /// <summary>
        /// Getting a word incorrect reduces the streak by 1
        /// </summary>
        IncrementalLoss,
        /// <summary>
        /// Getting a word incorrect resets the streak to 0
        /// </summary>
        TotalLoss
    }
}
