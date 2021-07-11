using System.Speech.Synthesis;
using LevelUpLearning.Core;
using LevelUpLearning.Core.Data;

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

        private readonly static string[] CONGRATS_PHRASES = new string[]
        {
            "You did it!",
            "Good job!",
            "Congrats!",
            "That's right!",
            "Wow",
            "Hooray",
            "Way to go",
            "Super",
            "Fantastic",
            "Correct!",
            "Whoa",
            "Amazing!",
            "Great job!",
            "You're a genius!",
            "Another one!",
            "You're the best!",
            "I'm in awe",
            "Incredible",
            "Couldn't have done better myself"
        };

        public static void ShutUpAndSay(string words)
        {
            ShutUp();
            Synth.SpeakAsync(GetSanitized(words));
        }

        public static void Congrats()
        {
            ShutUpAndSay(CONGRATS_PHRASES[DataController.Random.Next(CONGRATS_PHRASES.Length)]);
        }

        /// <summary>
        /// Uses Windows speech synthesis to prompt the user to spelling the given SpellingWord
        /// using its Prompt.
        /// </summary>
        /// <param name="word"></param>
        public static void PromptToSpellWord(SpellingWord word)
        {
            PromptToSpellWord(word.Word, word.Prompt);
        }

        /// <summary>
        /// Uses Windows speech synthesis to prompt the user to spell the given word using an example sentence.
        /// </summary>
        /// <param name="word">Word to be spelled</param>
        /// <param name="exampleSentence">Sentence that uses the word as an example</param>
        public static void PromptToSpellWord(string word, string exampleSentence)
        {
            //TODO: SSML is also used by other TTS systems, right?  Move some of this to a more generic spot
            string emphasizedWord = GetEmphasizedWord(word);

            exampleSentence = exampleSentence.Replace(PLACEHOLDER, emphasizedWord);
            var prompt = $"{emphasizedWord}.  {exampleSentence}";
            var pb = new PromptBuilder();
            pb.AppendSsmlMarkup(GetSanitized(prompt));

            ShutUp();
            Synth.SpeakAsync(pb); //TODO: Can I not pass the string here?  It's been a while and my old code wasn't documented
        }

        public static void ExplainMisspelling(string correctWord, string incorrectSpelling)
        {
            string msg = $"The word was {GetEmphasizedWord(correctWord)}.  You spelled {GetEmphasizedWord(incorrectSpelling)}.";

            var x = new PromptBuilder();
            x.AppendSsmlMarkup(GetSanitized(msg));

            ShutUp();
            //TODO: Again, can I pass the string by itself?  Or is promptbuilder required to use SSML?
            //TODO: DRY: Create a method that does the promptbuilder stuff 
            Synth.SpeakAsync(x);
        }

        public static void ShutUp()
        {
            Synth.SpeakAsyncCancelAll();
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

        /// <summary>
        /// This removes any special characters that keep the speech from properly generating.
        /// </summary>
        /// <param name="speech"></param>
        /// <returns></returns>
        private static string GetSanitized(string speech)
        {
            return speech.Replace("&", " and ");
        }
    }
}
