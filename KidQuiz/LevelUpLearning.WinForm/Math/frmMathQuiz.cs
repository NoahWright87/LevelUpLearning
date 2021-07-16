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
        int problemsRemaining;
        
        double ProgressPercentage => 1 - (double)problemsRemaining / settings.ProblemsToDo;
        string CurrentProblem;
        double CurrentProblemDifficulty;

        double UserMathLevel
        {
            get
            {
                return DataController.State.CurrentUser.Character.LevelMath;
            }
            set
            {
                if (value <= 0) DataController.State.CurrentUser.Character.LevelMath = 0;
                DataController.State.CurrentUser.Character.LevelMath = value;
            }
        }

        double UserMathLevelInitial;

        public frmMathQuiz()
        {
            InitializeComponent();

            //TODO: Show math settings form, change things based on those settings
            settings = new MathQuizSettings();

            problemsRemaining = settings.ProblemsToDo;

            UserMathLevelInitial = UserMathLevel;

            GenerateProblem();
        }

        private void GenerateProblem()
        {
            var problem = MathUtils.GetMathProblemFor(UserMathLevel + Utils.Random.NextDouble()*settings.LevelRange);
            CurrentProblem = problem.Item1;
            CurrentProblemDifficulty = problem.Item2;

            lblPrompt.Text = CurrentProblem;

            lblProgress.Text = $"{ProgressPercentage:0.00%}";
            barRemaining.Value = (int)(ProgressPercentage * barRemaining.Maximum);

            this.Text = $"LUL Math - {UserMathLevelInitial:0.0} ==> {UserMathLevel:0.0}";

            txtInput.Clear();
            txtInput.Focus();
        }
        private void SubmitAnswer(double userInput)
        {
            var answer = MathUtils.SolveMathProblem(CurrentProblem);
            answer = System.Math.Round(answer, 3);
            userInput = System.Math.Round(userInput, 3);

            bool isCorrect = (answer == userInput);
            if (isCorrect)
            {
                MessageBox.Show("Correct!");
            }
            else
            {
                MessageBox.Show($"Incorrect.  {CurrentProblem} = {answer} ( you said {userInput}");
            }

            problemsRemaining--;
            UserMathLevel += Utils.LevelChange(UserMathLevel, CurrentProblemDifficulty, isCorrect);

            if (problemsRemaining <= 0)
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
            MessageBox.Show($"All done!{Environment.NewLine}Level changed from {UserMathLevelInitial} to {UserMathLevel}");
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
