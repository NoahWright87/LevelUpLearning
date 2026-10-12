
namespace LevelUpLearning.WinForms.Spelling
{
    partial class frmSpellingQuizPerformance
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblUser = new System.Windows.Forms.Label();
            this.cboSpellingLists = new System.Windows.Forms.ComboBox();
            this.lblSpellingList = new System.Windows.Forms.Label();
            this.grpListStats = new System.Windows.Forms.GroupBox();
            this.tableListPerformance = new System.Windows.Forms.TableLayoutPanel();
            this.lblBestStars = new System.Windows.Forms.Label();
            this.lblLastStars = new System.Windows.Forms.Label();
            this.lblBestLabel = new System.Windows.Forms.Label();
            this.lblBestScore = new System.Windows.Forms.Label();
            this.lblLastDateLabel = new System.Windows.Forms.Label();
            this.lblAttemptsLabel = new System.Windows.Forms.Label();
            this.lblLastScore = new System.Windows.Forms.Label();
            this.lblAttempts = new System.Windows.Forms.Label();
            this.lblLastDate = new System.Windows.Forms.Label();
            this.lblLastLabel = new System.Windows.Forms.Label();
            this.grpWordStats = new System.Windows.Forms.GroupBox();
            this.dataWords = new System.Windows.Forms.DataGridView();
            this.colWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastPerf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPerf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpListStats.SuspendLayout();
            this.tableListPerformance.SuspendLayout();
            this.grpWordStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataWords)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.Location = new System.Drawing.Point(381, 15);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(598, 24);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "X\'s Spelling Stats";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSpellingLists
            // 
            this.cboSpellingLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpellingLists.FormattingEnabled = true;
            this.cboSpellingLists.Location = new System.Drawing.Point(106, 12);
            this.cboSpellingLists.Name = "cboSpellingLists";
            this.cboSpellingLists.Size = new System.Drawing.Size(255, 24);
            this.cboSpellingLists.TabIndex = 1;
            this.cboSpellingLists.SelectedIndexChanged += new System.EventHandler(this.cboSpellingLists_SelectedIndexChanged);
            // 
            // lblSpellingList
            // 
            this.lblSpellingList.AutoSize = true;
            this.lblSpellingList.Location = new System.Drawing.Point(12, 15);
            this.lblSpellingList.Name = "lblSpellingList";
            this.lblSpellingList.Size = new System.Drawing.Size(88, 17);
            this.lblSpellingList.TabIndex = 2;
            this.lblSpellingList.Text = "Spelling List:";
            // 
            // grpListStats
            // 
            this.grpListStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpListStats.Controls.Add(this.tableListPerformance);
            this.grpListStats.Location = new System.Drawing.Point(15, 42);
            this.grpListStats.Name = "grpListStats";
            this.grpListStats.Size = new System.Drawing.Size(346, 297);
            this.grpListStats.TabIndex = 3;
            this.grpListStats.TabStop = false;
            this.grpListStats.Text = "List Performance";
            // 
            // tableListPerformance
            // 
            this.tableListPerformance.ColumnCount = 2;
            this.tableListPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableListPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableListPerformance.Controls.Add(this.lblBestStars, 1, 4);
            this.tableListPerformance.Controls.Add(this.lblLastStars, 0, 4);
            this.tableListPerformance.Controls.Add(this.lblBestLabel, 1, 2);
            this.tableListPerformance.Controls.Add(this.lblBestScore, 1, 3);
            this.tableListPerformance.Controls.Add(this.lblLastDateLabel, 0, 1);
            this.tableListPerformance.Controls.Add(this.lblAttemptsLabel, 0, 0);
            this.tableListPerformance.Controls.Add(this.lblLastScore, 0, 3);
            this.tableListPerformance.Controls.Add(this.lblAttempts, 1, 0);
            this.tableListPerformance.Controls.Add(this.lblLastDate, 1, 1);
            this.tableListPerformance.Controls.Add(this.lblLastLabel, 0, 2);
            this.tableListPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableListPerformance.Location = new System.Drawing.Point(3, 18);
            this.tableListPerformance.Name = "tableListPerformance";
            this.tableListPerformance.RowCount = 5;
            this.tableListPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableListPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableListPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableListPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableListPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableListPerformance.Size = new System.Drawing.Size(340, 276);
            this.tableListPerformance.TabIndex = 0;
            // 
            // lblBestStars
            // 
            this.lblBestStars.AutoSize = true;
            this.lblBestStars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBestStars.Font = new System.Drawing.Font("Lucida Console", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBestStars.Location = new System.Drawing.Point(173, 219);
            this.lblBestStars.Name = "lblBestStars";
            this.lblBestStars.Size = new System.Drawing.Size(164, 57);
            this.lblBestStars.TabIndex = 6;
            this.lblBestStars.Text = "☆☆☆☆☆";
            this.lblBestStars.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLastStars
            // 
            this.lblLastStars.AutoSize = true;
            this.lblLastStars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastStars.Font = new System.Drawing.Font("Lucida Console", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastStars.Location = new System.Drawing.Point(3, 219);
            this.lblLastStars.Name = "lblLastStars";
            this.lblLastStars.Size = new System.Drawing.Size(164, 57);
            this.lblLastStars.TabIndex = 5;
            this.lblLastStars.Text = "☆☆☆☆☆";
            this.lblLastStars.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBestLabel
            // 
            this.lblBestLabel.AutoSize = true;
            this.lblBestLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBestLabel.Location = new System.Drawing.Point(173, 110);
            this.lblBestLabel.Name = "lblBestLabel";
            this.lblBestLabel.Size = new System.Drawing.Size(164, 27);
            this.lblBestLabel.TabIndex = 1;
            this.lblBestLabel.Text = "Best Score:";
            this.lblBestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBestScore
            // 
            this.lblBestScore.AutoSize = true;
            this.lblBestScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBestScore.Font = new System.Drawing.Font("Lucida Console", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBestScore.Location = new System.Drawing.Point(173, 137);
            this.lblBestScore.Name = "lblBestScore";
            this.lblBestScore.Size = new System.Drawing.Size(164, 82);
            this.lblBestScore.TabIndex = 1;
            this.lblBestScore.Text = "00.00%";
            this.lblBestScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastDateLabel
            // 
            this.lblLastDateLabel.AutoSize = true;
            this.lblLastDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastDateLabel.Location = new System.Drawing.Point(3, 55);
            this.lblLastDateLabel.Name = "lblLastDateLabel";
            this.lblLastDateLabel.Size = new System.Drawing.Size(164, 55);
            this.lblLastDateLabel.TabIndex = 2;
            this.lblLastDateLabel.Text = "Last Attempt: ";
            this.lblLastDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAttemptsLabel
            // 
            this.lblAttemptsLabel.AutoSize = true;
            this.lblAttemptsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAttemptsLabel.Location = new System.Drawing.Point(3, 0);
            this.lblAttemptsLabel.Name = "lblAttemptsLabel";
            this.lblAttemptsLabel.Size = new System.Drawing.Size(164, 55);
            this.lblAttemptsLabel.TabIndex = 1;
            this.lblAttemptsLabel.Text = "Attempts: ";
            this.lblAttemptsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLastScore
            // 
            this.lblLastScore.AutoSize = true;
            this.lblLastScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastScore.Font = new System.Drawing.Font("Lucida Console", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastScore.Location = new System.Drawing.Point(3, 137);
            this.lblLastScore.Name = "lblLastScore";
            this.lblLastScore.Size = new System.Drawing.Size(164, 82);
            this.lblLastScore.TabIndex = 1;
            this.lblLastScore.Text = "00.00%";
            this.lblLastScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAttempts
            // 
            this.lblAttempts.AutoSize = true;
            this.lblAttempts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAttempts.Location = new System.Drawing.Point(173, 0);
            this.lblAttempts.Name = "lblAttempts";
            this.lblAttempts.Size = new System.Drawing.Size(164, 55);
            this.lblAttempts.TabIndex = 3;
            this.lblAttempts.Text = "0";
            this.lblAttempts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastDate
            // 
            this.lblLastDate.AutoSize = true;
            this.lblLastDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastDate.Location = new System.Drawing.Point(173, 55);
            this.lblLastDate.Name = "lblLastDate";
            this.lblLastDate.Size = new System.Drawing.Size(164, 55);
            this.lblLastDate.TabIndex = 4;
            this.lblLastDate.Text = "1/1/2001";
            this.lblLastDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastLabel
            // 
            this.lblLastLabel.AutoSize = true;
            this.lblLastLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastLabel.Location = new System.Drawing.Point(3, 110);
            this.lblLastLabel.Name = "lblLastLabel";
            this.lblLastLabel.Size = new System.Drawing.Size(164, 27);
            this.lblLastLabel.TabIndex = 1;
            this.lblLastLabel.Text = "Last Score:";
            this.lblLastLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpWordStats
            // 
            this.grpWordStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpWordStats.Controls.Add(this.dataWords);
            this.grpWordStats.Location = new System.Drawing.Point(378, 42);
            this.grpWordStats.Name = "grpWordStats";
            this.grpWordStats.Size = new System.Drawing.Size(604, 297);
            this.grpWordStats.TabIndex = 3;
            this.grpWordStats.TabStop = false;
            this.grpWordStats.Text = "Word Performances";
            // 
            // dataWords
            // 
            this.dataWords.AllowUserToAddRows = false;
            this.dataWords.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataWords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWord,
            this.colLastPerf,
            this.colTotalPerf});
            this.dataWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataWords.Location = new System.Drawing.Point(3, 18);
            this.dataWords.Name = "dataWords";
            this.dataWords.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataWords.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataWords.RowHeadersVisible = false;
            this.dataWords.RowHeadersWidth = 51;
            this.dataWords.RowTemplate.Height = 24;
            this.dataWords.Size = new System.Drawing.Size(598, 276);
            this.dataWords.TabIndex = 0;
            // 
            // colWord
            // 
            this.colWord.HeaderText = "Word";
            this.colWord.MinimumWidth = 6;
            this.colWord.Name = "colWord";
            this.colWord.ReadOnly = true;
            this.colWord.Width = 200;
            // 
            // colLastPerf
            // 
            this.colLastPerf.HeaderText = "Last Result";
            this.colLastPerf.MinimumWidth = 6;
            this.colLastPerf.Name = "colLastPerf";
            this.colLastPerf.ReadOnly = true;
            this.colLastPerf.Width = 150;
            // 
            // colTotalPerf
            // 
            this.colTotalPerf.HeaderText = "Total Results";
            this.colTotalPerf.MinimumWidth = 6;
            this.colTotalPerf.Name = "colTotalPerf";
            this.colTotalPerf.ReadOnly = true;
            this.colTotalPerf.Width = 150;
            // 
            // frmSpellingQuizPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 375);
            this.Controls.Add(this.grpWordStats);
            this.Controls.Add(this.grpListStats);
            this.Controls.Add(this.lblSpellingList);
            this.Controls.Add(this.cboSpellingLists);
            this.Controls.Add(this.lblUser);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpellingQuizPerformance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spelling Stats";
            this.grpListStats.ResumeLayout(false);
            this.tableListPerformance.ResumeLayout(false);
            this.tableListPerformance.PerformLayout();
            this.grpWordStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataWords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cboSpellingLists;
        private System.Windows.Forms.Label lblSpellingList;
        private System.Windows.Forms.GroupBox grpListStats;
        private System.Windows.Forms.GroupBox grpWordStats;
        private System.Windows.Forms.TableLayoutPanel tableListPerformance;
        private System.Windows.Forms.Label lblBestStars;
        private System.Windows.Forms.Label lblLastStars;
        private System.Windows.Forms.Label lblBestLabel;
        private System.Windows.Forms.Label lblBestScore;
        private System.Windows.Forms.Label lblLastDateLabel;
        private System.Windows.Forms.Label lblAttemptsLabel;
        private System.Windows.Forms.Label lblLastScore;
        private System.Windows.Forms.Label lblAttempts;
        private System.Windows.Forms.Label lblLastDate;
        private System.Windows.Forms.Label lblLastLabel;
        private System.Windows.Forms.DataGridView dataWords;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastPerf;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPerf;
    }
}