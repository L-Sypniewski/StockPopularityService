namespace StockPopularityCore.Model
{
    public readonly struct StockName
    {
        public readonly string? LongName { get; }
        public readonly string Codename { get; }


        public StockName(string? longName, string codename)
        {
            LongName = longName;
            Codename = codename;
        }


        public override string ToString() => $"LongName: {LongName}, Codename: {Codename}";
    }
}