using System;

namespace LevelUpLearning.Core
{
    public static class Utils
    {
        public static Random Random { get; private set; } = new Random();

        public static bool FlipCoin => Random.Next(2) == 0;

        public static double LevelChange(double currentLevel, double problemLevel, bool isCorrect)
        {
            double difference = problemLevel - currentLevel;

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
                denominator -= difference;
            }

            var change = (numerator / denominator) * (isCorrect ? 1 : -1);
            return change;
        }
    }
}
