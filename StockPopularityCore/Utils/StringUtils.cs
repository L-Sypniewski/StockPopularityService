using System.Diagnostics.CodeAnalysis;

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
        /// Returns a given string without the first and the last characters.
        /// </summary>
        /// <param name="s">A string to trim</param>
        /// <returns>Given string without the first and the last character</returns>
        /// <remarks>If <paramref name="s" /> has length smaller than or equal to 2 then s is returned</remarks>
        public static string WithoutFirstAndLastCharacter(this string s)
        {
            return s.Length > 2 ? s[1..^1] : s;
        }


        /// <summary>
        /// Counts occurrences of a given char in a string
        /// </summary>
        /// <param name="s">Source string</param>
        /// <param name="ch">Char which occurrences count will be returned</param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        public static int CharOccurrences(this string s, char ch)
        {
            // Method taken from: https://stackoverflow.com/a/541994/8297552. LINQ solution is less performant
            var count = 0;
            foreach (var c in s)
                if (c == ch)
                    count++;
            return count;
        }
    }
}