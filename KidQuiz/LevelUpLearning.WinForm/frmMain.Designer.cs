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
            this.btnSpellingTest = new System.Windows.Forms.Button();
            this.btnSpellingTestSetup = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblComingSoon = new System.Windows.Forms.Label();
            this.btnSpellingStats = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSpellingTest
            // 
            this.btnSpellingTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpellingTest.Location = new System.Drawing.Point(39, 113);
            this.btnSpellingTest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSpellingTest.Name = "btnSpellingTest";
            this.btnSpellingTest.Size = new System.Drawing.Size(194, 86);
            this.btnSpellingTest.TabIndex = 5;
            this.btnSpellingTest.Text = "Spelling Test";
            this.btnSpellingTest.UseVisualStyleBackColor = true;
            this.btnSpellingTest.Click += new System.EventHandler(this.btnSpellingTest_Click);
            // 
            // btnSpellingTestSetup
            // 
            this.btnSpellingTestSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpellingTestSetup.Location = new System.Drawing.Point(39, 203);
            this.btnSpellingTestSetup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSpellingTestSetup.Name = "btnSpellingTestSetup";
            this.btnSpellingTestSetup.Size = new System.Drawing.Size(94, 32);
            this.btnSpellingTestSetup.TabIndex = 5;
            this.btnSpellingTestSetup.Text = "Edit...";
            this.btnSpellingTestSetup.UseVisualStyleBackColor = true;
            this.btnSpellingTestSetup.Click += new System.EventHandler(this.btnSpellingTestSetup_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.Location = new System.Drawing.Point(12, 20);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(435, 29);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Welcome, [[user]]";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.DarkRed;
            this.btnLogout.Location = new System.Drawing.Point(453, 11);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(61, 41);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "❌";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblComingSoon
            // 
            this.lblComingSoon.AutoSize = true;
            this.lblComingSoon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComingSoon.Location = new System.Drawing.Point(83, 266);
            this.lblComingSoon.Name = "lblComingSoon";
            this.lblComingSoon.Size = new System.Drawing.Size(117, 20);
            this.lblComingSoon.TabIndex = 8;
            this.lblComingSoon.Text = "More to come!";
            // 
            // btnSpellingStats
            // 
            this.btnSpellingStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpellingStats.Location = new System.Drawing.Point(139, 203);
            this.btnSpellingStats.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSpellingStats.Name = "btnSpellingStats";
            this.btnSpellingStats.Size = new System.Drawing.Size(94, 32);
            this.btnSpellingStats.TabIndex = 9;
            this.btnSpellingStats.Text = "Stats";
            this.btnSpellingStats.UseVisualStyleBackColor = true;
            this.btnSpellingStats.Click += new System.EventHandler(this.btnSpellingStats_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(12, 60);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(435, 29);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Version X";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnUpdate.Location = new System.Drawing.Point(453, 56);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(61, 41);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "🔁";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 294);
            this.Controls.Add(this.btnSpellingStats);
            this.Controls.Add(this.lblComingSoon);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.btnSpellingTestSetup);
            this.Controls.Add(this.btnSpellingTest);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Level Up Learning - Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSpellingTest;
        private System.Windows.Forms.Button btnSpellingTestSetup;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblComingSoon;
        private System.Windows.Forms.Button btnSpellingStats;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnUpdate;
    }
}