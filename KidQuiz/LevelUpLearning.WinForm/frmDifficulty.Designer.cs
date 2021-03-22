namespace LevelUpLearning.WinForm
{
    partial class frmDifficulty
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
            this.cboOptions = new System.Windows.Forms.ComboBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnEditWords = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboOptions
            // 
            this.cboOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOptions.FormattingEnabled = true;
            this.cboOptions.Location = new System.Drawing.Point(12, 12);
            this.cboOptions.Name = "cboOptions";
            this.cboOptions.Size = new System.Drawing.Size(444, 32);
            this.cboOptions.TabIndex = 0;
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(343, 50);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(113, 49);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnEditWords
            // 
            this.btnEditWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditWords.Location = new System.Drawing.Point(12, 107);
            this.btnEditWords.Name = "btnEditWords";
            this.btnEditWords.Size = new System.Drawing.Size(113, 49);
            this.btnEditWords.TabIndex = 2;
            this.btnEditWords.Text = "Edit Words";
            this.btnEditWords.UseVisualStyleBackColor = true;
            this.btnEditWords.Click += new System.EventHandler(this.btnEditWords_Click);
            // 
            // frmDifficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 168);
            this.Controls.Add(this.btnEditWords);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.cboOptions);
            this.Name = "frmDifficulty";
            this.Text = "Select Difficulty";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboOptions;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnEditWords;
    }
}