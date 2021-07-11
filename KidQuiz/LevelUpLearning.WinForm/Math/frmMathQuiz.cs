using LevelUpLearning.Core;
using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Math
{
    public partial class frmMathQuiz : Form
    {
        //TODO: Put this with the quiz settings
        MathQuizSettings settings;
        int problemsToDo = 20;
        int maxNum = 9;
        

        int numCorrect = 0;
        int numSubmitted = 0;

        double leftNumber = 0;
        double rightNumber = 0;
        //TODO: Current operator

        //TODO: Change based on operator
        double CorrectAnswer => leftNumber + rightNumber;
        string ProblemText => $"{leftNumber} + {rightNumber}";

        double ProgressPercentage => (double)numSubmitted / problemsToDo;

        public frmMathQuiz()
        {
            InitializeComponent();

            //TODO: Show math settings, change things based on those settings
            GenerateProblem();


        }

        private void GenerateProblem()
        {
            //TODO: Vary based on settings
            leftNumber = Utils.Random.Next(maxNum);
            rightNumber = Utils.Random.Next(maxNum);

            lblPrompt.Text = ProblemText;
            lblProgress.Text = $"{ProgressPercentage:0.00%}";
            barRemaining.Value = (int)(ProgressPercentage * barRemaining.Maximum);

            txtInput.Clear();
            txtInput.Focus();
        }
        private void SubmitAnswer(double answer)
        {
            //TODO: Account for precision - may need to round this once we get into decimals
            if (answer == CorrectAnswer)
            {
                MessageBox.Show("Correct!");
                numCorrect++;
            }
            else
            {
                MessageBox.Show($"Incorrect.  {ProblemText} = {CorrectAnswer} ( you said {answer}");
            }
            numSubmitted++;

            if (numSubmitted >= problemsToDo)
            {
                Finish();
            }
            else
            {
                GenerateProblem();
            }
        }

        private void Finish()
        {
            MessageBox.Show($"All done!{Environment.NewLine}You got {numCorrect} / {numSubmitted} correct!");
            Close();
            //TODO: Show a better message, save stats for later
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtInput.TextLength > 0 && double.TryParse(txtInput.Text, out double answer))
            {
                SubmitAnswer(answer);
            }
            else
            {
                //TODO: Should I alert the user or no?
            }
        }
    }
}
