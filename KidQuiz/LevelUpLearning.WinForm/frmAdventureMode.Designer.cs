
namespace LevelUpLearning.WinForms
{
    partial class frmAdventureMode
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
            this.rpgBanner1 = new LevelUpLearning.WinForms.Common.RpgBanner();
            this.panelActivity = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panelActivity.SuspendLayout();
            this.SuspendLayout();
            // 
            // rpgBanner1
            // 
            this.rpgBanner1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rpgBanner1.Location = new System.Drawing.Point(0, 0);
            this.rpgBanner1.Name = "rpgBanner1";
            this.rpgBanner1.Size = new System.Drawing.Size(914, 87);
            this.rpgBanner1.TabIndex = 0;
            // 
            // panelActivity
            // 
            this.panelActivity.Controls.Add(this.button1);
            this.panelActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActivity.Location = new System.Drawing.Point(0, 73);
            this.panelActivity.Name = "panelActivity";
            this.panelActivity.Size = new System.Drawing.Size(762, 378);
            this.panelActivity.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmAdventureMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 540);
            this.Controls.Add(this.panelActivity);
            this.Controls.Add(this.rpgBanner1);
            this.Name = "frmAdventureMode";
            this.Text = "Level Up Learning - Adventure Mode";
            this.panelActivity.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Common.RpgBanner rpgBanner1;
        private System.Windows.Forms.FlowLayoutPanel panelActivity;
        private System.Windows.Forms.Button button1;
    }
}