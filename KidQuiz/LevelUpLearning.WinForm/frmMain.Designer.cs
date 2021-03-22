namespace LevelUpLearning.WinForm
{
    partial class frmMain
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
            this.dgWords = new System.Windows.Forms.DataGridView();
            this.Word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExampleSentence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStartQuiz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgWords)).BeginInit();
            this.SuspendLayout();
            // 
            // dgWords
            // 
            this.dgWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Word,
            this.ExampleSentence});
            this.dgWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWords.Location = new System.Drawing.Point(0, 0);
            this.dgWords.Name = "dgWords";
            this.dgWords.Size = new System.Drawing.Size(758, 587);
            this.dgWords.TabIndex = 3;
            // 
            // Word
            // 
            this.Word.HeaderText = "Word";
            this.Word.Name = "Word";
            this.Word.Width = 150;
            // 
            // ExampleSentence
            // 
            this.ExampleSentence.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExampleSentence.HeaderText = "Example Sentence";
            this.ExampleSentence.Name = "ExampleSentence";
            // 
            // btnStartQuiz
            // 
            this.btnStartQuiz.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStartQuiz.Location = new System.Drawing.Point(0, 564);
            this.btnStartQuiz.Name = "btnStartQuiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(758, 23);
            this.btnStartQuiz.TabIndex = 4;
            this.btnStartQuiz.Text = "Begin Quiz";
            this.btnStartQuiz.UseVisualStyleBackColor = true;
            this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 587);
            this.Controls.Add(this.btnStartQuiz);
            this.Controls.Add(this.dgWords);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmMain";
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.dgWords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgWords;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExampleSentence;
        private System.Windows.Forms.Button btnStartQuiz;

    }
}