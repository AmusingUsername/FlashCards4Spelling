namespace FlashCards4Spelling
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelLabelVoice = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButtonVoices = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelLabelRate = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonSpeedSlower = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelSpeedRate = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonSpeedFaster = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonModifyWords = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReport = new System.Windows.Forms.ToolStripButton();
            this.buttonReplay = new System.Windows.Forms.Button();
            this.labelFlashCardWord = new System.Windows.Forms.Label();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelLabelVoice,
            this.toolStripDropDownButtonVoices,
            this.toolStripSeparator1,
            this.toolStripLabelLabelRate,
            this.toolStripButtonSpeedSlower,
            this.toolStripLabelSpeedRate,
            this.toolStripButtonSpeedFaster,
            this.toolStripSeparator2,
            this.toolStripButtonModifyWords,
            this.toolStripButtonReport});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(800, 27);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLabelLabelVoice
            // 
            this.toolStripLabelLabelVoice.Name = "toolStripLabelLabelVoice";
            this.toolStripLabelLabelVoice.Size = new System.Drawing.Size(48, 28);
            this.toolStripLabelLabelVoice.Text = "Voice:";
            // 
            // toolStripDropDownButtonVoices
            // 
            this.toolStripDropDownButtonVoices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonVoices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripDropDownButtonVoices.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonVoices.Image")));
            this.toolStripDropDownButtonVoices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonVoices.Name = "toolStripDropDownButtonVoices";
            this.toolStripDropDownButtonVoices.Size = new System.Drawing.Size(118, 24);
            this.toolStripDropDownButtonVoices.Text = "Tir-far-thóinn";
            this.toolStripDropDownButtonVoices.ToolTipText = "Select a voice";
            this.toolStripDropDownButtonVoices.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripDropDownButtonVoices_DropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabelLabelRate
            // 
            this.toolStripLabelLabelRate.Name = "toolStripLabelLabelRate";
            this.toolStripLabelLabelRate.Size = new System.Drawing.Size(54, 28);
            this.toolStripLabelLabelRate.Text = "Speed:";
            // 
            // toolStripButtonSpeedSlower
            // 
            this.toolStripButtonSpeedSlower.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSpeedSlower.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSpeedSlower.Image")));
            this.toolStripButtonSpeedSlower.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSpeedSlower.Name = "toolStripButtonSpeedSlower";
            this.toolStripButtonSpeedSlower.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonSpeedSlower.Text = "-";
            this.toolStripButtonSpeedSlower.ToolTipText = "Decrease rate of speech";
            this.toolStripButtonSpeedSlower.Click += new System.EventHandler(this.toolStripButtonSpeedSlower_Click);
            // 
            // toolStripLabelSpeedRate
            // 
            this.toolStripLabelSpeedRate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabelSpeedRate.Name = "toolStripLabelSpeedRate";
            this.toolStripLabelSpeedRate.Size = new System.Drawing.Size(18, 28);
            this.toolStripLabelSpeedRate.Text = "0";
            // 
            // toolStripButtonSpeedFaster
            // 
            this.toolStripButtonSpeedFaster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSpeedFaster.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSpeedFaster.Image")));
            this.toolStripButtonSpeedFaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSpeedFaster.Name = "toolStripButtonSpeedFaster";
            this.toolStripButtonSpeedFaster.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonSpeedFaster.Text = "+";
            this.toolStripButtonSpeedFaster.ToolTipText = "Increase rate of speech";
            this.toolStripButtonSpeedFaster.Click += new System.EventHandler(this.toolStripButtonSpeedFaster_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonModifyWords
            // 
            this.toolStripButtonModifyWords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonModifyWords.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonModifyWords.Image")));
            this.toolStripButtonModifyWords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonModifyWords.Name = "toolStripButtonModifyWords";
            this.toolStripButtonModifyWords.Size = new System.Drawing.Size(106, 28);
            this.toolStripButtonModifyWords.Text = "Modify Words";
            this.toolStripButtonModifyWords.ToolTipText = "Modify words for flash cards";
            this.toolStripButtonModifyWords.Click += new System.EventHandler(this.toolStripButtonModifyWords_Click);
            // 
            // toolStripButtonReport
            // 
            this.toolStripButtonReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReport.Image")));
            this.toolStripButtonReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReport.Name = "toolStripButtonReport";
            this.toolStripButtonReport.Size = new System.Drawing.Size(58, 28);
            this.toolStripButtonReport.Text = "Report";
            this.toolStripButtonReport.ToolTipText = "Check progress";
            this.toolStripButtonReport.Click += new System.EventHandler(this.toolStripButtonReport_Click);
            // 
            // buttonReplay
            // 
            this.buttonReplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReplay.Location = new System.Drawing.Point(12, 30);
            this.buttonReplay.Name = "buttonReplay";
            this.buttonReplay.Size = new System.Drawing.Size(76, 29);
            this.buttonReplay.TabIndex = 1;
            this.buttonReplay.Text = "Replay Word";
            this.buttonReplay.UseVisualStyleBackColor = true;
            this.buttonReplay.Click += new System.EventHandler(this.buttonReplay_Click);
            // 
            // labelFlashCardWord
            // 
            this.labelFlashCardWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFlashCardWord.Location = new System.Drawing.Point(10, 27);
            this.labelFlashCardWord.Name = "labelFlashCardWord";
            this.labelFlashCardWord.Size = new System.Drawing.Size(778, 414);
            this.labelFlashCardWord.TabIndex = 2;
            this.labelFlashCardWord.Text = "Crikey";
            this.labelFlashCardWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelFlashCardWord.TextChanged += new System.EventHandler(this.labelFlashCardWord_TextChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonReplay);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.labelFlashCardWord);
            this.Name = "FormMain";
            this.Text = "Flash Cards for Spelling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripLabel toolStripLabelLabelVoice;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonVoices;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelLabelRate;
        private System.Windows.Forms.ToolStripButton toolStripButtonSpeedSlower;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSpeedRate;
        private System.Windows.Forms.ToolStripButton toolStripButtonSpeedFaster;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonModifyWords;
        private System.Windows.Forms.ToolStripButton toolStripButtonReport;
        private System.Windows.Forms.Button buttonReplay;
        private System.Windows.Forms.Label labelFlashCardWord;
    }
}

