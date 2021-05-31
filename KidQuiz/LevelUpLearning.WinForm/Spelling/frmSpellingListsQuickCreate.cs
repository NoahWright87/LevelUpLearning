using LevelUpLearning.Core.Data;
using LevelUpLearning.WinForms.Common;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Spelling
{

    public partial class frmSpellingListsQuickCreate : Form
    {
        public frmSpellingListsQuickCreate()
        {
            InitializeComponent();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $@"Enter a word on each line.  If you want, you can include example sentences after the word by adding a tab.  You can use %% as a placeholder for the spelling word, or type the word itself and the program will try and do it for you.  Example input:

dog
bat{"\t"}A %% sleeps in the cave.
cat{"\t"}The cat climbed a tree.
frog{"\t"}Don't let that %% in the house!

NOTE: Not all words have to have sentences.  You will have a chance to enter them and hear them spoken when you click the 'Create' button."
                , "Directions");
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWordList.Text))
            {
                MessageBox.Show("You have to enter something first.  If you want to cancel, just close this form.");
            }
            else
            {
                SpellingWordList newList = new SpellingWordList();
                //Keep asking until we get a name!
                while (string.IsNullOrWhiteSpace(newList.ListName))
                {
                    newList.ListName = TextDialog.Show(this, "Name of this list?");
                }

                foreach (var line in txtWordList.Lines.Select(x => x?.Trim() ?? "").Where(x => !string.IsNullOrEmpty(x)))
                {
                    var parts = line.Split('\t');
                    var word = parts[0];
                    var prompt = parts.Length > 1 ? parts[1] : null;

                    SpellingWord newWord = null;
                    //Keep asking until we get a word
                    while (newWord == null)
                    {
                        newWord = frmSpellingWordQuickCreate.QuickCreateWord(this, word, prompt);
                    }
                    newList.Words.Add(newWord);
                }

                DataController.Root.Spelling.WordLists.Add(newList.ListName, newList);

                MessageBox.Show($"'{newList.ListName}' list created!");
                Close();
            }
        }
    }
}
