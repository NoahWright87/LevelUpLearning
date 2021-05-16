using System.Speech.Synthesis;
using LevelUpLearning.Core;

namespace LevelUpLearning.SpeechWindows
{
    /// <summary>
    /// Utility for generating speech using Windows built-in speech synthesis
    /// </summary>
    public class SpeechUtil
    {
        /// <summary>
        /// DO NOT USE DIRECTLY, use Synth property instead
        /// </summary>
        private static SpeechSynthesizer _synth;
        /// <summary>
        /// Getter for SpeechSynthesizer singleton
        /// </summary>
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

        public const string PLACEHOLDER = "%%";

        /// <summary>
        /// Uses Windows speech synthesis to prompt the user to spell the given word using an example sentence.
        /// </summary>
        /// <param name="word">Word to be spelled</param>
        /// <param name="exampleSentence">Sentence that uses the word as an example</param>
        public static void PromptToSpellWord(string word, string exampleSentence)
        {
            //TODO: To prevent accidental replacements, use a placeholder (defined in Constants) instead of the word itself
            //  e.g.: word is use, sentence is "Please USE a napkin becaUSE you don't want to make a mess" 
            //TODO: SSML is also used by other TTS systems, right?  Move some of this to a more generic spot
            string emphasizedWord = GetEmphasizedWord(word);

            exampleSentence = exampleSentence.Replace(PLACEHOLDER, emphasizedWord);
            var prompt = $"Spell {emphasizedWord}.  {exampleSentence}";
            var pb = new PromptBuilder();
            pb.AppendSsmlMarkup(prompt);

            Synth.SpeakAsync(pb); //TODO: Can I not pass the string here?  It's been a while and my old code wasn't documented
        }

        public static void ExplainMisspelling(string correctWord, string incorrectSpelling)
        {
            string msg = $"The word was {GetEmphasizedWord(correctWord)}.  You spelled {GetEmphasizedWord(incorrectSpelling)}.";

            var x = new PromptBuilder();
            x.AppendSsmlMarkup(msg);

            Synth.SpeakAsyncCancelAll();
            //TODO: Again, can I pass the string by itself?  Or is promptbuilder required to use SSML?
            //TODO: DRY: Create a method that does the promptbuilder stuff 
            Synth.SpeakAsync(x);
        }

        //TODO: Turn into a string extension, move to .Core project
        /// <summary>
        /// Appends SSML tags around a word to emphasize it by saying it in a slow, low voice with pauses around it
        /// </summary>
        /// <param name="word">Word to be emphasized</param>
        /// <returns>String containing SSML to be synthesized</returns>
        private static string GetEmphasizedWord(string word)
        {
            return $"{Constants.SSML_BREAK_WEAK}{Constants.SSML_EMPHASIZE_BEGIN}{word}{Constants.SSML_EMPHASIZE_END}{Constants.SSML_BREAK_WEAK}";
        }
    }
}
