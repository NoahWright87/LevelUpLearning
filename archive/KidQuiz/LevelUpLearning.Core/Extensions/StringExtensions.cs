using System.Linq;
using System.Text.RegularExpressions;

namespace LevelUpLearning.Core.Extensions
{
    public static class StringExtensions
    {
        const double DifficultyPerLength = 1;
        const double DifficultyPerSameLetter = 1;
        const double DifficultyPerSameType = 1;
        const double DifficultyPerTrickyLetter = 0.5;
        const double DifficultyPerTrickyPattern = 2;

        static readonly Regex TrickyPatterns = new Regex("(e$|es$|ies$|^qu)", RegexOptions.IgnoreCase);
        static readonly Regex TrickyLetters = new Regex("(q|z)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Calculates a word's approximate difficulty.  Difficulty is based on:
        /// - Overall length of the word
        /// - Number of consontants / vowels in a row
        /// - Repetition of the same letter (especially if next to each other)
        /// - Presence of tricky letters (q, z) and patterns (ies, sneaky e, etc)
        /// </summary>
        public static double Difficulty(this string s)
        {
            double difficulty = s.Length * DifficultyPerLength;

            char lastChar = s.FirstOrDefault();
            int sameCharInARow = 0;
            int sameTypeInARow = 0;
            for (int i = 1; i < s.Length; i++)
            {
                char currChar = s[i];
                if (lastChar == currChar)
                {
                    sameCharInARow++;
                    sameTypeInARow++;
                }
                else if (currChar.IsVowel() == lastChar.IsVowel())
                {
                    sameCharInARow = 0;
                    sameTypeInARow++;
                }
                else
                {
                    sameCharInARow = 0;
                    sameTypeInARow = 0;
                }

                lastChar = currChar;

                difficulty += sameCharInARow * DifficultyPerSameLetter;
                difficulty += sameTypeInARow * DifficultyPerSameType;
            }

            difficulty += TrickyPatterns.Matches(s).Count * DifficultyPerTrickyPattern;
            difficulty += TrickyLetters.Matches(s).Count * DifficultyPerTrickyLetter;

            return difficulty;
        }

        public static int DegreeOfDifferenceFrom(this string s, string other)
        {
            //TODO: Calculate this
            return 0;
        }
    }
}
