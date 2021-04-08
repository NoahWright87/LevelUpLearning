using System.Collections.Generic;

namespace LevelUpLearning.Core.Data
{
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
        public List<UserData> Users;

        public Dictionary<string, VocabWordList> WordLists;
    }

    public class UserData
    {
        public string UserName;

        public Dictionary<string, VocabWordListPerformance> VocabPerformances;
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
