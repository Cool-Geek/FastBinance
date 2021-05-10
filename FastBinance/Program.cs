using FastBinance.BinanceApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBinance
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            BinanceClient client = new BinanceClient("aFcKiRkCD0g9CmtsEQdach6kKR1N7wtAGu2qRHfEaczVGFjlcL1531KvHrGSYvSp", "MfmVBSgIT54oTWfUyVzQHbhkhHdHual1HPUWAPMgsaBERvKxKUI5KkaB3Yby7DQT");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var order = await client.GetSymbolMarketDepth("BTCUSDT", 10);



            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}
