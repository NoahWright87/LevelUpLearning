using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KidQuiz
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            //Check for file of words
                //Load file if it exists
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            //Grab words from the screen
            //VocabWord[] vocabArray = new VocabWord[dgWords.RowCount];
            List<VocabWord> words = new List<VocabWord>();
            //for loop
            words[0] = new VocabWord(dgWords[0, 0].Value.ToString(), dgWords[1, 0].Value.ToString());
            //etc etc in loop

            //TODO: Make a constructor that takes a list of files
            new frmQuiz().ShowDialog();
        }
    }
}
