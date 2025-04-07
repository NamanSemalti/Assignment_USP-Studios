namespace USPDigital
{
    /// <summary>
    /// Holds static constants used throughout the application.
    /// </summary>
    public static class StaticDataHolder
    {
        #region Warnings & Infos

        // Warning shown when the user inputs whitespace in the word
        public const string WhiteSpaceWarning = "Please remove empty space from the word";

        // Warning shown when the API doesn't return a definition
        public const string DefinitionWarning = "Oops! Definition not found";

        // Warning shown when the API doesn't return an example
        public const string ExampleWarning = "Oops! Example not found";

        #endregion

        #region Animator Parameters

        // Animator boolean key to trigger the "talking" animation
        public const string CharacterTalkAnimationKey = "IsTalking";

        #endregion
    }
}
