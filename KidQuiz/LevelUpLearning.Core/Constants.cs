namespace LevelUpLearning.Core
{
    /// <summary>
    /// Class that contains constants used throughout the solution
    /// </summary>
    public static class Constants
    {
        //TODO: Move any hardcoded strings/numbers into here

        public static readonly string SSML_BREAK_WEAK = @"<break strength=""x-weak"" />";
        public static readonly string SSML_EMPHASIZE_BEGIN = @"<prosody rate=""slow"" pitch=""x-low""><emphasis level=""strong"">";
        public static readonly string SSML_EMPHASIZE_END = "</emphasis></prosody>";

    }
}
