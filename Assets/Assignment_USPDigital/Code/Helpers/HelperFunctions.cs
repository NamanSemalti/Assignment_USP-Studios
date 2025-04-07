namespace USPDigital
{
    /// <summary>
    /// Contains utility helper functions used across the project.
    /// </summary>
    public static class HelperFunctions
    {
        /// <summary>
        /// Checks whether the input string contains any whitespace characters.
        /// </summary>
        /// <param name="input">The string to check for spaces.</param>
        /// <returns>True if the string contains spaces; otherwise, false.</returns>
        public static bool HasEmptySpace(string input)
        {
            return input.Contains(" ");
        }
    }
}
