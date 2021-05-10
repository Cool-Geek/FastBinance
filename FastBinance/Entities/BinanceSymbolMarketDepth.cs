using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBinance.Entities
{
    public class BinanceSymbolMarketDepth
    {
        public List<BinanceSymbolOrderBook> Bids { get; set; }
        public List<BinanceSymbolOrderBook> Asks { get; set; }

    }
}
