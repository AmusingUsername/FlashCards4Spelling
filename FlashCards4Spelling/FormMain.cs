using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
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
        bool respondToCorrectWithSpeech = true;
        bool respondToIncorrectWithSpeech = true;
        string responseSoundCorrect = "C:\\Windows\\Media\\tada.wav"; //default value, this can be changed via settings file
        string responseSoundIncorrect = "C:\\Windows\\Media\\ringout.wav"; //default value, this can be changed via settings file
        string defaultResponseCorrect = "Correct!";
        string defaultResponseIncorrect = "That was wrong!";
        SoundPlayer playerCorrect;
        SoundPlayer playerIncorrect;

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
                playerIncorrect.LoadAsync();
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
                configSettingSet_voice(settings);
                configSettingSet_rate(settings);
                configSettingSet_respondToCorrectWithSpeech(settings);
                configSettingSet_respondToIncorrectWithSpeech(settings);
                configSettingSetGivenMultipleValues(settings, "responsesCorrect", responsesCorrect);
                configSettingSetGivenMultipleValues(settings, "responsesIncorrect", responsesIncorrect);
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

        private void configSettingSetGivenMultipleValues(KeyValueConfigurationCollection settings, string key, List<string> values)
        {
            foreach (string value in values)
            {
                settings.Add(key, value);
            }
        }

        private void configSettingSet_voice(KeyValueConfigurationCollection settings)
        {
            configSettingSetGivenValue(settings, "voice", tts.GetCurrentVoice(true));
        }
        private void configSettingSet_rate(KeyValueConfigurationCollection settings)
        {
            configSettingSetGivenValue(settings, "rate", toolStripLabelSpeedRate.Text);
        }

        private void configSettingSet_respondToIncorrectWithSpeech(KeyValueConfigurationCollection settings)
        {
            configSettingSetGivenValue(settings, "respondToIncorrectWithSpeech", respondToIncorrectWithSpeech.ToString());
        }

        private void configSettingSet_respondToCorrectWithSpeech(KeyValueConfigurationCollection settings)
        {
            configSettingSetGivenValue(settings, "respondToCorrectWithSpeech", respondToCorrectWithSpeech.ToString());
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
                if (textBoxWordEntry.Text.Equals(labelFlashCardWord.Text, StringComparison.CurrentCultureIgnoreCase))
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
            if (respondToCorrectWithSpeech)
            {
                string response = defaultResponseCorrect;
                if (responsesCorrect.Count > 0)
                {
                    response = responsesCorrect[new Random().Next(0, responsesCorrect.Count -1)];
                }
                tts.Speak(response);
            }
            else
            {
                //play sound wav this.responseSoundCorrect
                if (playerCorrect != null)
                {
                    playerCorrect.Play();
                }
            }

//disabled until we are pulling the list from the datalayer rather than test data above, will cause foreign key violation otherwise
            //data.setResponseResult(labelFlashCardWord.Text, true);
            nextCard();
        }

        private void handleIncorrect()
        {
            if(respondToIncorrectWithSpeech)
            {
                string response = defaultResponseIncorrect;
                if (responsesIncorrect.Count > 0)
                {
                    response = responsesIncorrect[new Random().Next(0, responsesCorrect.Count -1)];
                }
                tts.Speak(response);
            }
            else
            {
                //play sound wav this.responseSoundIncorrect
                if(playerIncorrect != null)
                {
                    playerIncorrect.Play();
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
            //change the value of labelFlashCardWord.Text
            int index = words.LastIndexOf(labelFlashCardWord.Text);
            if (index >= 0 && index < words.Count - 1)
            {
                labelFlashCardWord.Text = words[index + 1];
            }
            else if(index == words.Count-1)
            {//last word... hooray, succes!
                
            }
            else
            {//initial word
                labelFlashCardWord.Text = words[0];
            }
            textBoxWordEntry.Focus();
        }

        private void buttonNextWord_Click(object sender, EventArgs e)
        {
            string lastWord = labelFlashCardWord.Text;
            adjustControlsForIncorrect(false);
            //nextCard needs to be called before adding the word to the end as
            //it uses the last index of the word in the list to find next word
            nextCard();
            //add the word to the end of the list to try again at the end
            words.Add(lastWord); 
        }


    }
}
