using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCards4Spelling
{
    internal partial class FormDataMaintenance : Form
    {
        private string ui_save = "Save";
        private string ui_add = "Add";
        private DataLayer data;
        private List<string> wordList;
        private string selectedWord {  get { return listBoxWords.SelectedValue.ToString().Trim(); } }

        public FormDataMaintenance()
        {
            InitializeComponent();
            data = new DataLayer();
            finalizeConstruction();
        }

        public FormDataMaintenance(DataLayer inData)
        {
            InitializeComponent();
            this.data = inData;
            finalizeConstruction();
        }

        private void finalizeConstruction()
        {
            wordList = new List<string>();
            this.data.DataLayerChanged += refreshWordsList;
            refreshWordsList();
        }

        private void checkBoxWordActive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWordActive.Checked == true)
            {
                ((CheckBox)sender).ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ((CheckBox)sender).ForeColor = System.Drawing.Color.Red;
            }
            data.updateWordActive(selectedWord, checkBoxWordActive.Checked);
        }

        private void buttonWordEdit_Click(object sender, EventArgs e)
        {
            buttonAdd.Text = ui_save;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (buttonAdd.Text == ui_add)
            {
                addRecord(textBoxNewWord.Text);
            }
            else if (buttonAdd.Text == ui_save)
            {
                if (editRecord(textBoxNewWord.Text))
                {
                    buttonAdd.Text = ui_add;
                }
            }
            textBoxNewWord.Text = string.Empty;
        }

        private bool editRecord(string word)
        {
            bool output = false;
            //edit row in datastructure (if key possible, add/delete new row), select row
            return output;
        }

        private void addRecord(string word)
        {
            //add to datastructure, refresh textBoxWords
            if (data != null)
            {
                data.addWord(word);
            }
        }

        private void textBoxNewWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonAdd_Click(sender, e);
            }
        }

        private void listBoxWords_ControlAdded(object sender, ControlEventArgs e)
        {
            wordList = data.getWordsAll().ToList<string>();
            listBoxWords.DataSource = wordList;
        }

        /// <summary>
        /// handles event/notification from DataLayer that data changed, refresh the list
        /// </summary>
        /// <param name="word"></param>
        private void refreshWordsList(string word = "")
        {
            //there has to be a better way, need to fix how data is refreshed...
            //intention is to obfuscate the underlying data structure with DataLayer without
            //also managing the local copy of the list... maybe look at underlying
            //data structure for the listbox or how it's bound and have DataLayer extend that?
            wordList = data.getWordsAll().ToList<string>();
            listBoxWords.DataSource = wordList;

            if (word != "")
            {
                listBoxWords.SelectedIndex = listBoxWords.FindStringExact(word);
            }
        }

        private void FormDataMaintenance_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.exportData();
        }

        private void listBoxWords_SelectedValueChanged(object sender, EventArgs e)
        {
            string selection = selectedWord;
            if(selection != "")
            {
                textBoxWordCategory.Text = data.getWordCategory(selection);
                checkBoxWordResultMastered.Checked = data.getWordMastered(selection);
                checkBoxWordActive.Checked = data.getWordActive(selection);
                int countCorrect = data.getResultsCorrect(selection);
                labelWordResultsCorrectValue.Text = countCorrect.ToString();
                int countAttempts = data.getResultsAttempts(selection);
                labelWordResultsAttemptsValue.Text = countAttempts.ToString();
                if (countAttempts > 0)
                {
                    labelWordResultsCorrectPercent.Text = 100 * (countCorrect / countAttempts) + "%";
                }
                else
                {
                    labelWordResultsCorrectPercent.Text = "0%";
                }
                listBoxResults.DataSource = data.getResultsList(selection);
                listBoxResults.Refresh();
            }
        }
    }
}
