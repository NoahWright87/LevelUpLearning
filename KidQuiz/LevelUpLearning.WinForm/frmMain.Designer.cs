﻿namespace LevelUpLearning.WinForm
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
            this.btnSpellingTest.Location = new System.Drawing.Point(21, 92);
            this.btnSpellingTest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSpellingTest.Name = "btnSpellingTest";
            this.btnSpellingTest.Size = new System.Drawing.Size(207, 32);
            this.btnSpellingTest.TabIndex = 5;
            this.btnSpellingTest.Text = "Spelling Test";
            this.btnSpellingTest.UseVisualStyleBackColor = true;
            this.btnSpellingTest.Click += new System.EventHandler(this.btnSpellingTest_Click);
            // 
            // btnSpellingTestSetup
            // 
            this.btnSpellingTestSetup.Location = new System.Drawing.Point(272, 92);
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
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(276, 9);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(404, 17);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Welcome, [[user]] //TODO: Set this //TODO: Make right-aligned";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(708, 6);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(94, 32);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblComingSoon
            // 
            this.lblComingSoon.AutoSize = true;
            this.lblComingSoon.Location = new System.Drawing.Point(135, 155);
            this.lblComingSoon.Name = "lblComingSoon";
            this.lblComingSoon.Size = new System.Drawing.Size(97, 17);
            this.lblComingSoon.TabIndex = 8;
            this.lblComingSoon.Text = "More to come!";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 269);
            this.Controls.Add(this.lblComingSoon);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.btnSpellingTestSetup);
            this.Controls.Add(this.btnSpellingTest);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
    }
}