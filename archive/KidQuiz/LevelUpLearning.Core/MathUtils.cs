using org.mariuszgromada.math.mxparser;
using LevelUpLearning.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelUpLearning.Core
{
    public static class MathUtils
    {
        //Scoring details
        private static double ScorePerTerm => 4.0;
        private static double ScorePerDigit => 5.0;
        private static double DecimalMultiplier => 2;
        private static double NegativityMultiplier => 1.5;
        private static Dictionary<MathOperator, double> OperatorLevels => new Dictionary<MathOperator, double>()
        {
            [MathOperator.Add] = 1,
            [MathOperator.Subtract] = 10,
            [MathOperator.Multiply] = 20,
            [MathOperator.Divide] = 30,
            [MathOperator.Exponent] = 100,
        };

        public static (string, double) GetMathProblemFor(double targetLevel)
        {
            var mathOperator = OperatorLevels.Where(l => l.Value <= targetLevel)
                .OrderBy(x => DataController.Random.Next())
                .FirstOrDefault().Key;

            double level = OperatorLevels[mathOperator];

            int numDigits = 1 + DataController.Random.Next(MaxExtraDigits(level, targetLevel));
            level += DigitNumScoreChange(numDigits);

            int numTerms = 2 + DataController.Random.Next(MaxExtraTerms(level, targetLevel));
            level *= TermNumScoreMultiplier(numTerms);

            bool hasDecimals = false;
            if (level * DecimalMultiplier <= targetLevel) hasDecimals = Utils.FlipCoin;
            if (hasDecimals) level *= DecimalMultiplier;

            bool hasNegatives = false;
            if (mathOperator != MathOperator.Subtract &&
                mathOperator != MathOperator.Add && 
                level * NegativityMultiplier <= targetLevel) hasNegatives = Utils.FlipCoin;
            if (hasNegatives) level *= NegativityMultiplier;

            return (GetMathProblem(mathOperator, numDigits, numTerms, hasDecimals, hasNegatives), level);
        }

        private static string GetMathProblem(MathOperator op, int numDigits, int numTerms, bool hasDecimals, bool hasNegatives)
        {
            StringBuilder sb = new StringBuilder();

            int minTerm = (int)Math.Pow(10, numDigits - 1);
            int maxTerm = minTerm * 10;

            List<double> terms = new List<double>();
            for (int i = 0; i < numTerms; i++)
            {
                var newTerm = (double)Utils.Random.Next(minTerm, maxTerm);
                if (hasDecimals)
                {
                    int divisor = (int)Math.Pow(10, Utils.Random.Next(0, numDigits));
                    newTerm /= divisor;
                }
                if (hasNegatives && Utils.FlipCoin)
                {
                    newTerm *= -1;
                }

                terms.Add(newTerm);
            }

            return string.Join(OperatorStrings[op], terms);
        }

        public static double SolveMathProblem(string problem)
        {
            return new Expression(problem).calculate();
        }

        private static Dictionary<MathOperator, string> OperatorStrings => new Dictionary<MathOperator, string>()
        {
            [MathOperator.Add] = " + ",
            [MathOperator.Subtract] = " - ",
            [MathOperator.Multiply] = " * ",
            [MathOperator.Divide] = " / ",
            [MathOperator.Exponent] = "^"
        };

        private static int MaxExtraTerms(double level, double target)
        {
            //2 terms (x1), 3 terms (x2), 4 terms (x3), etc
            var multiple = target / level;
            return (int)(multiple / ScorePerTerm);
        }
        private static double TermNumScoreMultiplier(int numTerms)
        {
            return (numTerms - 1) * ScorePerTerm;
        }

        private static int MaxExtraDigits(double level, double target)
        {
            //1 digit (+0), 2 digits (+2), 3 digits (+4)
            var diff = target - level;
            return (int)(diff / ScorePerDigit);
        }
        private static double DigitNumScoreChange(int numDigits)
        {
            return (numDigits - 1) * ScorePerDigit;
        }
    }
}
