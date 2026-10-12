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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboOptions
            // 
            this.cboOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboOptions.FormattingEnabled = true;
            this.cboOptions.Location = new System.Drawing.Point(16, 18);
            this.cboOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboOptions.Name = "cboOptions";
            this.cboOptions.Size = new System.Drawing.Size(591, 37);
            this.cboOptions.TabIndex = 0;
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGo.Location = new System.Drawing.Point(457, 77);
            this.btnGo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(151, 75);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnEditWords
            // 
            this.btnEditWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnEditWords.Location = new System.Drawing.Point(16, 165);
            this.btnEditWords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditWords.Name = "btnEditWords";
            this.btnEditWords.Size = new System.Drawing.Size(151, 75);
            this.btnEditWords.TabIndex = 2;
            this.btnEditWords.Text = "Edit Words";
            this.btnEditWords.UseVisualStyleBackColor = true;
            this.btnEditWords.Click += new System.EventHandler(this.btnEditWords_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(790, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "//TODO: Remove \"edit words\" button.  Split form, put specific difficulty down bel" +
    "ow (same pattern as word list screen)";
            // 
            // frmDifficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 258);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditWords);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.cboOptions);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDifficulty";
            this.Text = "Select Difficulty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboOptions;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnEditWords;
        private System.Windows.Forms.Label label1;
    }
}