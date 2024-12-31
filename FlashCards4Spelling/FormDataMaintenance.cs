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
    public partial class FormDataMaintenance : Form
    {
        private string ui_save = "Save";
        private string ui_add = "Add";

        public FormDataMaintenance()
        {
            InitializeComponent();
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
        }
    }
}
