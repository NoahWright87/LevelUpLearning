namespace LevelUpLearning.WinForm
{
    partial class frmQuiz
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblWordsLeft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtInput.Location = new System.Drawing.Point(12, 129);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(895, 75);
            this.txtInput.TabIndex = 0;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDone.Location = new System.Drawing.Point(699, 231);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(208, 97);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "Submit";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRepeat.Location = new System.Drawing.Point(12, 26);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(208, 97);
            this.btnRepeat.TabIndex = 2;
            this.btnRepeat.Text = "Repeat";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnStats
            // 
            this.btnStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStats.Location = new System.Drawing.Point(12, 369);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(208, 97);
            this.btnStats.TabIndex = 3;
            this.btnStats.Text = "Stats";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHint.Location = new System.Drawing.Point(227, 45);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(159, 46);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Loop: X";
            // 
            // lblWordsLeft
            // 
            this.lblWordsLeft.AutoSize = true;
            this.lblWordsLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWordsLeft.Location = new System.Drawing.Point(227, 389);
            this.lblWordsLeft.Name = "lblWordsLeft";
            this.lblWordsLeft.Size = new System.Drawing.Size(298, 46);
            this.lblWordsLeft.TabIndex = 5;
            this.lblWordsLeft.Text = "Remaining: 100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(498, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "//TODO: Load from selected lists and difficulty, not what we\'re doing now.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(660, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "//TODO: Change loop/remaining text to something softer, easier to understand, lik" +
    "e a progress bar";
            // 
            // frmQuiz
            // 
            this.AcceptButton = this.btnDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 480);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblWordsLeft);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.txtInput);
            this.Name = "frmQuiz";
            this.Text = "Kid Quiz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblWordsLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

