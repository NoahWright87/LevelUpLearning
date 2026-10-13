namespace LevelUpLearning.Core.Extensions
{
    public static class IntExtensions
    {
        public static string ToStars(this int i)
        {
            switch(i)
            {
                case 5: return  "★★★★★";
                case 4: return  "★★★★☆";
                case 3: return  "★★★☆☆";
                case 2: return  "★★☆☆☆";
                case 1: return  "★☆☆☆☆";
                default: return "☆☆☆☆☆";
            }
        }
    }
}
