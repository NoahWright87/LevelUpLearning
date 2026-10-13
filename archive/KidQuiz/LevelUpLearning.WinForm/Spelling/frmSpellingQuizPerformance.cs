using LevelUpLearning.Core.Data;
using LevelUpLearning.Core.Extensions;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Spelling
{
    public partial class frmSpellingQuizPerformance : Form
    {
        public frmSpellingQuizPerformance() : this(null)
        {
            
        }
        public frmSpellingQuizPerformance(string listName)
        {
            InitializeComponent();

            cboSpellingLists.Items.Clear();
            foreach (var list in DataController.Root.Spelling.WordLists.Values)
            {
                cboSpellingLists.Items.Add(list);
            }

            if (listName != null)
            {
                cboSpellingLists.Text = listName;
            }
            else
            {
                cboSpellingLists.SelectedIndex = -1;
            }

            lblUser.Text = $"{DataController.State.CurrentUser.DisplayName}'s spelling stats";
        }

        private void cboSpellingLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSpellingLists.SelectedIndex < 0)
            {
                grpListStats.Hide();
                grpWordStats.Hide();
            }
            else
            {
                grpListStats.Show();
                grpWordStats.Show();


                if (DataController.Root.Spelling.UserPerformances.TryGetValue(DataController.State.CurrentUser.UserName, out var perf))
                {
                    var list = cboSpellingLists.SelectedItem as SpellingWordList;
                    if (perf.ListPerformances.TryGetValue(list.ListName, out var listPerf))
                    {
                        UpdateListStats(listPerf);
                    }
                    else
                    {
                        UpdateListStats(null);
                    }

                    dataWords.Rows.Clear();
                    foreach (var sw in list.Words.OrderBy(w => w.Word))
                    {
                        if (perf.WordPerformances.TryGetValue(sw.Word, out var wordPerf))
                        {
                            AddWordStats(sw.Word, wordPerf);
                        }
                        else
                        {
                            AddWordStats(sw.Word, null);
                        }
                    }
                }
                else
                {
                    UpdateListStats(null);
                }
            }
        }
        private void UpdateListStats(SpellingWordListPerformance perf)
        {
            if (perf != null)
            {
                lblAttempts.Text = perf.TotalAttempts.ToString();
                lblLastDate.Text = perf.LastAttemptDate.ToShortDateString();
                lblBestScore.Text = $"{perf.BestScore:0.0}";
                lblBestStars.Text = perf.BestPercentage.ToStars();
                lblLastScore.Text = $"{perf.LastScore:0.0}";
                lblLastStars.Text = perf.LastPercentage.ToStars();
            }
            else
            {
                lblAttempts.Text = "N/A";
                lblLastDate.Text = "N/A";
                lblBestScore.Text = "N/A";
                lblBestStars.Text = "";
                lblLastScore.Text = "N/A";
                lblLastStars.Text = "";
            }
        }
        private void AddWordStats(string word, SpellingWordPerformance perf)
        {
            if (perf != null)
            {
                var lastResult = $"{perf.LastCorrect} / {perf.LastAttempts} ({perf.CorrectPercentage:0.00%})";
                var allResults = $"{perf.TotalCorrect} / {perf.TotalAttempts} ({perf.TotalCorrectPercentage:0.00%})";

                dataWords.Rows.Add(word, lastResult, allResults);
            }
            else
            {
                dataWords.Rows.Add(word, "N/A", "N/A");
            }
        }
    }
}
