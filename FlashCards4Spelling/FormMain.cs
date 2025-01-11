using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FlashCards4Spelling.Properties;

namespace FlashCards4Spelling
{
    public partial class FormMain : Form
    {
        TTSInterface tts;
        DataLayer data;
        List<string> words = (new string[] { "forgot", "Constantinople", "malcontent", "espresso", "tollgate" }).ToList<string>();
        List<string> responsesCorrect;
        List<string> responsesIncorrect;
        SoundPlayer playerCorrect;
        SoundPlayer playerIncorrect;
        bool lastResponseCorrect = true;

        //Settings which can be overridden with config file setting
        //---------------------------------------------------------
        int requiredWordAttempts = 40;
        bool respondToCorrectWithSpeech = true;
        bool respondToIncorrectWithSpeech = true;
        string responseSoundCorrect = "C:\\Windows\\Media\\tada.wav"; //default value, this can be changed via settings file
        string responseSoundIncorrect = "C:\\Windows\\Media\\ringout.wav"; //default value, this can be changed via settings file
        //---------------------------------------------------------
        //other settings
        //---------------------------------------------------------
        string defaultResponseCorrect = "Correct!";
        string defaultResponseIncorrect = "That was wrong!";
        string defaultResponseFinished = "You're done! Great!";
        string defaultResponseFinishedResults = "You got {0} words correct on the first try!";
        //---------------------------------------------------------


        public FormMain()
        {
            InitializeComponent();
            tts = new TTSInterface();
            initializeToolBarComponents();
            responsesCorrect = new List<string>();
            responsesIncorrect = new List<string>();
            configsReadApply();
            data = new DataLayer();
            if (!respondToCorrectWithSpeech)
            {
                
                playerCorrect = new SoundPlayer();
                playerCorrect.SoundLocation = responseSoundCorrect;
                playerCorrect.LoadAsync();
            }
            if (!respondToIncorrectWithSpeech)
            {
                playerIncorrect = new SoundPlayer();
                playerIncorrect.SoundLocation = responseSoundIncorrect;
                playerIncorrect.LoadAsync();
            }

            nextCard();
        }
        private void configsReadApply()
        {
            bool bValue = false;
            int iValue;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count > 0)
                {
                    foreach (string key in appSettings.AllKeys)
                    {
                        switch (key)
                        {
                            case "voice":
                                string value = appSettings[key];
                                tts.SetVoice(value);
                                toolStripDropDownButtonVoices.Text = tts.GetCurrentVoice();
                                break;
                            case "rate":
                                int rate;
                                if (!Int32.TryParse(appSettings[key], out rate) || rate < tts.GetMinRate() || rate > tts.GetMaxRate())
                                {
                                    rate = 0;
                                }
                                toolStripLabelSpeedRate.Text = rate.ToString();
                                tts.SetRate(rate);
                                break;
                            case "responsesCorrect":
                                responsesCorrect.AddRange(appSettings[key].Split(',')); //ConfigManager saves multi-values as a csv
                                break;
                            case "responsesIncorrect":
                                responsesIncorrect.AddRange(appSettings[key].Split(',')); //ConfigManager saves multi-values as a csv
                                break;
                            case "respondToCorrectWithSpeech":
                                if(bool.TryParse(appSettings[key], out bValue))
                                {
                                    respondToCorrectWithSpeech = bValue;
                                }
                                break;
                            case "respondToIncorrectWithSpeech":
                                if (bool.TryParse(appSettings[key], out bValue))
                                {
                                    respondToIncorrectWithSpeech = bValue;
                                }
                                break;
                            case "requiredWordAttempts":
                                if(int.TryParse(appSettings[key], out iValue))
                                {
                                    requiredWordAttempts = iValue;
                                }
                                break;
                            case "responseSoundCorrect":
                                if (File.Exists(appSettings[key]))
                                {
                                    responseSoundCorrect = appSettings[key];
                                }
                                break;
                            case "responseSoundIncorrect":
                                if (File.Exists(appSettings[key]))
                                {
                                    responseSoundIncorrect = appSettings[key];
                                }
                                break;
                        }
                    }
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void configsSave()
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
                configSettingSetGivenValue(settings, "requiredWordAttempts", requiredWordAttempts);
                configSettingSetGivenValue(settings, "voice", tts.GetCurrentVoice(true));
                configSettingSetGivenValue(settings, "rate", toolStripLabelSpeedRate.Text);
                configSettingSetGivenValue(settings, "respondToCorrectWithSpeech", respondToCorrectWithSpeech);
                configSettingSetGivenValue(settings, "respondToIncorrectWithSpeech", respondToIncorrectWithSpeech);
                configSettingSetGivenMultipleValues(settings, "responsesCorrect", responsesCorrect);
                configSettingSetGivenMultipleValues(settings, "responsesIncorrect", responsesIncorrect);
                configSettingSetGivenValue(settings, "responseSoundCorrect", responseSoundCorrect);
                configSettingSetGivenValue(settings, "responseSoundIncorrect", responseSoundIncorrect);
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("Error writing app settings: " + ex.Message);
            }
        }

        private void configSettingSetGivenValue(KeyValueConfigurationCollection settings, string key, string value)
        {
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
        }

        private void configSettingSetGivenValue(KeyValueConfigurationCollection settings, string key, int iValue)
        {
            configSettingSetGivenValue(settings, key, iValue.ToString());
        }

        private void configSettingSetGivenValue(KeyValueConfigurationCollection settings, string key, bool bValue)
        {
            configSettingSetGivenValue(settings, key, bValue.ToString());
        }

        private void configSettingSetGivenMultipleValues(KeyValueConfigurationCollection settings, string key, List<string> values)
        {
            //settings[key].Value = null; 
            settings.Remove(key);
            foreach (string value in values)
            {
                settings.Add(key, value);
            }
        }

        private void initializeToolBarComponents()
        {
            foreach (string voice in tts.GetVoices())
            {
                toolStripDropDownButtonVoices.DropDown.Items.Add(voice);
            }
            toolStripDropDownButtonVoices.Text = tts.GetCurrentVoice();
        }
        private void toolStripDropDownButtonVoices_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs clicked)
        {
            string selected = clicked.ClickedItem.Text;

            tts.SetVoice(selected);
            ((ToolStripDropDownButton)sender).Text = selected;
        }

        private void setRate(int addend)
        {
            int rate = Int32.Parse(toolStripLabelSpeedRate.Text) + addend;
            toolStripLabelSpeedRate.Text = rate.ToString();
            if (rate >= 10)
            {
                toolStripButtonSpeedFaster.Enabled = false;
                toolStripLabelSpeedRate.ForeColor = Color.Red;
            }
            else if (rate <= -10)
            {
                toolStripButtonSpeedSlower.Enabled = false;
                toolStripLabelSpeedRate.ForeColor = Color.Red;
            }
            else
            {
                toolStripLabelSpeedRate.ForeColor = Color.Black;
                toolStripButtonSpeedFaster.Enabled = true;
                toolStripButtonSpeedSlower.Enabled = true;
            }
            tts.AdjustRate(addend);
        }

        private void toolStripButtonSpeedSlower_Click(object sender, EventArgs e)
        {
            setRate(-1);
        }

        private void toolStripButtonSpeedFaster_Click(object sender, EventArgs e)
        {
            setRate(+1);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            configsSave();
            if (tts != null)
            {
                tts.Dispose();
            }
            if(playerCorrect != null)
            {
                playerCorrect.Stop();
                playerCorrect.Dispose();
                playerCorrect = null;
            }
            if (playerIncorrect != null)
            {
                playerIncorrect.Stop();
                playerIncorrect.Dispose();
                playerIncorrect = null;
            }

        }

        private void toolStripButtonModifyWords_Click(object sender, EventArgs e)
        {
            FormDataMaintenance fdm = new FormDataMaintenance(data);
            fdm.FormClosed += returnFromMaintenance;
            fdm.Show();
            this.Hide();
        }

        private void returnFromMaintenance(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void toolStripButtonReport_Click(object sender, EventArgs e)
        {
            //open reporting dialog
        }

        private void labelFlashCardWord_TextChanged(object sender, EventArgs e)
        {
//NOTE: we may want to give some prompt like "the next word is..." because the success prompts may be confusing due to same voice
            tts.Speak(((Label)sender).Text);
        }

        private void buttonReplay_Click(object sender, EventArgs e)
        {
            tts.Speak(labelFlashCardWord.Text);
        }

        private void textBoxWordEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                textBoxWordEntry.Text = textBoxWordEntry.Text.Trim();
                if (textBoxWordEntry.Text.Trim().Equals(labelFlashCardWord.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    handleCorrect();
                }
                else
                {
                    handleIncorrect();
                }
            }
        }

        private void handleCorrect()
        {
            lastResponseCorrect = true;
            if (respondToCorrectWithSpeech)
            {
                string response = defaultResponseCorrect;
                if (responsesCorrect.Count > 0)
                {
                    response = responsesCorrect[new Random().Next(0, responsesCorrect.Count)];
                }
                tts.Speak(response);
            }
            else
            {
                //play sound wav this.responseSoundCorrect
                if (playerCorrect != null)
                {
                    playerCorrect.PlaySync();
                }
            }

//disabled until we are pulling the list from the datalayer rather than test data above, will cause foreign key violation otherwise
            //data.setResponseResult(labelFlashCardWord.Text, true);
            nextCard();
        }

        private void handleIncorrect()
        {
            lastResponseCorrect = false;
            if (respondToIncorrectWithSpeech)
            {
                string response = defaultResponseIncorrect;
                if (responsesIncorrect.Count > 0)
                {
                    int index = new Random().Next(0, responsesIncorrect.Count);
                    response = responsesIncorrect[index];
                }
                tts.Speak(response);
            }
            else
            {
                //play sound wav this.responseSoundIncorrect
                if(playerIncorrect != null)
                {
                    playerIncorrect.PlaySync();
                }
            }
            //hide the textBoxWordEntry, revealing the word, and display next button
            adjustControlsForIncorrect(true);
            //disabled until we are pulling the list from the datalayer rather than test data above, will cause foreign key violation otherwise
            //data.setResponseResult(labelFlashCardWord.Text, false);
        }

        private void adjustControlsForIncorrect(bool showButton)
        {
            buttonNextWord.Visible = showButton;
            buttonNextWord.Enabled = showButton;
            textBoxWordEntry.Visible = !showButton;
            textBoxWordEntry.Enabled = !showButton;
            if(showButton)
            {
                buttonNextWord.Focus();
            }
        }
        private void nextCard()
        {
            textBoxWordEntry.Text = string.Empty;

            if (!lastResponseCorrect)
            {//add the word to the end of the list to try again at the end
                words.Add(labelFlashCardWord.Text.Trim());
            }

            //change the value of labelFlashCardWord.Text if not last word
            int lastIndex = words.LastIndexOf(labelFlashCardWord.Text.Trim());
            if (lastIndex >= 0 && lastIndex < words.Count - 1) //between first item in list and item before the last OR
            {
                labelFlashCardWord.Text = words[lastIndex + 1];
            }
            else if(lastIndex == words.Count-1 && lastResponseCorrect) //if the last item and correct
            {//hooray, succes!
                finishedFlashCards();
            }
            else if (lastIndex == words.Count - 1 && !lastResponseCorrect) //last item, but misspelled the word
            { 
                labelFlashCardWord.Text += " ";
            }
            else //before the first word in list, lastIndex will be negative
            {
                labelFlashCardWord.Text = words[0];
            }
            textBoxWordEntry.Focus();
        }

        private void buttonNextWord_Click(object sender, EventArgs e)
        {
            adjustControlsForIncorrect(false);
            //nextCard needs to be called before adding the word to the end as
            //it uses the last index of the word in the list to find next word
            nextCard();
            
        }


        private void finishedFlashCards()
        {
            labelFlashCardWord.Text = defaultResponseFinished;
            labelFlashCardWord.ForeColor = Color.White;
            labelFlashCardWord.BackColor = Color.Green;
            textBoxWordEntry.Visible = false;
            textBoxWordEntry.Enabled = false;

            int firstTryCorrect = 0;
            foreach (string word in words)
            {
                if (words.IndexOf(word) == words.LastIndexOf(word))
                {
                    firstTryCorrect++;
                }
            }

            tts.Speak(String.Format(defaultResponseFinishedResults, firstTryCorrect.ToString()));
        }

    }
}
