using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards4Spelling
{
    internal class TTSInterface
    {
        protected Dictionary<string, InstalledVoice> voices;
        protected SpeechSynthesizer synth;

        public TTSInterface()
        {
            this.synth = new SpeechSynthesizer();
            voices = new Dictionary<string, InstalledVoice>();

            foreach(InstalledVoice voice in this.synth.GetInstalledVoices())
            {
                voices.Add(cleanVoiceName(voice.VoiceInfo.Name), voice);
            }
        }

        public void AdjustRate(int addend)
        {
            this.synth.Rate += addend;
        }

        public void SetRate(int rate)
        {
            this.synth.Rate = rate;
        }

        public int GetRate()
        {
            return this.synth.Rate;
        }

        public int GetMaxRate() { return 10; }

        public int GetMinRate() { return -10; }

        public void SetVoice(string voice)
        {
            if (voices.ContainsKey(voice))
            {
                this.synth.SelectVoice(voices[voice].VoiceInfo.Name);
            }
            else
            {
                this.synth.SelectVoice(voice);
            }
            
        }

        public string GetCurrentVoice(bool returnTechnicalVoice = false)
        {
            string voice = this.synth.Voice.Name;
            if (!returnTechnicalVoice)
            {
                voice = cleanVoiceName(voice);
            }

            return voice;
        }

        public string[] GetVoices()
        {
            return voices.Keys.ToArray();
        }

        public void Speak(string prompt)
        {
            //this.synth.Speak(prompt);
            this.synth.SpeakAsync(prompt);
        }

        private string cleanVoiceName(string voice)
        {
            return voice.Replace("Microsoft ", string.Empty).Replace(" Desktop", string.Empty);
        }

        internal void Dispose()
        {
            this.synth.Dispose();
        }
        
    }
}
