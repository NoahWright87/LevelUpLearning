using System.Collections.Generic;

namespace LevelUpLearning.Core
{
    //TODO: Looks like this was never implemented.  Flesh it out into something useful
    //  Looks like it could be used with some work.  Should probably include a name for a list of words
    //  That would let us have prebuilt vocab words, or store each week's spelling test in a separate list
    //  Would also be nice for user to be able to pick multiple tests.  e.g.: each week you get 10 new words,
    //  and at end of month you're tested on last 4 week's, so you select all 4 of those to be tested on

    //This will store all data for the quizzer.

    public class Data
    {
        List<VocabWord> ArchivedWords;
        List<VocabWord> CurrentWords;


        public void ArchiveCurrentWords()
        {
            //TODO: Move currentwords into archivedwords
        }

        public void AddWord(VocabWord newWord)
        {
            //TODO: Add to current words
        }

        public void RecordPerformance()
        {
            //TODO: Record performance for this quizzing session
        }


    }
}
