using LevelUpLearning.Core;
using LevelUpLearning.Core.Data;
using LevelUpLearning.SpeechWindows;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Spelling
{
    public partial class frmSpellingWordQuickCreate : Form
    {
        private frmSpellingWordQuickCreate(string word, string prompt)
        {
            InitializeComponent();

            txtWord.Text = word.Trim();
            //If the user didn't use a placeholder, do it for them
            if (prompt != null && !prompt.Contains(Constants.SPELLING_WORD_PLACEHOLDER))
            {
                prompt = prompt.Replace(word, Constants.SPELLING_WORD_PLACEHOLDER);
            }
            txtPrompt.Text = prompt?.Trim();

            SpeakPrompt();
        }

        public static SpellingWord QuickCreateWord(IWin32Window owner, string word, string prompt)
        {

            var popup = new frmSpellingWordQuickCreate(word, prompt);
            if (popup.ShowDialog(owner) == DialogResult.OK)
            {
                return new SpellingWord()
                {
                    Word = popup.txtWord.Text.Trim(),
                    Prompt = popup.txtPrompt.Text.Trim()
                };
            }
            else
            {
                return null;
            }
        }

        private bool SomethingIsMissing() => string.IsNullOrWhiteSpace(txtWord.Text) || string.IsNullOrWhiteSpace(txtPrompt.Text);

        private void SpeakPrompt()
        {
            if (!SomethingIsMissing())
            {
                SpeechUtil.PromptToSpellWord(txtWord.Text, txtPrompt.Text);
            }
        }

        private void Finish()
        {
            if (SomethingIsMissing())
            {
                MessageBox.Show("You must provide both a word and a prompt!", "Whoops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnListen_Click(object sender, EventArgs e) => SpeakPrompt();
        private void btnDone_Click(object sender, EventArgs e) => Finish();
    }
}
