namespace Core.Model
{
    public readonly struct StockName
    {
        public string? LongName { get; }
        public string Codename { get; }


        public StockName(string codename, string? longName = null)
        {
            LongName = longName?.Trim();
            Codename = codename.Trim();
        }


        public override string ToString() => $"LongName: {LongName}, Codename: {Codename}";
    }
}