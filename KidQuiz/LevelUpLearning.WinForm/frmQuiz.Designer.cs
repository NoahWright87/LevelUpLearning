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
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(9, 84);
            this.txtInput.Margin = new System.Windows.Forms.Padding(2);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(672, 62);
            this.txtInput.TabIndex = 0;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(524, 150);
            this.btnDone.Margin = new System.Windows.Forms.Padding(2);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(156, 63);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "Submit";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepeat.Location = new System.Drawing.Point(9, 17);
            this.btnRepeat.Margin = new System.Windows.Forms.Padding(2);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(156, 63);
            this.btnRepeat.TabIndex = 2;
            this.btnRepeat.Text = "Repeat";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnStats
            // 
            this.btnStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStats.Location = new System.Drawing.Point(9, 240);
            this.btnStats.Margin = new System.Windows.Forms.Padding(2);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(156, 63);
            this.btnStats.TabIndex = 3;
            this.btnStats.Text = "Stats";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lblHint.Location = new System.Drawing.Point(170, 29);
            this.lblHint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(128, 37);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Loop: X";
            // 
            // lblWordsLeft
            // 
            this.lblWordsLeft.AutoSize = true;
            this.lblWordsLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lblWordsLeft.Location = new System.Drawing.Point(170, 253);
            this.lblWordsLeft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWordsLeft.Name = "lblWordsLeft";
            this.lblWordsLeft.Size = new System.Drawing.Size(239, 37);
            this.lblWordsLeft.TabIndex = 5;
            this.lblWordsLeft.Text = "Remaining: 100";
            // 
            // frmQuiz
            // 
            this.AcceptButton = this.btnDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 312);
            this.Controls.Add(this.lblWordsLeft);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.txtInput);
            this.Margin = new System.Windows.Forms.Padding(2);
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
    }
}

