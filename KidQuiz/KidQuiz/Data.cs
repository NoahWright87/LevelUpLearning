using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidQuiz
{
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
