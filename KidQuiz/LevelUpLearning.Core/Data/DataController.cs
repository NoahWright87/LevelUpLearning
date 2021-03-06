using LevelUpLearning.Core.Extensions;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LevelUpLearning.Core.Data
{
    /// <summary>
    /// Singleton to hold data for the program
    /// </summary>
    public class DataController
    {
        //TODO: Since this class is tasked with specifically loading the data from an XML file, should it live elsewhere?
        //This project is for core classes used across all projects, this would be something that's Windows-specific

        /// <summary>
        /// Backing variable for Instance.  DO NOT USE DIRECTLY
        /// </summary>
        private static DataController _instance;
        /// <summary>
        /// Singleton to store this class's data
        /// </summary>
        private static DataController Instance
        {
            get
            {
                if (_instance == null) _instance = new DataController();
                return _instance;
            }
        }

        private static readonly string DATA_FILENAME = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Level Up Learning",
                "lul.dat");
        
        private DataRoot _root;
        public static DataRoot Root => Instance._root;

        private readonly DataState _state = new DataState();
        public static DataState State => Instance._state;

        private readonly Random _random = new Random();
        public static Random Random => Instance._random;

        private DataController()
        {
            Load();
        }
        private XmlSerializer Serializer => new XmlSerializer(typeof(DataRoot));
        private static XmlSerializer SpellingListSerializer => new XmlSerializer(typeof(SpellingWordList));
        private void Save()
        {
            //TODO: What if we can't make the save data?
            if (!Directory.Exists(Path.GetDirectoryName(DATA_FILENAME)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DATA_FILENAME));
            }
            using (XmlWriter writer = XmlWriter.Create(DATA_FILENAME))
            {
                Serializer.Serialize(writer, _root);
            }
        }
        public static void SaveRoot()
        {
            Instance.Save();
        }
        private void Load()
        {
            if (File.Exists(DATA_FILENAME))
            {
                using (XmlReader reader = XmlReader.Create(DATA_FILENAME))
                {
                    _root = (DataRoot)Serializer.Deserialize(reader);
                }
            }
            else
            {
                _root = new DataRoot();
                Save();
            }
        }

        public static bool TryExportWordFile(SpellingWordList list, string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    SpellingListSerializer.Serialize(writer, list);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool TryImportWordXml(string xml)
        {
            try
            {
                using (var reader = new StringReader(xml))
                {
                    var list = (SpellingWordList)SpellingListSerializer.Deserialize(reader);
                    if (Root.Spelling.WordLists.ContainsKey(list.ListName))
                    {
                        //TODO: Merge the lists together instead
                        return false;
                    }
                    else
                    {
                        Root.Spelling.WordLists.Add(list.ListName, list);
                        SaveRoot();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool TryImportWordFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            try
            {
                string xml = File.ReadAllText(filePath);
                return TryImportWordXml(xml);
            }
            catch
            {
                return false;
            }
        }
    }

    public class DataState
    {
        public UserData CurrentUser { get; set; }
    }


    //TODO: Put serialization decorators on these classes
    //TODO: Split these files up once I'm sure I've got all the data I need
    public class DataRoot //top-level container for all the data
    {
        public UserData LastUser { get; set; }
        public SerializableDictionary<string, UserData> Users { get; set; } = new SerializableDictionary<string, UserData>();
        public SpellingData Spelling { get; set; } = new SpellingData();
    }

    public class SpellingData
    {
        public SerializableDictionary<string, SpellingWordList> WordLists { get; set; }
            = new SerializableDictionary<string, SpellingWordList>();
        public SerializableDictionary<string, UserSpellingPerformance> UserPerformances { get; set; }
            = new SerializableDictionary<string, UserSpellingPerformance>();

        public double MaxDifficulty => WordLists.Values.SelectMany(wl => wl.Words)
            .Max(w => w.Word.Difficulty());

        public void SaveUserPerformance(UserData user, SpellingWord word, int attempts, int correct)
        {
            //Create a default if one doesn't already exist
            if (!UserPerformances.ContainsKey(user.UserName))
            {
                UserPerformances.Add(user.UserName, new UserSpellingPerformance() { UserName = user.UserName });
            }
            var perf = UserPerformances[user.UserName];

            if (!perf.WordPerformances.ContainsKey(word.Word))
            {
                perf.WordPerformances.Add(word.Word, new SpellingWordPerformance()
                {
                    SpellingWord = word.Word
                });
            }
            var wordPerf = perf.WordPerformances[word.Word];
            wordPerf.LastAttemptDate = DateTime.Today;
            wordPerf.LastCorrect = correct;
            wordPerf.LastAttempts = attempts;
            wordPerf.TotalCorrect += correct;
            wordPerf.TotalAttempts += attempts;
        }
        public void SaveUserPerformance(UserData user, SpellingWordList list, SpellingQuizSettings settings, int totalAttempts, int totalCorrect)
        {
            if (!UserPerformances.ContainsKey(user.UserName))
            {
                UserPerformances.Add(user.UserName, new UserSpellingPerformance() { UserName = user.UserName });
            }
            var perf = UserPerformances[user.UserName];

            if (!perf.ListPerformances.ContainsKey(list.ListName))
            {
                perf.ListPerformances.Add(list.ListName, new SpellingWordListPerformance() { });
            }
            var listPerf = perf.ListPerformances[list.ListName];
            listPerf.LastAttemptDate = DateTime.Today;
            listPerf.TotalAttempts++;
            listPerf.LastPercentage = (double)totalCorrect / totalAttempts * settings.DifficultyMultiplier;
            listPerf.LastScore = list.OverallDifficulty * listPerf.LastPercentage;
            if (listPerf.LastPercentage > listPerf.BestPercentage)
            {
                listPerf.BestPercentage = listPerf.LastPercentage;
                listPerf.BestScore = list.OverallDifficulty * listPerf.BestPercentage;
            }
        }
    }
    public class SpellingWordList
    {
        public string ListName { get; set; }
        public List<SpellingWord> Words { get; set; } = new List<SpellingWord>();
        public double GradeLevel { get; set; }

        public override string ToString() => ListName;
        public double OverallDifficulty
        {
            get
            {
                double difficulty = 0;
                foreach (var w in Words)
                {
                    difficulty += w.Word.Difficulty();
                }
                return difficulty;
            }
        }
    }
    public class SpellingWord
    {
        public string Word { get; set; }
        public string Prompt { get; set; }

        public override string ToString() => Word;

        public string GetPrompt(int hintLetters, bool showBlanks)
        {
            if (hintLetters <= 0)
            {
                if (showBlanks)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < Word.Length; i++)
                    {
                        sb.Append("_ ");
                    }
                    return sb.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                var indices = Enumerable.Range(0, Word.Length).ToList().OrderBy(x => DataController.Random.Next()).Take(hintLetters).ToList();

                var sb = new StringBuilder();
                for (int i = 0; i < Word.Length; i++)
                {
                    if (indices.Contains(i)) sb.Append($"{Word[i]} ");
                    else sb.Append("_ ");
                }

                return sb.ToString();
            }
        }
    }
    public class UserSpellingPerformance
    {
        public string UserName { get; set; }
        public SerializableDictionary<string, SpellingWordPerformance> WordPerformances { get; set; }
            = new SerializableDictionary<string, SpellingWordPerformance>();
        public SerializableDictionary<string, SpellingWordListPerformance> ListPerformances { get; set; }
             = new SerializableDictionary<string, SpellingWordListPerformance>();
    }
    public class SpellingWordPerformance
    {
        public DateTime LastAttemptDate { get; set; }
        public string SpellingWord { get; set; }
        public int TotalAttempts { get; set; }
        public int TotalCorrect { get; set; }
        public double TotalCorrectPercentage => TotalAttempts > 0 ? (double)TotalCorrect / TotalAttempts : 0;
        public int LastAttempts { get; set; }
        public int LastCorrect { get; set; }
        public double CorrectPercentage => LastAttempts > 0 ? (double)LastCorrect / LastAttempts : 0;
    }
    public class SpellingWordListPerformance
    {
        public DateTime LastAttemptDate { get; set; }
        public int TotalAttempts { get; set; }
        public double LastPercentage { get; set; } = 0;
        public double LastScore { get; set; } = 0;
        public double BestPercentage { get; set; } = 0;
        public double BestScore { get; set; } = 0;
    }

    public class SpellingQuizSettings
    {
        #region Presets
        public static readonly SpellingQuizSettings Intro = new SpellingQuizSettings()
        {
            ShowBlanks = true,
            NumHintLetters = int.MaxValue,
            NumHintLettersChange = 0,
            TargetStreak = 2,
            StreakPenalty = 0
        };
        public static readonly SpellingQuizSettings Easy = new SpellingQuizSettings()
        {
            ShowBlanks = true,
            NumHintLetters = 4,
            NumHintLettersChange = 1,
            TargetStreak = 3,
            StreakPenalty = 0
        };
        public static readonly SpellingQuizSettings Medium = new SpellingQuizSettings()
        {
            ShowBlanks = true,
            NumHintLetters = 3,
            NumHintLettersChange = 2,
            TargetStreak = 3,
            StreakPenalty = 1
        };
        public static readonly SpellingQuizSettings Hard = new SpellingQuizSettings()
        {
            ShowBlanks = false,
            NumHintLetters = 0,
            NumHintLettersChange = 0,
            TargetStreak = 3,
            StreakPenalty = 2
        };
        #endregion

        public bool ShowBlanks { get; set; }
        public int NumHintLetters { get; set; }
        public int NumHintLettersChange { get; set; }
        public int TargetStreak { get; set; }
        public int StreakPenalty { get; set; }
        public List<SpellingWordList> SelectedLists { get; set; } = new List<SpellingWordList>();

        public double DifficultyMultiplier => 
            CalculateMultiplier(ShowBlanks, NumHintLetters, NumHintLettersChange, TargetStreak, StreakPenalty);
        public static double CalculateMultiplier(bool showBlanks, int hintLetters, int hintLettersChange, int streak, int penalty)
        {
            double blanksMultiplier = showBlanks ? 0.75 : 1.00;
            double hintsMultiplier = Math.Min(1.0, 1.0 - (0.05 * hintLetters) + (0.025 * hintLettersChange));
            double streakMultiplier = 0.70 + streak * 0.10;
            double penaltyMultipler = 1.0 + penalty * 0.05;

            return blanksMultiplier * hintsMultiplier * streakMultiplier * penaltyMultipler;
        }
    }
    public class SpellingAdventureSettings
    {
        public int DifficultyRange { get; set; } = 5;
        public int WordsPerDeal { get; set; } = 5;
        public int RepsPerWord { get; set; } = 2;
        public int TotalDeals { get; set; } = 2;
        public int MistakesPerHint { get; set; } = 2;

        public static readonly SpellingAdventureSettings Quick = new SpellingAdventureSettings()
        {
            DifficultyRange = 4,
            MistakesPerHint = 1,
            RepsPerWord = 2,
            TotalDeals = 2,
            WordsPerDeal = 5
        };

        public static readonly SpellingAdventureSettings Normal = new SpellingAdventureSettings()
        {
            DifficultyRange = 8,
            MistakesPerHint = 2,
            RepsPerWord = 1,
            TotalDeals = 4,
            WordsPerDeal = 7
        };

        public static readonly SpellingAdventureSettings Brutal = new SpellingAdventureSettings()
        {
            DifficultyRange = 12,
            MistakesPerHint = 3,
            RepsPerWord = 2,
            TotalDeals = 5,
            WordsPerDeal = 5
        };

        public static readonly SpellingAdventureSettings Random = new SpellingAdventureSettings()
        {
            DifficultyRange = 30,
            MistakesPerHint = 1,
            RepsPerWord = 1,
            TotalDeals = 10,
            WordsPerDeal = 3
        };
    }

    public class MathQuizSettings
    {
        //[Difficulty(DifficultyMultiplier = 2)]
        public int ProblemsToDo { get; set; } = 10;

        public double LevelRange { get; set; } = 1.5;

        //public double MaxLeftOperand { get; set; }
        //public double MaxRightOperand { get; set; }
        //public bool OnlyPositiveInputs { get; set; }
        //public bool OnlyPositiveResults { get; set; }
        //public bool OnlyWholeNumbers { get; set; }
        //public bool ShowVisuals { get; set; } //TODO: Use this to show visuals on quiz
        //public List<MathOperator> Operators { get; set; }

        ////Presets
        //public static MathQuizSettings SINGLE_DIGIT_ADDITION => new MathQuizSettings()
        //{

        //};
    }
    public class DifficultyAttribute : Attribute
    {
        //TODO: use this eventually, maybe to automatically scale difficulty ???
        public double DifficultyMultiplier { get; private set; }
    }

    public enum MathOperator
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Exponent
    }

    public class UserData
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        //TODO: Birthday, other demographics??
        public override string ToString() => DisplayName ?? UserName;

        public CharacterData Character { get; set; } = new CharacterData();
    }

    public class CharacterData
    {
        public int Level { get; set; } = 1;
        public int TotalExperience { get; set; } = 0;
        public double LevelSpelling { get; set; } = 0;
        public void SetSpellingLevel(double value)
        {
            LevelSpelling = value.Clamp(0, DataController.Root.Spelling.MaxDifficulty);
        }
        public double LevelMath { get; set; } = 0;

        public int CurrentHealth { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public int CurrentMana { get; set; } = 100;
        public int MaxMana { get; set; } = 100;
    }
}
