namespace StockPopularityCore.Utils
{
    public static class StringUtils
    {
        // TODO: Use NugetPackage with own utils
        public static string WithoutNewLineChars(this string s) => s.Replace("\n", " ")
                                                                    .Replace("\t", " ")
                                                                    .Replace("\r", " ")
                                                                    .Trim();


        /// <summary>
        ///  Returns a given string without the first and the last characters.
        /// </summary>
        /// <param name="s">A string to trim</param>
        /// <returns>Given string without the first and the last character</returns>
        /// <remarks>If <paramref name="s" /> has length smaller than or equal to 2 then s is returned</remarks>
        public static string WithoutFirstAndLastCharacter(this string s)
        {
            return s.Length > 2 ? s[1..^1] : s;
        }
    }
}