namespace StockPopularityCore.Utils
{
    public static class StringUtils
    {
        // TODO: Use NugetPackage with own utils
        public static string WithoutNewLineChars(this string s) => s.Replace("\n", " ")
                                                                    .Replace("\t", " ")
                                                                    .Replace("\r", " ")
                                                                    .Trim();
    }
}