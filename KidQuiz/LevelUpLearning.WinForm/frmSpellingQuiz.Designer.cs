namespace LevelUpLearning.WinForm
{
    partial class frmSpellingQuiz
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
            this.lblHint = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.barRemaining = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Courier New", 36F, System.Drawing.FontStyle.Bold);
            this.txtInput.Location = new System.Drawing.Point(12, 103);
            this.txtInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(895, 75);
            this.txtInput.TabIndex = 0;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.btnDone.Location = new System.Drawing.Point(699, 185);
            this.btnDone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(208, 78);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "Submit";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.btnRepeat.Location = new System.Drawing.Point(12, 21);
            this.btnRepeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(208, 78);
            this.btnRepeat.TabIndex = 2;
            this.btnRepeat.Text = "Repeat";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lblHint.Location = new System.Drawing.Point(227, 36);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(159, 46);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Loop: X";
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Black;
            this.lblProgress.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(361, 293);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(194, 46);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "100.00%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // barRemaining
            // 
            this.barRemaining.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barRemaining.Location = new System.Drawing.Point(0, 284);
            this.barRemaining.Name = "barRemaining";
            this.barRemaining.Size = new System.Drawing.Size(917, 64);
            this.barRemaining.TabIndex = 6;
            // 
            // frmSpellingQuiz
            // 
            this.AcceptButton = this.btnDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 348);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.barRemaining);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.txtInput);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmSpellingQuiz";
            this.Text = "Kid Quiz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar barRemaining;
    }
}

