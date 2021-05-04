using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBinance.Entities
{
    public class BinanceAccountInformation
    {
        public short MakerCommission { get; set; }
        public short TakerCommission { get; set; }
        public short BuyerCommission { get; set; }
        public short SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public long UpdateTime { get; set; }
        public string AccountType{ get; set; }
        public List<BinanceBalance> Balances{ get; set; }
        public List<string> Permissions { get; set; }
    }
}
