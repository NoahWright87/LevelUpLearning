namespace LevelUpLearning.Core.Extensions
{
    public static class DoubleExtensions
    {
        public static int StarsCount(this double d)
        {
            if (d >= 0.95) return 5;
            else if (d >= 0.75) return 4;
            else if (d >= 0.50) return 3;
            else if (d >= 0.25) return 2;
            else if (d > 0) return 1;
            else return 0;
        }

        public static string ToStars(this double d)
        {
            return d.StarsCount().ToStars();
        }

        public static bool IsWithin(this double value, double min, double max)
        {
            return value <= max && value >= min;
        }
    }
}
