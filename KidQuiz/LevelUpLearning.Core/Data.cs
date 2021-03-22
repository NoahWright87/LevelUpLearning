using System.Collections.Generic;

namespace LevelUpLearning.Core
{
    //TODO: Looks like this was never implemented - either flesh it out or remove it

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
