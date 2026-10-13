
namespace LevelUpLearning.WinForms.Spelling
{
    partial class frmSpellingListsQuickCreate
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtWordList = new System.Windows.Forms.TextBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblListName = new System.Windows.Forms.Label();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.lblListPreview = new System.Windows.Forms.Label();
            this.lblListSize = new System.Windows.Forms.Label();
            this.numListSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numListSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type / paste word list below.\r\nTo include example sentences, type them after a ta" +
    "b";
            // 
            // txtWordList
            // 
            this.txtWordList.AcceptsReturn = true;
            this.txtWordList.AcceptsTab = true;
            this.txtWordList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWordList.Location = new System.Drawing.Point(12, 61);
            this.txtWordList.Multiline = true;
            this.txtWordList.Name = "txtWordList";
            this.txtWordList.Size = new System.Drawing.Size(776, 328);
            this.txtWordList.TabIndex = 1;
            this.txtWordList.TextChanged += new System.EventHandler(this.UpdateListNames);
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.Location = new System.Drawing.Point(713, 396);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 59);
            this.btnFinish.TabIndex = 2;
            this.btnFinish.Text = "Create!";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(713, 12);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 43);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblListName
            // 
            this.lblListName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new System.Drawing.Point(24, 404);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(75, 17);
            this.lblListName.TabIndex = 4;
            this.lblListName.Text = "List Name:";
            // 
            // txtListName
            // 
            this.txtListName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtListName.Location = new System.Drawing.Point(105, 404);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(228, 22);
            this.txtListName.TabIndex = 5;
            this.txtListName.Text = "New List";
            this.txtListName.TextChanged += new System.EventHandler(this.UpdateListNames);
            // 
            // lblListPreview
            // 
            this.lblListPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListPreview.Location = new System.Drawing.Point(339, 404);
            this.lblListPreview.Name = "lblListPreview";
            this.lblListPreview.Size = new System.Drawing.Size(368, 47);
            this.lblListPreview.TabIndex = 6;
            this.lblListPreview.Text = "List names will go here";
            // 
            // lblListSize
            // 
            this.lblListSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblListSize.AutoSize = true;
            this.lblListSize.Location = new System.Drawing.Point(12, 434);
            this.lblListSize.Name = "lblListSize";
            this.lblListSize.Size = new System.Drawing.Size(87, 17);
            this.lblListSize.TabIndex = 7;
            this.lblListSize.Text = "Words / List:";
            // 
            // numListSize
            // 
            this.numListSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numListSize.Location = new System.Drawing.Point(105, 432);
            this.numListSize.Name = "numListSize";
            this.numListSize.Size = new System.Drawing.Size(120, 22);
            this.numListSize.TabIndex = 8;
            this.numListSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numListSize.ValueChanged += new System.EventHandler(this.UpdateListNames);
            // 
            // frmSpellingListsQuickCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 467);
            this.Controls.Add(this.numListSize);
            this.Controls.Add(this.lblListSize);
            this.Controls.Add(this.lblListPreview);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.txtWordList);
            this.Controls.Add(this.label1);
            this.Name = "frmSpellingListsQuickCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spelling Words - Quick Create";
            ((System.ComponentModel.ISupportInitialize)(this.numListSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWordList;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.Label lblListPreview;
        private System.Windows.Forms.Label lblListSize;
        private System.Windows.Forms.NumericUpDown numListSize;
    }
}