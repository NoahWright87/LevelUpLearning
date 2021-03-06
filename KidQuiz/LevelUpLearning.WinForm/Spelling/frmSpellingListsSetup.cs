using LevelUpLearning.Core.Data;
using LevelUpLearning.Core.Extensions;
using LevelUpLearning.SpeechWindows;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class frmSpellingListsSetup : Form
    {
        SpellingWordList currentList = null;
        bool listIsNew = false;
        SpellingWord currentWord = null;
        bool wordIsNew = false;

        public frmSpellingListsSetup()
        {
            InitializeComponent();

            grpList.Enabled = false;
            grpWord.Enabled = false;

            InitSpellingLists();
        }

        private void InitSpellingLists()
        {
            lstSpellingLists.Items.Clear();
            foreach(var list in DataController.Root.Spelling.WordLists.Values)
            {
                lstSpellingLists.Items.Add(list);
            }

            grpLists.Show();

            lstSpellingLists_SelectedIndexChanged(null, null);

            currentList = null;
            InitSpellingList();
        }
        private void InitSpellingList()
        {
            if (currentList == null)
            {
                currentWord = null;
                InitWord();
                grpList.Hide();
                btnEditList.Hide();
                btnDeleteList.Hide();
            }
            else
            {
                btnEditList.Show();
                btnDeleteList.Show();
                grpList.Show();
                //grpList.Enabled = true;

                txtListName.Text = currentList.ListName;

                lstWords.Items.Clear();
                lstWords.Items.AddRange(currentList.Words.ToArray());
                UpdateListDifficulty();
            }
        }
        private void InitWord()
        {
            if (currentWord == null)
            {
                grpWord.Hide();

                btnEditWord.Hide();
                btnDeleteWord.Hide();
            }
            else
            {
                grpWord.Show();

                btnEditWord.Show();
                btnDeleteWord.Show();

                txtSpellingWord.Text = currentWord.Word;
                txtExampleSentence.Text = currentWord.Prompt;
            }
        }
        private void UpdateDifficulty()
        {
            lblDifficultyRating.Text = $"Difficulty:{Environment.NewLine}  {txtSpellingWord.Text.Difficulty()}";
        }
        private void UpdateListDifficulty()
        {
            lblListDifficulty.Text = $"Overall Difficulty:{Environment.NewLine}  {currentList.OverallDifficulty}";
        }

        private void lstSpellingLists_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstSpellingLists.SelectedIndex < 0)
            {
                currentList = null;
                btnExport.Hide();
            }
            else
            {
                currentList = lstSpellingLists.SelectedItem as SpellingWordList;
                btnExport.Show();
                listIsNew = false;
            }
            InitSpellingList();
        }

        private void lstWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstWords.SelectedIndex < 0)
            {
                currentWord = null;
            }
            else
            {
                currentWord = lstWords.SelectedItem as SpellingWord;
                wordIsNew = false;
            }
            InitWord();
        }

        private void btnAddList_Click(object sender, System.EventArgs e)
        {
            currentList = new SpellingWordList();
            listIsNew = true;

            btnEditList_Click(sender, e);
        }

        private void btnEditList_Click(object sender, System.EventArgs e)
        {
            grpLists.Enabled = false;
            grpList.Enabled = true;
            InitSpellingList();
        }

        private void btnDeleteList_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure?  You cannot undo this change!", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataController.Root.Spelling.WordLists.Remove(currentList.ListName);
                lstSpellingLists.Items.RemoveAt(lstSpellingLists.SelectedIndex);
                currentList = null;
                InitSpellingList();
            }
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            currentWord = new SpellingWord();
            wordIsNew = true;
            btnEditWord_Click(sender, e);
        }

        private void btnEditWord_Click(object sender, EventArgs e)
        {
            grpList.Enabled = false;
            grpWord.Enabled = true;
            InitWord();
        }

        private void btnDeleteWord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?") == DialogResult.Yes)
            {
                //  Remove from the list of words
                lstWords.Items.RemoveAt(lstWords.SelectedIndex);
                currentWord = null;
                InitWord();
            }
        }

        private void btnInsertWord_Click(object sender, EventArgs e)
        {
            txtExampleSentence.SelectedText = "%%";
        }

        private void btnHearTest_Click(object sender, EventArgs e)
        {
            if (txtExampleSentence.TextLength <= 0 || txtSpellingWord.TextLength <= 0)
            {
                MessageBox.Show("Must provide a word and an example sentence!!");
            }
            else
            {
                SpeechUtil.PromptToSpellWord(txtSpellingWord.Text, txtExampleSentence.Text);
            }
        }

        private void btnCancelWord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?") == DialogResult.Yes)
            {
                currentWord = null;
                InitWord();
                grpList.Enabled = true;
            }
        }

        private void btnSaveWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSpellingWord.Text))
            {
                MessageBox.Show("You have to enter a word");
            }
            else if (string.IsNullOrWhiteSpace(txtExampleSentence.Text))
            {
                MessageBox.Show("You have to enter an example sentence");
            }
            else
            {
                currentWord.Word = txtSpellingWord.Text;
                currentWord.Prompt = txtExampleSentence.Text;

                if (wordIsNew)
                {
                    lstWords.Items.Add(currentWord);
                    UpdateListDifficulty();
                }
                else
                {
                    lstWords.SelectedItem = currentWord;
                }

                grpWord.Enabled = false;
                grpList.Enabled = true;
                currentWord = null;
                InitWord();
            }
        }

        private void btnCancelList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                currentList = null;
                InitSpellingList();
                grpLists.Enabled = true;
            }
        }

        private void btnSaveList_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtListName.Text))
            {
                MessageBox.Show("You must give the list a name");
            }
            else if (lstWords.Items.Count <= 0)
            {
                MessageBox.Show("The list must have at least one word");
            }
            else
            {
                currentList.ListName = txtListName.Text;
                currentList.Words = new List<SpellingWord>();
                foreach (var item in lstWords.Items)
                {
                    if (item is SpellingWord word) currentList.Words.Add(word);
                }

                if (listIsNew)
                {
                    lstSpellingLists.Items.Add(currentList);
                }
                else
                {
                    lstWords.SelectedItem = currentWord;

                }

                grpList.Enabled = false;
                grpLists.Enabled = true;
                currentList = null;
                InitSpellingList();
            }
        }

        private void txtSpellingWord_TextChanged(object sender, EventArgs e)
        {
            UpdateDifficulty();
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You will lose all changes.  Are you sure?", "Cancel?",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            DataController.Root.Spelling.WordLists = new SerializableDictionary<string, SpellingWordList>();
            foreach (var item in lstSpellingLists.Items)
            {
                if (item is SpellingWordList list) DataController.Root.Spelling.WordLists.Add(list.ListName, list);
            }
            DataController.SaveRoot();
            Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                int numFails = 0;
                int numSuccesses = 0;
                foreach (var file in openFileDialog.FileNames)
                {
                    if (DataController.TryImportWordFile(file))
                    {
                        numSuccesses++;
                    }
                    else
                    {
                        numFails++;
                    }
                }

                string successMessage = $"{numSuccesses} file(s) imported.";
                string failMessage = numFails > 0 ? $"  {numFails} file(s) failed to import.  Make sure you picked a valid file and that the list doesn't already exist." : "";

                MessageBox.Show($"{successMessage}{failMessage}", "Complete", MessageBoxButtons.OK, 
                    numFails > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.None);

                if (numSuccesses > 0)
                {
                    InitSpellingLists();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = currentList.ListName;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!DataController.TryExportWordFile(currentList, saveFileDialog.FileName))
                {
                    MessageBox.Show("List export failed!  Make sure you have permission to write files in the folder you selected.");
                }
            }
        }
    }
}
