using Microsoft.VisualStudio.Services.Common;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LevelUpLearning.Core.Data
{
    /// <summary>
    /// Singleton to hold data for the program
    /// </summary>
    public class datafilesingletonrenamelater
    {
        //TODO: Since this class is tasked with specifically loading the data from an XML file, should it live elsewhere?
        //This project is for core classes used across all projects, this would be something that's Windows-specific

        /// <summary>
        /// Backing variable for Instance.  DO NOT USE DIRECTLY
        /// </summary>
        private static datafilesingletonrenamelater _instance;
        /// <summary>
        /// Singleton to store this class's data
        /// </summary>
        private static datafilesingletonrenamelater Instance
        {
            get
            {
                if (_instance == null) _instance = new datafilesingletonrenamelater();
                return _instance;
            }
        }

        private static readonly string DATA_FILENAME = "lul.dat";
        //TODO: Create a DTO class that has XML/JSON decorators that will serialize the data
        //  Can use the SerializableDictionary class I messed around with below

        private datafilesingletonrenamelater()
        {
            if (!File.Exists(DATA_FILENAME))
            {
                //TODO: Create default data
            }

            //TODO: Load data from filename
        }

        public static void test_thing()
        {
            SerializableDictionary<string, int> dataToSave = new SerializableDictionary<string, int>()
            {
                {"hey", 3 },
                {"george", 6 },
                {"yo", 2 },
                {"a", 1 },
                {"shapoopy", 8 }
            };

            XmlSerializer xmlSerializer = new XmlSerializer(dataToSave.GetType());
            StringWriter writer = new StringWriter();
            xmlSerializer.Serialize(writer, dataToSave);
            var result = writer.ToString();

            System.Console.WriteLine(result);
        }


        
    }





    //TODO: Looks like this was never implemented.  Flesh it out into something useful
    //  Looks like it could be used with some work.  Should probably include a name for a list of words
    //  That would let us have prebuilt vocab words, or store each week's spelling test in a separate list
    //  Would also be nice for user to be able to pick multiple tests.  e.g.: each week you get 10 new words,
    //  and at end of month you're tested on last 4 week's, so you select all 4 of those to be tested on

    //This will store all data for the quizzer.

    //TODO: Not sure about this name
    //TODO: Put serialization decorators on these classes
    //  Can Dictionary be serialized??
    //TODO: Split these files up once I'm sure I've got all the data I need
    public class DataFile
    {
        //TODO: Remembered user (if that box is checked on login)

        public List<UserData> Users;

        public SerializableDictionary<string, VocabWordList> WordLists;
    }

    public class UserData
    {
        public string UserName;

        public SerializableDictionary<string, VocabWordListPerformance> VocabPerformances;
    }

    public class VocabWordList
    {
        public string ListName;

        public bool IsPublic;

        public List<VocabWord> Words;
    }

    public class VocabWordListPerformance
    {
        //TODO: Track long term performance here
    }
}
