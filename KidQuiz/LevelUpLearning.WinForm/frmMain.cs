using LevelUpLearning.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    //TODO: This code looks incomplete - I need a way to create words from this page
    //  If I remember correctly, I didn't finish this and went with the lazy method of a tab-delimited file editted manually
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
            List<VocabWord> words = new List<VocabWord>();
            //for loop
            words[0] = new VocabWord(dgWords[0, 0].Value.ToString(), dgWords[1, 0].Value.ToString());
            //etc etc in loop

            //TODO: Make a constructor that takes a list of files
            new frmQuiz().ShowDialog();
        }
    }
}
