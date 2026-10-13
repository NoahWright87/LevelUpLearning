
namespace LevelUpLearning.WinForm
{
    partial class frmSpellingQuizDifficultyAdventure
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
            this.barDifficulty = new System.Windows.Forms.TrackBar();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.grpDifficultyDetails = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblReps = new System.Windows.Forms.Label();
            this.barReps = new System.Windows.Forms.TrackBar();
            this.lblWordsPerDeal = new System.Windows.Forms.Label();
            this.barWordsPerDeal = new System.Windows.Forms.TrackBar();
            this.lblDeals = new System.Windows.Forms.Label();
            this.barNumDeals = new System.Windows.Forms.TrackBar();
            this.lblDifficultyRange = new System.Windows.Forms.Label();
            this.barDifficultyRange = new System.Windows.Forms.TrackBar();
            this.lblHintLetters = new System.Windows.Forms.Label();
            this.barHint = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.barDifficulty)).BeginInit();
            this.grpDifficultyDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barReps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barWordsPerDeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barNumDeals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDifficultyRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHint)).BeginInit();
            this.SuspendLayout();
            // 
            // barDifficulty
            // 
            this.barDifficulty.LargeChange = 1;
            this.barDifficulty.Location = new System.Drawing.Point(40, 30);
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
            this.lblDifficulty.Location = new System.Drawing.Point(37, 10);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(114, 17);
            this.lblDifficulty.TabIndex = 3;
            this.lblDifficulty.Text = "Overall Difficulty:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 509);
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
            this.btnFinish.Location = new System.Drawing.Point(355, 497);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(172, 46);
            this.btnFinish.TabIndex = 7;
            this.btnFinish.Text = "GO!";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(496, 30);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(31, 35);
            this.btnHelp.TabIndex = 16;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // grpDifficultyDetails
            // 
            this.grpDifficultyDetails.Controls.Add(this.lblTotal);
            this.grpDifficultyDetails.Controls.Add(this.lblReps);
            this.grpDifficultyDetails.Controls.Add(this.barReps);
            this.grpDifficultyDetails.Controls.Add(this.lblWordsPerDeal);
            this.grpDifficultyDetails.Controls.Add(this.barWordsPerDeal);
            this.grpDifficultyDetails.Controls.Add(this.lblDeals);
            this.grpDifficultyDetails.Controls.Add(this.barNumDeals);
            this.grpDifficultyDetails.Controls.Add(this.lblDifficultyRange);
            this.grpDifficultyDetails.Controls.Add(this.barDifficultyRange);
            this.grpDifficultyDetails.Controls.Add(this.lblHintLetters);
            this.grpDifficultyDetails.Controls.Add(this.barHint);
            this.grpDifficultyDetails.Location = new System.Drawing.Point(12, 92);
            this.grpDifficultyDetails.Name = "grpDifficultyDetails";
            this.grpDifficultyDetails.Size = new System.Drawing.Size(515, 399);
            this.grpDifficultyDetails.TabIndex = 19;
            this.grpDifficultyDetails.TabStop = false;
            this.grpDifficultyDetails.Text = "Difficulty Details";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(25, 367);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(141, 17);
            this.lblTotal.TabIndex = 25;
            this.lblTotal.Text = "Total Words in Quiz: ";
            // 
            // lblReps
            // 
            this.lblReps.AutoSize = true;
            this.lblReps.Location = new System.Drawing.Point(355, 202);
            this.lblReps.Name = "lblReps";
            this.lblReps.Size = new System.Drawing.Size(104, 17);
            this.lblReps.TabIndex = 24;
            this.lblReps.Text = "Reps per Word";
            // 
            // barReps
            // 
            this.barReps.LargeChange = 1;
            this.barReps.Location = new System.Drawing.Point(28, 202);
            this.barReps.Name = "barReps";
            this.barReps.Size = new System.Drawing.Size(321, 56);
            this.barReps.TabIndex = 23;
            this.barReps.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barReps.ValueChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // lblWordsPerDeal
            // 
            this.lblWordsPerDeal.AutoSize = true;
            this.lblWordsPerDeal.Location = new System.Drawing.Point(355, 146);
            this.lblWordsPerDeal.Name = "lblWordsPerDeal";
            this.lblWordsPerDeal.Size = new System.Drawing.Size(107, 17);
            this.lblWordsPerDeal.TabIndex = 22;
            this.lblWordsPerDeal.Text = "Words per Deal";
            // 
            // barWordsPerDeal
            // 
            this.barWordsPerDeal.LargeChange = 1;
            this.barWordsPerDeal.Location = new System.Drawing.Point(28, 146);
            this.barWordsPerDeal.Maximum = 20;
            this.barWordsPerDeal.Name = "barWordsPerDeal";
            this.barWordsPerDeal.Size = new System.Drawing.Size(321, 56);
            this.barWordsPerDeal.TabIndex = 21;
            this.barWordsPerDeal.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barWordsPerDeal.ValueChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // lblDeals
            // 
            this.lblDeals.AutoSize = true;
            this.lblDeals.Location = new System.Drawing.Point(355, 90);
            this.lblDeals.Name = "lblDeals";
            this.lblDeals.Size = new System.Drawing.Size(114, 17);
            this.lblDeals.TabIndex = 20;
            this.lblDeals.Text = "Number of Deals";
            // 
            // barNumDeals
            // 
            this.barNumDeals.LargeChange = 1;
            this.barNumDeals.Location = new System.Drawing.Point(28, 90);
            this.barNumDeals.Name = "barNumDeals";
            this.barNumDeals.Size = new System.Drawing.Size(321, 56);
            this.barNumDeals.TabIndex = 19;
            this.barNumDeals.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barNumDeals.ValueChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // lblDifficultyRange
            // 
            this.lblDifficultyRange.AutoSize = true;
            this.lblDifficultyRange.Location = new System.Drawing.Point(355, 34);
            this.lblDifficultyRange.Name = "lblDifficultyRange";
            this.lblDifficultyRange.Size = new System.Drawing.Size(107, 17);
            this.lblDifficultyRange.TabIndex = 18;
            this.lblDifficultyRange.Text = "Difficulty Range";
            // 
            // barDifficultyRange
            // 
            this.barDifficultyRange.LargeChange = 1;
            this.barDifficultyRange.Location = new System.Drawing.Point(28, 34);
            this.barDifficultyRange.Maximum = 30;
            this.barDifficultyRange.Name = "barDifficultyRange";
            this.barDifficultyRange.Size = new System.Drawing.Size(321, 56);
            this.barDifficultyRange.TabIndex = 17;
            this.barDifficultyRange.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barDifficultyRange.ValueChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // lblHintLetters
            // 
            this.lblHintLetters.AutoSize = true;
            this.lblHintLetters.Location = new System.Drawing.Point(355, 266);
            this.lblHintLetters.Name = "lblHintLetters";
            this.lblHintLetters.Size = new System.Drawing.Size(117, 17);
            this.lblHintLetters.TabIndex = 12;
            this.lblHintLetters.Text = "Mistakes per Hint";
            // 
            // barHint
            // 
            this.barHint.LargeChange = 1;
            this.barHint.Location = new System.Drawing.Point(28, 266);
            this.barHint.Minimum = 1;
            this.barHint.Name = "barHint";
            this.barHint.Size = new System.Drawing.Size(321, 56);
            this.barHint.TabIndex = 8;
            this.barHint.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barHint.Value = 1;
            this.barHint.ValueChanged += new System.EventHandler(this.ValuesChanged);
            // 
            // frmSpellingQuizDifficultyAdventure
            // 
            this.AcceptButton = this.btnFinish;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(549, 563);
            this.Controls.Add(this.grpDifficultyDetails);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblDifficulty);
            this.Controls.Add(this.barDifficulty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpellingQuizDifficultyAdventure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spelling Quiz Auto-Leveling Settings";
            ((System.ComponentModel.ISupportInitialize)(this.barDifficulty)).EndInit();
            this.grpDifficultyDetails.ResumeLayout(false);
            this.grpDifficultyDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barReps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barWordsPerDeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barNumDeals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDifficultyRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar barDifficulty;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.GroupBox grpDifficultyDetails;
        private System.Windows.Forms.Label lblDifficultyRange;
        private System.Windows.Forms.TrackBar barDifficultyRange;
        private System.Windows.Forms.Label lblHintLetters;
        private System.Windows.Forms.TrackBar barHint;
        private System.Windows.Forms.Label lblWordsPerDeal;
        private System.Windows.Forms.TrackBar barWordsPerDeal;
        private System.Windows.Forms.Label lblDeals;
        private System.Windows.Forms.TrackBar barNumDeals;
        private System.Windows.Forms.Label lblReps;
        private System.Windows.Forms.TrackBar barReps;
        private System.Windows.Forms.Label lblTotal;
    }
}