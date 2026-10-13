using LevelUpLearning.Core.Data;
using System;
using System.Collections.Generic;
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

NOTE: Not all words have to have sentences.  You will have a chance to enter them and hear them spoken when you click the 'Create' button.
If you have a LOT of words, they can automatically be split into multiple lists using the 'Words / List' option below."
                , "Directions");
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWordList.Text))
            {
                MessageBox.Show("You have to enter something first.  If you want to cancel, just close this form.");
            }
            else if (string.IsNullOrEmpty(txtListName.Text))
            {
                MessageBox.Show("You must enter a list name first.");
            }
            else
            {
                //SpellingWordList newList = new SpellingWordList();
                ////Keep asking until we get a name!
                //while (string.IsNullOrWhiteSpace(newList.ListName))
                //{
                //    newList.ListName = TextDialog.Show(this, "Name of this list?");
                //}

                List<SpellingWord> newList = new List<SpellingWord>();

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
                    newList.Add(newWord);
                }

                if (NumOfLists > 1)
                {
                    for(int i = 0; i < NumOfLists; i++)
                    {
                        var subListName = $"{ListName} List {i + 1}";
                        var words = newList.Skip(i * WordsPerList).Take(WordsPerList).ToList();

                        DataController.Root.Spelling.WordLists.Add(subListName, new SpellingWordList()
                        {
                            ListName = subListName,
                            Words = words
                        });
                    }

                    MessageBox.Show($"Created {NumOfLists} '{ListName}' lists");
                }
                else
                {
                    DataController.Root.Spelling.WordLists.Add(ListName, new SpellingWordList()
                    {
                        ListName = ListName,
                        Words = newList
                    });
                    MessageBox.Show($"'{ListName}' list created!");
                }

                Close();
            }
        }

        string ListName => txtListName.Text.Trim();
        int NumOfLists => NumOfWords / WordsPerList + (NumOfWords % WordsPerList > 0 ? 1 : 0);
        int NumOfWords => txtWordList.Lines.Count(x => x.Length > 0);
        int WordsPerList => (int)numListSize.Value;
        private void UpdateListNames(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWordList.Text) || string.IsNullOrWhiteSpace(ListName))
            {
                lblListPreview.Text = string.Empty;
                return;
            }

            if (NumOfLists > 1)
            {
                lblListPreview.Text = $"{NumOfLists} lists will be created named {ListName} List 1, {ListName} List 2, etc";
            }
            else
            {
                lblListPreview.Text = $"1 list named {ListName} will be created.";
            }
        }
    }
}
