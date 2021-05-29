
namespace LevelUpLearning.WinForm
{
    partial class frmSpellingQuizDifficulty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstWordLists = new System.Windows.Forms.CheckedListBox();
            this.lblWordLists = new System.Windows.Forms.Label();
            this.barDifficulty = new System.Windows.Forms.TrackBar();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.grpDifficultyDetails = new System.Windows.Forms.GroupBox();
            this.lblStreakPenalty = new System.Windows.Forms.Label();
            this.lblTargetStreak = new System.Windows.Forms.Label();
            this.lblHintChange = new System.Windows.Forms.Label();
            this.lblHintLetters = new System.Windows.Forms.Label();
            this.barStreakPenalty = new System.Windows.Forms.TrackBar();
            this.barTargetStreak = new System.Windows.Forms.TrackBar();
            this.barHintChange = new System.Windows.Forms.TrackBar();
            this.barHintLetters = new System.Windows.Forms.TrackBar();
            this.chkShowBlanks = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblScoreMultiplier = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barDifficulty)).BeginInit();
            this.grpDifficultyDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barStreakPenalty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barTargetStreak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHintChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHintLetters)).BeginInit();
            this.SuspendLayout();
            // 
            // lstWordLists
            // 
            this.lstWordLists.FormattingEnabled = true;
            this.lstWordLists.Location = new System.Drawing.Point(12, 33);
            this.lstWordLists.Name = "lstWordLists";
            this.lstWordLists.Size = new System.Drawing.Size(353, 361);
            this.lstWordLists.TabIndex = 0;
            // 
            // lblWordLists
            // 
            this.lblWordLists.AutoSize = true;
            this.lblWordLists.Location = new System.Drawing.Point(13, 13);
            this.lblWordLists.Name = "lblWordLists";
            this.lblWordLists.Size = new System.Drawing.Size(146, 17);
            this.lblWordLists.TabIndex = 1;
            this.lblWordLists.Text = "Word Lists to Practice";
            // 
            // barDifficulty
            // 
            this.barDifficulty.LargeChange = 1;
            this.barDifficulty.Location = new System.Drawing.Point(397, 33);
            this.barDifficulty.Maximum = 4;
            this.barDifficulty.Name = "barDifficulty";
            this.barDifficulty.Size = new System.Drawing.Size(321, 56);
            this.barDifficulty.TabIndex = 2;
            this.barDifficulty.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barDifficulty.Scroll += new System.EventHandler(this.barDifficulty_Scroll);
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Location = new System.Drawing.Point(394, 13);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(65, 17);
            this.lblDifficulty.TabIndex = 3;
            this.lblDifficulty.Text = "Difficulty:";
            // 
            // grpDifficultyDetails
            // 
            this.grpDifficultyDetails.Controls.Add(this.lblScoreMultiplier);
            this.grpDifficultyDetails.Controls.Add(this.lblStreakPenalty);
            this.grpDifficultyDetails.Controls.Add(this.lblTargetStreak);
            this.grpDifficultyDetails.Controls.Add(this.lblHintChange);
            this.grpDifficultyDetails.Controls.Add(this.lblHintLetters);
            this.grpDifficultyDetails.Controls.Add(this.barStreakPenalty);
            this.grpDifficultyDetails.Controls.Add(this.barTargetStreak);
            this.grpDifficultyDetails.Controls.Add(this.barHintChange);
            this.grpDifficultyDetails.Controls.Add(this.barHintLetters);
            this.grpDifficultyDetails.Controls.Add(this.chkShowBlanks);
            this.grpDifficultyDetails.Location = new System.Drawing.Point(397, 69);
            this.grpDifficultyDetails.Name = "grpDifficultyDetails";
            this.grpDifficultyDetails.Size = new System.Drawing.Size(487, 325);
            this.grpDifficultyDetails.TabIndex = 5;
            this.grpDifficultyDetails.TabStop = false;
            this.grpDifficultyDetails.Text = "Difficulty Details";
            // 
            // lblStreakPenalty
            // 
            this.lblStreakPenalty.AutoSize = true;
            this.lblStreakPenalty.Location = new System.Drawing.Point(344, 249);
            this.lblStreakPenalty.Name = "lblStreakPenalty";
            this.lblStreakPenalty.Size = new System.Drawing.Size(100, 17);
            this.lblStreakPenalty.TabIndex = 15;
            this.lblStreakPenalty.Text = "Streak Penalty";
            // 
            // lblTargetStreak
            // 
            this.lblTargetStreak.AutoSize = true;
            this.lblTargetStreak.Location = new System.Drawing.Point(344, 187);
            this.lblTargetStreak.Name = "lblTargetStreak";
            this.lblTargetStreak.Size = new System.Drawing.Size(95, 17);
            this.lblTargetStreak.TabIndex = 14;
            this.lblTargetStreak.Text = "Target Streak";
            // 
            // lblHintChange
            // 
            this.lblHintChange.AutoSize = true;
            this.lblHintChange.Location = new System.Drawing.Point(344, 125);
            this.lblHintChange.Name = "lblHintChange";
            this.lblHintChange.Size = new System.Drawing.Size(86, 17);
            this.lblHintChange.TabIndex = 13;
            this.lblHintChange.Text = "Hint Change";
            // 
            // lblHintLetters
            // 
            this.lblHintLetters.AutoSize = true;
            this.lblHintLetters.Location = new System.Drawing.Point(344, 63);
            this.lblHintLetters.Name = "lblHintLetters";
            this.lblHintLetters.Size = new System.Drawing.Size(81, 17);
            this.lblHintLetters.TabIndex = 12;
            this.lblHintLetters.Text = "Hint Letters";
            // 
            // barStreakPenalty
            // 
            this.barStreakPenalty.LargeChange = 1;
            this.barStreakPenalty.Location = new System.Drawing.Point(17, 249);
            this.barStreakPenalty.Maximum = 5;
            this.barStreakPenalty.Name = "barStreakPenalty";
            this.barStreakPenalty.Size = new System.Drawing.Size(321, 56);
            this.barStreakPenalty.TabIndex = 11;
            this.barStreakPenalty.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barStreakPenalty.ValueChanged += new System.EventHandler(this.CustomDifficultyChanged);
            // 
            // barTargetStreak
            // 
            this.barTargetStreak.LargeChange = 1;
            this.barTargetStreak.Location = new System.Drawing.Point(17, 187);
            this.barTargetStreak.Maximum = 5;
            this.barTargetStreak.Minimum = 1;
            this.barTargetStreak.Name = "barTargetStreak";
            this.barTargetStreak.Size = new System.Drawing.Size(321, 56);
            this.barTargetStreak.TabIndex = 10;
            this.barTargetStreak.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barTargetStreak.Value = 1;
            this.barTargetStreak.ValueChanged += new System.EventHandler(this.CustomDifficultyChanged);
            // 
            // barHintChange
            // 
            this.barHintChange.LargeChange = 1;
            this.barHintChange.Location = new System.Drawing.Point(17, 125);
            this.barHintChange.Maximum = 5;
            this.barHintChange.Name = "barHintChange";
            this.barHintChange.Size = new System.Drawing.Size(321, 56);
            this.barHintChange.TabIndex = 9;
            this.barHintChange.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barHintChange.ValueChanged += new System.EventHandler(this.CustomDifficultyChanged);
            // 
            // barHintLetters
            // 
            this.barHintLetters.LargeChange = 1;
            this.barHintLetters.Location = new System.Drawing.Point(17, 63);
            this.barHintLetters.Maximum = 5;
            this.barHintLetters.Name = "barHintLetters";
            this.barHintLetters.Size = new System.Drawing.Size(321, 56);
            this.barHintLetters.TabIndex = 8;
            this.barHintLetters.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barHintLetters.ValueChanged += new System.EventHandler(this.CustomDifficultyChanged);
            // 
            // chkShowBlanks
            // 
            this.chkShowBlanks.AutoSize = true;
            this.chkShowBlanks.Location = new System.Drawing.Point(17, 36);
            this.chkShowBlanks.Name = "chkShowBlanks";
            this.chkShowBlanks.Size = new System.Drawing.Size(110, 21);
            this.chkShowBlanks.TabIndex = 0;
            this.chkShowBlanks.Text = "Show Blanks";
            this.chkShowBlanks.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(16, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFinish.Location = new System.Drawing.Point(712, 400);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(172, 46);
            this.btnFinish.TabIndex = 7;
            this.btnFinish.Text = "GO!";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(847, 42);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(31, 35);
            this.btnHelp.TabIndex = 16;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // lblScoreMultiplier
            // 
            this.lblScoreMultiplier.AutoSize = true;
            this.lblScoreMultiplier.Location = new System.Drawing.Point(17, 302);
            this.lblScoreMultiplier.Name = "lblScoreMultiplier";
            this.lblScoreMultiplier.Size = new System.Drawing.Size(109, 17);
            this.lblScoreMultiplier.TabIndex = 16;
            this.lblScoreMultiplier.Text = "Score Multiplier:";
            // 
            // frmSpellingQuizDifficulty
            // 
            this.AcceptButton = this.btnFinish;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(894, 467);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpDifficultyDetails);
            this.Controls.Add(this.lblDifficulty);
            this.Controls.Add(this.barDifficulty);
            this.Controls.Add(this.lblWordLists);
            this.Controls.Add(this.lstWordLists);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpellingQuizDifficulty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spelling Quiz Settings";
            ((System.ComponentModel.ISupportInitialize)(this.barDifficulty)).EndInit();
            this.grpDifficultyDetails.ResumeLayout(false);
            this.grpDifficultyDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barStreakPenalty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barTargetStreak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHintChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHintLetters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstWordLists;
        private System.Windows.Forms.Label lblWordLists;
        private System.Windows.Forms.TrackBar barDifficulty;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.GroupBox grpDifficultyDetails;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.CheckBox chkShowBlanks;
        private System.Windows.Forms.Label lblStreakPenalty;
        private System.Windows.Forms.Label lblTargetStreak;
        private System.Windows.Forms.Label lblHintChange;
        private System.Windows.Forms.Label lblHintLetters;
        private System.Windows.Forms.TrackBar barStreakPenalty;
        private System.Windows.Forms.TrackBar barTargetStreak;
        private System.Windows.Forms.TrackBar barHintChange;
        private System.Windows.Forms.TrackBar barHintLetters;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label lblScoreMultiplier;
    }
}