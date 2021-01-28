namespace StockPopularityCore.Model
{
    public readonly struct StockName
    {
        public string? LongName { get; }
        public string Codename { get; }


        public StockName(string codename, string? longName = null)
        {
            LongName = longName;
            Codename = codename;
        }


        public override string ToString() => $"LongName: {LongName}, Codename: {Codename}";
    }
}