
namespace LevelUpLearning.WinForm
{
    partial class frmSpellingListsSetup
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
            this.grpList = new System.Windows.Forms.GroupBox();
            this.lblListDifficulty = new System.Windows.Forms.Label();
            this.btnCancelList = new System.Windows.Forms.Button();
            this.btnSaveList = new System.Windows.Forms.Button();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.lblListName = new System.Windows.Forms.Label();
            this.btnDeleteWord = new System.Windows.Forms.Button();
            this.lstWords = new System.Windows.Forms.ListBox();
            this.btnEditWord = new System.Windows.Forms.Button();
            this.btnAddWord = new System.Windows.Forms.Button();
            this.lstSpellingLists = new System.Windows.Forms.ListBox();
            this.grpLists = new System.Windows.Forms.GroupBox();
            this.btnAddList = new System.Windows.Forms.Button();
            this.btnCancelAll = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnEditList = new System.Windows.Forms.Button();
            this.btnDeleteList = new System.Windows.Forms.Button();
            this.btnHearTest = new System.Windows.Forms.Button();
            this.txtExampleSentence = new System.Windows.Forms.TextBox();
            this.lblExampleSentence = new System.Windows.Forms.Label();
            this.txtSpellingWord = new System.Windows.Forms.TextBox();
            this.lblSpellingWord = new System.Windows.Forms.Label();
            this.grpWord = new System.Windows.Forms.GroupBox();
            this.lblDifficultyRating = new System.Windows.Forms.Label();
            this.btnInsertWord = new System.Windows.Forms.Button();
            this.btnCancelWord = new System.Windows.Forms.Button();
            this.btnSaveWord = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpList.SuspendLayout();
            this.grpLists.SuspendLayout();
            this.grpWord.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.lblListDifficulty);
            this.grpList.Controls.Add(this.btnCancelList);
            this.grpList.Controls.Add(this.btnSaveList);
            this.grpList.Controls.Add(this.txtListName);
            this.grpList.Controls.Add(this.lblListName);
            this.grpList.Controls.Add(this.btnDeleteWord);
            this.grpList.Controls.Add(this.lstWords);
            this.grpList.Controls.Add(this.btnEditWord);
            this.grpList.Controls.Add(this.btnAddWord);
            this.grpList.Location = new System.Drawing.Point(336, 20);
            this.grpList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpList.Size = new System.Drawing.Size(319, 446);
            this.grpList.TabIndex = 27;
            this.grpList.TabStop = false;
            this.grpList.Text = "List Details";
            this.grpList.Visible = false;
            // 
            // lblListDifficulty
            // 
            this.lblListDifficulty.AutoSize = true;
            this.lblListDifficulty.Location = new System.Drawing.Point(6, 321);
            this.lblListDifficulty.Name = "lblListDifficulty";
            this.lblListDifficulty.Size = new System.Drawing.Size(118, 34);
            this.lblListDifficulty.TabIndex = 34;
            this.lblListDifficulty.Text = "Overall Difficulty: \r\n??";
            // 
            // btnCancelList
            // 
            this.btnCancelList.Location = new System.Drawing.Point(6, 411);
            this.btnCancelList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelList.Name = "btnCancelList";
            this.btnCancelList.Size = new System.Drawing.Size(94, 31);
            this.btnCancelList.TabIndex = 30;
            this.btnCancelList.Text = "Cancel";
            this.btnCancelList.UseVisualStyleBackColor = true;
            this.btnCancelList.Click += new System.EventHandler(this.btnCancelList_Click);
            // 
            // btnSaveList
            // 
            this.btnSaveList.Location = new System.Drawing.Point(106, 411);
            this.btnSaveList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveList.Name = "btnSaveList";
            this.btnSaveList.Size = new System.Drawing.Size(194, 31);
            this.btnSaveList.TabIndex = 31;
            this.btnSaveList.Text = "Save Changes";
            this.btnSaveList.UseVisualStyleBackColor = true;
            this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(6, 39);
            this.txtListName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(294, 22);
            this.txtListName.TabIndex = 32;
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Location = new System.Drawing.Point(6, 20);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(71, 17);
            this.lblListName.TabIndex = 31;
            this.lblListName.Text = "List Name";
            // 
            // btnDeleteWord
            // 
            this.btnDeleteWord.Location = new System.Drawing.Point(206, 357);
            this.btnDeleteWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteWord.Name = "btnDeleteWord";
            this.btnDeleteWord.Size = new System.Drawing.Size(94, 31);
            this.btnDeleteWord.TabIndex = 21;
            this.btnDeleteWord.Text = "Delete";
            this.btnDeleteWord.UseVisualStyleBackColor = true;
            this.btnDeleteWord.Click += new System.EventHandler(this.btnDeleteWord_Click);
            // 
            // lstWords
            // 
            this.lstWords.FormattingEnabled = true;
            this.lstWords.ItemHeight = 16;
            this.lstWords.Location = new System.Drawing.Point(6, 66);
            this.lstWords.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstWords.Name = "lstWords";
            this.lstWords.Size = new System.Drawing.Size(294, 244);
            this.lstWords.TabIndex = 16;
            this.lstWords.SelectedIndexChanged += new System.EventHandler(this.lstWords_SelectedIndexChanged);
            // 
            // btnEditWord
            // 
            this.btnEditWord.Location = new System.Drawing.Point(106, 357);
            this.btnEditWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditWord.Name = "btnEditWord";
            this.btnEditWord.Size = new System.Drawing.Size(94, 31);
            this.btnEditWord.TabIndex = 19;
            this.btnEditWord.Text = "Edit";
            this.btnEditWord.UseVisualStyleBackColor = true;
            this.btnEditWord.Click += new System.EventHandler(this.btnEditWord_Click);
            // 
            // btnAddWord
            // 
            this.btnAddWord.Location = new System.Drawing.Point(6, 357);
            this.btnAddWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddWord.Name = "btnAddWord";
            this.btnAddWord.Size = new System.Drawing.Size(94, 31);
            this.btnAddWord.TabIndex = 17;
            this.btnAddWord.Text = "Add";
            this.btnAddWord.UseVisualStyleBackColor = true;
            this.btnAddWord.Click += new System.EventHandler(this.btnAddWord_Click);
            // 
            // lstSpellingLists
            // 
            this.lstSpellingLists.FormattingEnabled = true;
            this.lstSpellingLists.ItemHeight = 16;
            this.lstSpellingLists.Location = new System.Drawing.Point(6, 21);
            this.lstSpellingLists.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstSpellingLists.Name = "lstSpellingLists";
            this.lstSpellingLists.Size = new System.Drawing.Size(294, 292);
            this.lstSpellingLists.TabIndex = 14;
            this.lstSpellingLists.SelectedIndexChanged += new System.EventHandler(this.lstSpellingLists_SelectedIndexChanged);
            // 
            // grpLists
            // 
            this.grpLists.Controls.Add(this.btnExport);
            this.grpLists.Controls.Add(this.btnImport);
            this.grpLists.Controls.Add(this.lstSpellingLists);
            this.grpLists.Controls.Add(this.btnAddList);
            this.grpLists.Controls.Add(this.btnCancelAll);
            this.grpLists.Controls.Add(this.btnSaveAll);
            this.grpLists.Controls.Add(this.btnEditList);
            this.grpLists.Controls.Add(this.btnDeleteList);
            this.grpLists.Location = new System.Drawing.Point(12, 20);
            this.grpLists.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLists.Name = "grpLists";
            this.grpLists.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLists.Size = new System.Drawing.Size(315, 446);
            this.grpLists.TabIndex = 25;
            this.grpLists.TabStop = false;
            this.grpLists.Text = "Spelling Lists";
            this.grpLists.Visible = false;
            // 
            // btnAddList
            // 
            this.btnAddList.Location = new System.Drawing.Point(6, 357);
            this.btnAddList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new System.Drawing.Size(94, 31);
            this.btnAddList.TabIndex = 26;
            this.btnAddList.Text = "Add";
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
            // 
            // btnCancelAll
            // 
            this.btnCancelAll.Location = new System.Drawing.Point(6, 411);
            this.btnCancelAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelAll.Name = "btnCancelAll";
            this.btnCancelAll.Size = new System.Drawing.Size(94, 31);
            this.btnCancelAll.TabIndex = 28;
            this.btnCancelAll.Text = "Cancel";
            this.btnCancelAll.UseVisualStyleBackColor = true;
            this.btnCancelAll.Click += new System.EventHandler(this.btnCancelAll_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(106, 411);
            this.btnSaveAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(194, 31);
            this.btnSaveAll.TabIndex = 28;
            this.btnSaveAll.Text = "Save Changes";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnEditList
            // 
            this.btnEditList.Location = new System.Drawing.Point(106, 357);
            this.btnEditList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditList.Name = "btnEditList";
            this.btnEditList.Size = new System.Drawing.Size(94, 31);
            this.btnEditList.TabIndex = 28;
            this.btnEditList.Text = "Edit";
            this.btnEditList.UseVisualStyleBackColor = true;
            this.btnEditList.Click += new System.EventHandler(this.btnEditList_Click);
            // 
            // btnDeleteList
            // 
            this.btnDeleteList.Location = new System.Drawing.Point(206, 357);
            this.btnDeleteList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteList.Name = "btnDeleteList";
            this.btnDeleteList.Size = new System.Drawing.Size(94, 31);
            this.btnDeleteList.TabIndex = 29;
            this.btnDeleteList.Text = "Delete";
            this.btnDeleteList.UseVisualStyleBackColor = true;
            this.btnDeleteList.Click += new System.EventHandler(this.btnDeleteList_Click);
            // 
            // btnHearTest
            // 
            this.btnHearTest.Location = new System.Drawing.Point(160, 357);
            this.btnHearTest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHearTest.Name = "btnHearTest";
            this.btnHearTest.Size = new System.Drawing.Size(140, 44);
            this.btnHearTest.TabIndex = 30;
            this.btnHearTest.Text = "Hear Test";
            this.btnHearTest.UseVisualStyleBackColor = true;
            this.btnHearTest.Click += new System.EventHandler(this.btnHearTest_Click);
            // 
            // txtExampleSentence
            // 
            this.txtExampleSentence.AcceptsReturn = true;
            this.txtExampleSentence.Location = new System.Drawing.Point(6, 84);
            this.txtExampleSentence.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtExampleSentence.Multiline = true;
            this.txtExampleSentence.Name = "txtExampleSentence";
            this.txtExampleSentence.Size = new System.Drawing.Size(294, 226);
            this.txtExampleSentence.TabIndex = 24;
            // 
            // lblExampleSentence
            // 
            this.lblExampleSentence.AutoSize = true;
            this.lblExampleSentence.Location = new System.Drawing.Point(6, 66);
            this.lblExampleSentence.Name = "lblExampleSentence";
            this.lblExampleSentence.Size = new System.Drawing.Size(297, 17);
            this.lblExampleSentence.TabIndex = 22;
            this.lblExampleSentence.Text = "Example Sentence (use %% for spelling word)";
            // 
            // txtSpellingWord
            // 
            this.txtSpellingWord.Location = new System.Drawing.Point(6, 39);
            this.txtSpellingWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSpellingWord.Name = "txtSpellingWord";
            this.txtSpellingWord.Size = new System.Drawing.Size(294, 22);
            this.txtSpellingWord.TabIndex = 20;
            this.txtSpellingWord.TextChanged += new System.EventHandler(this.txtSpellingWord_TextChanged);
            // 
            // lblSpellingWord
            // 
            this.lblSpellingWord.AutoSize = true;
            this.lblSpellingWord.Location = new System.Drawing.Point(6, 20);
            this.lblSpellingWord.Name = "lblSpellingWord";
            this.lblSpellingWord.Size = new System.Drawing.Size(253, 17);
            this.lblSpellingWord.TabIndex = 18;
            this.lblSpellingWord.Text = "Word (make sure it\'s spelled correctly!)";
            // 
            // grpWord
            // 
            this.grpWord.Controls.Add(this.lblDifficultyRating);
            this.grpWord.Controls.Add(this.btnInsertWord);
            this.grpWord.Controls.Add(this.btnCancelWord);
            this.grpWord.Controls.Add(this.btnSaveWord);
            this.grpWord.Controls.Add(this.txtExampleSentence);
            this.grpWord.Controls.Add(this.lblSpellingWord);
            this.grpWord.Controls.Add(this.txtSpellingWord);
            this.grpWord.Controls.Add(this.lblExampleSentence);
            this.grpWord.Controls.Add(this.btnHearTest);
            this.grpWord.Location = new System.Drawing.Point(672, 20);
            this.grpWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpWord.Name = "grpWord";
            this.grpWord.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpWord.Size = new System.Drawing.Size(319, 446);
            this.grpWord.TabIndex = 33;
            this.grpWord.TabStop = false;
            this.grpWord.Text = "Spelling Word";
            this.grpWord.Visible = false;
            // 
            // lblDifficultyRating
            // 
            this.lblDifficultyRating.AutoSize = true;
            this.lblDifficultyRating.Location = new System.Drawing.Point(6, 321);
            this.lblDifficultyRating.Name = "lblDifficultyRating";
            this.lblDifficultyRating.Size = new System.Drawing.Size(151, 34);
            this.lblDifficultyRating.TabIndex = 34;
            this.lblDifficultyRating.Text = "Approximate Difficulty: \r\n??";
            // 
            // btnInsertWord
            // 
            this.btnInsertWord.Location = new System.Drawing.Point(6, 357);
            this.btnInsertWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInsertWord.Name = "btnInsertWord";
            this.btnInsertWord.Size = new System.Drawing.Size(140, 44);
            this.btnInsertWord.TabIndex = 33;
            this.btnInsertWord.Text = "Insert Vocab Word";
            this.btnInsertWord.UseVisualStyleBackColor = true;
            this.btnInsertWord.Click += new System.EventHandler(this.btnInsertWord_Click);
            // 
            // btnCancelWord
            // 
            this.btnCancelWord.Location = new System.Drawing.Point(6, 411);
            this.btnCancelWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelWord.Name = "btnCancelWord";
            this.btnCancelWord.Size = new System.Drawing.Size(94, 31);
            this.btnCancelWord.TabIndex = 31;
            this.btnCancelWord.Text = "Cancel";
            this.btnCancelWord.UseVisualStyleBackColor = true;
            this.btnCancelWord.Click += new System.EventHandler(this.btnCancelWord_Click);
            // 
            // btnSaveWord
            // 
            this.btnSaveWord.Location = new System.Drawing.Point(106, 411);
            this.btnSaveWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveWord.Name = "btnSaveWord";
            this.btnSaveWord.Size = new System.Drawing.Size(194, 31);
            this.btnSaveWord.TabIndex = 32;
            this.btnSaveWord.Text = "Save Changes";
            this.btnSaveWord.UseVisualStyleBackColor = true;
            this.btnSaveWord.Click += new System.EventHandler(this.btnSaveWord_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(6, 317);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 31);
            this.btnImport.TabIndex = 30;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(206, 317);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 31);
            this.btnExport.TabIndex = 30;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Filter = "XML|*.xml";
            this.saveFileDialog.Title = "Export Word List";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "XML|*.xml";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Import Word List";
            // 
            // frmSpellingListsSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 477);
            this.Controls.Add(this.grpWord);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.grpLists);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmSpellingListsSetup";
            this.Text = "Spelling Lists Setup";
            this.grpList.ResumeLayout(false);
            this.grpList.PerformLayout();
            this.grpLists.ResumeLayout(false);
            this.grpWord.ResumeLayout(false);
            this.grpWord.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Button btnCancelList;
        private System.Windows.Forms.Button btnSaveList;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.Button btnDeleteWord;
        private System.Windows.Forms.ListBox lstWords;
        private System.Windows.Forms.Button btnEditWord;
        private System.Windows.Forms.Button btnAddWord;
        private System.Windows.Forms.ListBox lstSpellingLists;
        private System.Windows.Forms.GroupBox grpLists;
        private System.Windows.Forms.Button btnAddList;
        private System.Windows.Forms.Button btnCancelAll;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnEditList;
        private System.Windows.Forms.Button btnDeleteList;
        private System.Windows.Forms.Button btnHearTest;
        private System.Windows.Forms.TextBox txtExampleSentence;
        private System.Windows.Forms.Label lblExampleSentence;
        private System.Windows.Forms.TextBox txtSpellingWord;
        private System.Windows.Forms.Label lblSpellingWord;
        private System.Windows.Forms.GroupBox grpWord;
        private System.Windows.Forms.Label lblDifficultyRating;
        private System.Windows.Forms.Button btnInsertWord;
        private System.Windows.Forms.Button btnCancelWord;
        private System.Windows.Forms.Button btnSaveWord;
        private System.Windows.Forms.Label lblListDifficulty;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}