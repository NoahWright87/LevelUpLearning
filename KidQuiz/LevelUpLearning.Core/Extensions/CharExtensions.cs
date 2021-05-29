using System.Linq;

namespace LevelUpLearning.Core.Extensions
{
    public static class CharExtensions
    {
        const string Vowels = "aeiouAEIOU";

        public static bool IsVowel(this char c)
        {
            return Vowels.Contains(c);
        }
        public static bool IsConsonant(this char c)
        {
            return !c.IsVowel();
        }
    }
}
