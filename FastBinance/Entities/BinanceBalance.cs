namespace FastBinance.Entities
{
    public class BinanceBalance
    {
        public string Asset { get; set; }
        public double Free { get; set; }
        public double Locked { get; set; }
    }
}