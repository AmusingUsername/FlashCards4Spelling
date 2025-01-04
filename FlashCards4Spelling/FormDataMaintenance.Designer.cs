namespace FlashCards4Spelling
{
    partial class FormDataMaintenance
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
            this.listBoxWords = new System.Windows.Forms.ListBox();
            this.textBoxNewWord = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.groupBoxWordProperties = new System.Windows.Forms.GroupBox();
            this.checkBoxWordActive = new System.Windows.Forms.CheckBox();
            this.textBoxWordCategory = new System.Windows.Forms.TextBox();
            this.labelWordCategory = new System.Windows.Forms.Label();
            this.groupBoxWordResults = new System.Windows.Forms.GroupBox();
            this.labelWordResultsCorrectValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelWordResultsAttemptsValue = new System.Windows.Forms.Label();
            this.labelWordResultsCorrect = new System.Windows.Forms.Label();
            this.labelWordResultsAttempts = new System.Windows.Forms.Label();
            this.checkBoxWordResultMastered = new System.Windows.Forms.CheckBox();
            this.buttonWordEdit = new System.Windows.Forms.Button();
            this.groupBoxWordProperties.SuspendLayout();
            this.groupBoxWordResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxWords
            // 
            this.listBoxWords.FormattingEnabled = true;
            this.listBoxWords.ItemHeight = 16;
            this.listBoxWords.Location = new System.Drawing.Point(12, 49);
            this.listBoxWords.Name = "listBoxWords";
            this.listBoxWords.Size = new System.Drawing.Size(242, 660);
            this.listBoxWords.TabIndex = 0;
            this.listBoxWords.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.listBoxWords_ControlAdded);
            // 
            // textBoxNewWord
            // 
            this.textBoxNewWord.Location = new System.Drawing.Point(12, 12);
            this.textBoxNewWord.Name = "textBoxNewWord";
            this.textBoxNewWord.Size = new System.Drawing.Size(183, 22);
            this.textBoxNewWord.TabIndex = 1;
            this.textBoxNewWord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxNewWord_KeyUp);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(201, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(53, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // groupBoxWordProperties
            // 
            this.groupBoxWordProperties.Controls.Add(this.checkBoxWordActive);
            this.groupBoxWordProperties.Controls.Add(this.textBoxWordCategory);
            this.groupBoxWordProperties.Controls.Add(this.labelWordCategory);
            this.groupBoxWordProperties.Location = new System.Drawing.Point(269, 78);
            this.groupBoxWordProperties.Name = "groupBoxWordProperties";
            this.groupBoxWordProperties.Size = new System.Drawing.Size(258, 91);
            this.groupBoxWordProperties.TabIndex = 3;
            this.groupBoxWordProperties.TabStop = false;
            // 
            // checkBoxWordActive
            // 
            this.checkBoxWordActive.AutoSize = true;
            this.checkBoxWordActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxWordActive.Checked = true;
            this.checkBoxWordActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWordActive.ForeColor = System.Drawing.Color.Green;
            this.checkBoxWordActive.Location = new System.Drawing.Point(7, 66);
            this.checkBoxWordActive.Name = "checkBoxWordActive";
            this.checkBoxWordActive.Size = new System.Drawing.Size(69, 20);
            this.checkBoxWordActive.TabIndex = 2;
            this.checkBoxWordActive.Text = "Active:";
            this.checkBoxWordActive.UseVisualStyleBackColor = true;
            this.checkBoxWordActive.CheckedChanged += new System.EventHandler(this.checkBoxWordActive_CheckedChanged);
            // 
            // textBoxWordCategory
            // 
            this.textBoxWordCategory.Location = new System.Drawing.Point(9, 38);
            this.textBoxWordCategory.Name = "textBoxWordCategory";
            this.textBoxWordCategory.Size = new System.Drawing.Size(242, 22);
            this.textBoxWordCategory.TabIndex = 1;
            // 
            // labelWordCategory
            // 
            this.labelWordCategory.AutoSize = true;
            this.labelWordCategory.Location = new System.Drawing.Point(6, 18);
            this.labelWordCategory.Name = "labelWordCategory";
            this.labelWordCategory.Size = new System.Drawing.Size(65, 16);
            this.labelWordCategory.TabIndex = 0;
            this.labelWordCategory.Text = "Category:";
            // 
            // groupBoxWordResults
            // 
            this.groupBoxWordResults.Controls.Add(this.labelWordResultsCorrectValue);
            this.groupBoxWordResults.Controls.Add(this.label2);
            this.groupBoxWordResults.Controls.Add(this.labelWordResultsAttemptsValue);
            this.groupBoxWordResults.Controls.Add(this.labelWordResultsCorrect);
            this.groupBoxWordResults.Controls.Add(this.labelWordResultsAttempts);
            this.groupBoxWordResults.Controls.Add(this.checkBoxWordResultMastered);
            this.groupBoxWordResults.Location = new System.Drawing.Point(268, 175);
            this.groupBoxWordResults.Name = "groupBoxWordResults";
            this.groupBoxWordResults.Size = new System.Drawing.Size(259, 102);
            this.groupBoxWordResults.TabIndex = 4;
            this.groupBoxWordResults.TabStop = false;
            // 
            // labelWordResultsCorrectValue
            // 
            this.labelWordResultsCorrectValue.Location = new System.Drawing.Point(198, 43);
            this.labelWordResultsCorrectValue.Name = "labelWordResultsCorrectValue";
            this.labelWordResultsCorrectValue.Size = new System.Drawing.Size(50, 23);
            this.labelWordResultsCorrectValue.TabIndex = 5;
            this.labelWordResultsCorrectValue.Text = "0";
            this.labelWordResultsCorrectValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(148, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "0%";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelWordResultsAttemptsValue
            // 
            this.labelWordResultsAttemptsValue.Location = new System.Drawing.Point(148, 18);
            this.labelWordResultsAttemptsValue.Name = "labelWordResultsAttemptsValue";
            this.labelWordResultsAttemptsValue.Size = new System.Drawing.Size(100, 23);
            this.labelWordResultsAttemptsValue.TabIndex = 3;
            this.labelWordResultsAttemptsValue.Text = "0";
            this.labelWordResultsAttemptsValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelWordResultsCorrect
            // 
            this.labelWordResultsCorrect.Location = new System.Drawing.Point(10, 42);
            this.labelWordResultsCorrect.Name = "labelWordResultsCorrect";
            this.labelWordResultsCorrect.Size = new System.Drawing.Size(62, 24);
            this.labelWordResultsCorrect.TabIndex = 2;
            this.labelWordResultsCorrect.Text = "Correct:";
            // 
            // labelWordResultsAttempts
            // 
            this.labelWordResultsAttempts.Location = new System.Drawing.Point(10, 18);
            this.labelWordResultsAttempts.Name = "labelWordResultsAttempts";
            this.labelWordResultsAttempts.Size = new System.Drawing.Size(62, 24);
            this.labelWordResultsAttempts.TabIndex = 1;
            this.labelWordResultsAttempts.Text = "Attempts:";
            // 
            // checkBoxWordResultMastered
            // 
            this.checkBoxWordResultMastered.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxWordResultMastered.Location = new System.Drawing.Point(8, 69);
            this.checkBoxWordResultMastered.Name = "checkBoxWordResultMastered";
            this.checkBoxWordResultMastered.Size = new System.Drawing.Size(104, 24);
            this.checkBoxWordResultMastered.TabIndex = 0;
            this.checkBoxWordResultMastered.Text = "Mastered:";
            this.checkBoxWordResultMastered.UseVisualStyleBackColor = true;
            // 
            // buttonWordEdit
            // 
            this.buttonWordEdit.Location = new System.Drawing.Point(268, 49);
            this.buttonWordEdit.Name = "buttonWordEdit";
            this.buttonWordEdit.Size = new System.Drawing.Size(137, 23);
            this.buttonWordEdit.TabIndex = 5;
            this.buttonWordEdit.Text = "Edit Selected Word";
            this.buttonWordEdit.UseVisualStyleBackColor = true;
            this.buttonWordEdit.Click += new System.EventHandler(this.buttonWordEdit_Click);
            // 
            // FormDataMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 721);
            this.Controls.Add(this.buttonWordEdit);
            this.Controls.Add(this.groupBoxWordResults);
            this.Controls.Add(this.groupBoxWordProperties);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxNewWord);
            this.Controls.Add(this.listBoxWords);
            this.Name = "FormDataMaintenance";
            this.Text = "FormDataMaintenance";
            this.groupBoxWordProperties.ResumeLayout(false);
            this.groupBoxWordProperties.PerformLayout();
            this.groupBoxWordResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxWords;
        private System.Windows.Forms.TextBox textBoxNewWord;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBoxWordProperties;
        private System.Windows.Forms.TextBox textBoxWordCategory;
        private System.Windows.Forms.Label labelWordCategory;
        private System.Windows.Forms.CheckBox checkBoxWordActive;
        private System.Windows.Forms.GroupBox groupBoxWordResults;
        private System.Windows.Forms.Label labelWordResultsCorrect;
        private System.Windows.Forms.Label labelWordResultsAttempts;
        private System.Windows.Forms.CheckBox checkBoxWordResultMastered;
        private System.Windows.Forms.Label labelWordResultsCorrectValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelWordResultsAttemptsValue;
        private System.Windows.Forms.Button buttonWordEdit;
    }
}