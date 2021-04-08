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
            this.SuspendLayout();
            // 
            // btnSpellingTest
            // 
            this.btnSpellingTest.Location = new System.Drawing.Point(21, 115);
            this.btnSpellingTest.Name = "btnSpellingTest";
            this.btnSpellingTest.Size = new System.Drawing.Size(207, 29);
            this.btnSpellingTest.TabIndex = 5;
            this.btnSpellingTest.Text = "Spelling Test";
            this.btnSpellingTest.UseVisualStyleBackColor = true;
            this.btnSpellingTest.Click += new System.EventHandler(this.btnSpellingTest_Click);
            // 
            // btnSpellingTestSetup
            // 
            this.btnSpellingTestSetup.Location = new System.Drawing.Point(272, 115);
            this.btnSpellingTestSetup.Name = "btnSpellingTestSetup";
            this.btnSpellingTestSetup.Size = new System.Drawing.Size(94, 29);
            this.btnSpellingTestSetup.TabIndex = 5;
            this.btnSpellingTestSetup.Text = "Edit...";
            this.btnSpellingTestSetup.UseVisualStyleBackColor = true;
            this.btnSpellingTestSetup.Click += new System.EventHandler(this.btnSpellingTestSetup_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(276, 11);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(426, 20);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Welcome, [[user]] //TODO: Set this //TODO: Make right-aligned";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(708, 7);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(94, 29);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblComingSoon
            // 
            this.lblComingSoon.AutoSize = true;
            this.lblComingSoon.Location = new System.Drawing.Point(135, 194);
            this.lblComingSoon.Name = "lblComingSoon";
            this.lblComingSoon.Size = new System.Drawing.Size(107, 20);
            this.lblComingSoon.TabIndex = 8;
            this.lblComingSoon.Text = "More to come!";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 336);
            this.Controls.Add(this.lblComingSoon);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.btnSpellingTestSetup);
            this.Controls.Add(this.btnSpellingTest);
            this.Name = "frmMain";
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
    }
}