//using FastBinance.BinanceApi;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FastBinance
//{
//    class Program
//    {
//        public async static Task Main(string[] args)
//        {
//            BinanceClient client = new BinanceClient("namRJx8du6Uq9nl1UhnhAZLGCncrdpGIIET7Np5eAfCbTeblJvzvG6SEKDoQS1HZ", "XvJG3ZkM8eIcpcLR3y5DThnPkOnNn0xWoq6DXNbq7aMKvNTv7R4orRiDt8Dg91OV");
//            var watch = System.Diagnostics.Stopwatch.StartNew();

//            //var order = (await client.SetOrder("TRXUSDT", OrderSide.BUY, OrderTypes.LIMIT, 200, 0.16067));

//            var balances = (await client.GetAccountInformation());


//            watch.Stop();            
//            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
//            Console.ReadKey();
//        }
//    }
//}
