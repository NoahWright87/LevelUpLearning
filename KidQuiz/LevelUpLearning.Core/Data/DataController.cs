using Microsoft.VisualStudio.Services.Common;
using System.Collections.Generic;
using System.IO;
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

        private static readonly string DATA_FILENAME = "lul.dat";
        
        private DataRoot _root;
        public static DataRoot Root => Instance._root;

        private DataState _state = new DataState();
        public static DataState State => Instance._state;

        private DataController()
        {
            Load();
            ////TODO: Some error handling in case the load fails
            //if (!File.Exists(DATA_FILENAME))
            //{
            //    _root = new DataRoot();
            //    Save();
            //}
            //else
            //{
            //    Load();
            //}
        }
        private XmlSerializer Serializer => new XmlSerializer(typeof(DataRoot));
        private void Save()
        {
            //TODO: Blow up the whole program if we can't make the save data
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
    }
    public class SpellingWordList
    {
        public string ListName { get; set; }
        public List<SpellingWord> Words { get; set; } = new List<SpellingWord>();
        public double GradeLevel { get; set; }

        public override string ToString() => ListName;
    }
    public class SpellingWord
    {
        public string Word { get; set; }
        public string Prompt { get; set; }

        public override string ToString() => Word;
    }
    public class UserSpellingPerformance
    {
        public string UserName { get; set; }
        public SerializableDictionary<string, SpellingPerformance> Performances { get; set; }
            = new SerializableDictionary<string, SpellingPerformance>();
    }
    public class SpellingPerformance
    {
        public string SpellingWordListName { get; set; }
        public string SpellingWord { get; set; }
        public int Attempts { get; set; }
        public int CorrectAnswers { get; set; }
        public double CorrectPercentage => Attempts > 0 ? (double)CorrectAnswers / Attempts : 0;
    }

    public class UserData
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        //TODO: Birthday, other demographics
        public override string ToString() => DisplayName ?? UserName;
    }
}
