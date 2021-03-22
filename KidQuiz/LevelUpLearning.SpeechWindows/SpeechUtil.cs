using System.Speech.Synthesis;

namespace LevelUpLearning.SpeechWindows
{
    public class SpeechUtil
    {
        private static SpeechSynthesizer _synth;
        private static SpeechSynthesizer Synth
        {
            get
            {
                if (_synth == null)
                {
                    _synth = new SpeechSynthesizer();
                    _synth.SetOutputToDefaultAudioDevice();
                    _synth.Rate = 0;// -5;
                    _synth.SelectVoice("Microsoft Zira Desktop");
                }
                return _synth;
            }
        }

        private readonly static string ssmlFormatString = @"<break strength=""x-weak"" /><prosody rate=""slow"" pitch=""x-low""><emphasis level=""strong"">{0}</emphasis></prosody><break strength=""x-weak"" />";
        
        public static void PromptToSpellWord(string word, string exampleSentence)
        {
            var wordWithSsml = string.Format(ssmlFormatString, word);
            exampleSentence = exampleSentence.Replace(word, wordWithSsml);
            var prompt = string.Format("Spell {0}.  {1}.", wordWithSsml, exampleSentence.Replace(word, wordWithSsml));
            var pb = new PromptBuilder();
            pb.AppendSsmlMarkup(prompt);

            Synth.SpeakAsync(pb); //TODO: Can I not pass the string here?  It's been a while and my old code wasn't documented
        }

        public static void ExplainMisspelling(string correctWord, string incorrectSpelling)
        {
            string msg = string.Format("The word was {0}.  You spelled {1}.",
                                       string.Format(ssmlFormatString, correctWord),
                                       string.Format(ssmlFormatString, incorrectSpelling));

            var x = new PromptBuilder();
            x.AppendSsmlMarkup(msg);

            Synth.SpeakAsyncCancelAll();
            Synth.SpeakAsync(x);
        }
    }
}
