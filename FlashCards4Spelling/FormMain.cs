using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCards4Spelling
{
    public partial class FormMain : Form
    {
        TTSInterface tts;

        public FormMain()
        {
            InitializeComponent();
            tts = new TTSInterface();
            initializeToolBarComponents();
            configsReadApply();
        }
        private void configsReadApply()
        {
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
                configSettingSetVoice(settings);
                configSettingSetRate(settings);
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("Error writing app settings: " + ex.Message);
            }
        }

        private void configSettingSetVoice(KeyValueConfigurationCollection settings)
        {
            string key = "voice";
            string value = tts.GetCurrentVoice(true);
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
        }
        private void configSettingSetRate(KeyValueConfigurationCollection settings)
        {
            string key = "rate";
            string value = toolStripLabelSpeedRate.Text;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
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
        }

        private void toolStripButtonModifyWords_Click(object sender, EventArgs e)
        {
            //open configuration dialog
        }

        private void toolStripButtonReport_Click(object sender, EventArgs e)
        {
            //open reporting dialog
        }
    }
}
