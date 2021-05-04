
using FastBinance.Entities;
using FastBinance.Security;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastBinance.BinanceApi
{
    public class BinanceClient
    {
        private readonly string _mainUrl = "https://api.binance.com";
        private readonly string _apiKey;
        private readonly string _secretKey;

        public BinanceClient(string apiKey, string secretKey)
        {
            _apiKey = apiKey;
            _secretKey = secretKey;
        }

        public async Task<BinanceAccountInformation> GetAccountInformation()
        {
            string args = $"recvWindow=6000&timestamp={GetUnixTimestamp()}";
            var response = await GetDataAsync("/api/v3/account", true, RequestMethod.Get, args);
            var jsonObject = JObject.Parse(response);
            BinanceAccountInformation information = (BinanceAccountInformation)jsonObject.ToObject(typeof(BinanceAccountInformation));
            return information;
        }

        public async Task<List<BinanceSymbol>> GetAllSymbolsAsync()
        {
            List<BinanceSymbol> pairs = new List<BinanceSymbol>();
            var response = await GetDataAsync("/api/v3/exchangeInfo", false,RequestMethod.Get);
            var jsonObject = JObject.Parse(response);
            var jsonToken = jsonObject.GetValue("symbols");
            
            foreach (var token in jsonToken)
            {
                pairs.Add((BinanceSymbol)token.ToObject(typeof(BinanceSymbol)));
            }
            return pairs;
        }

        public async Task<BinanceSymbol24HrStatistic> GetSymbol24hrTickerPriceAsync(string symbol)
        {
            string args = $"symbol={symbol}";
            var response = await GetDataAsync("/api/v3/ticker/24hr", false, RequestMethod.Get ,args);
            var jsonObject = JObject.Parse(response);
            BinanceSymbol24HrStatistic result = (BinanceSymbol24HrStatistic)jsonObject.ToObject(typeof(BinanceSymbol24HrStatistic));
            return result;
        }

        public async Task<List<BinanceSymbol24HrStatistic>> GetSymbols24hrTickerPriceAsync()
        {
            var response = await GetDataAsync("/api/v3/ticker/24hr", false, RequestMethod.Get);
            var jsonObject = JArray.Parse(response);
            List<BinanceSymbol24HrStatistic> result = (List<BinanceSymbol24HrStatistic>)jsonObject.ToObject(typeof(List<BinanceSymbol24HrStatistic>));
            return result;
        }

        public async Task<BinanceSymbolPrice> GetSymbolPriceAsync(string symbol)
        {
            string args = $"symbol={symbol}";
            var response = await GetDataAsync("/api/v3/ticker/price", false, RequestMethod.Get, args);
            var jsonObject = JObject.Parse(response);
            BinanceSymbolPrice result = (BinanceSymbolPrice)jsonObject.ToObject(typeof(BinanceSymbolPrice));
            return result;
        }

        public async Task<List<BinanceSymbolPrice>> GetSymbolsPriceAsync()
        {
            var response = await GetDataAsync("/api/v3/ticker/24hr", false, RequestMethod.Get);
            var jsonObject = JArray.Parse(response);
            List<BinanceSymbolPrice> result = (List<BinanceSymbolPrice>)jsonObject.ToObject(typeof(List<BinanceSymbolPrice>));
            return result;
        }

        public async Task<string> SetOrder(string symbol, OrderSide side, OrderTypes type, double quantity, double price, string timeInForce = "GTC", short recWindow = 5000)
        {
            string args = $"symbol={symbol}&side={side.ToString()}&type={type.ToString()}&timeInForce={timeInForce}&quantity={quantity}&price={price}&recvWindow={recWindow}&timestamp={GetUnixTimestamp()}";
            var response = await GetDataAsync("/api/v3/order", true, RequestMethod.Post, args);
            return response;
        }

        public bool TestConnectivity()
        {
            var response = GetData("/api/v3/ping", false);
            if (response == "")
            {
                return false;
            }
            return true;
        }

        public async Task<bool> TestConnectivityAsync()
        {
            var response = await GetDataAsync("/api/v3/ping", false, RequestMethod.Get);
            if (response == "")
            {
                return false;
            }
            return true;
        }

        #region

        private string GetUnixTimestamp()
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return milliseconds.ToString();
        }

        private string GetData(string restOfUrl, bool hMacSha256Enabled, string args = null)
        {
            string url = _mainUrl + restOfUrl;
            if (args != null)
            {
                if (hMacSha256Enabled)
                {
                    var signature = args.CreateHMAC256Signature(_secretKey);
                    url += $"?{args}&signature={signature}";
                }
                else
                {
                    url += $"?{args}";
                }
            }
            var client = new RestClient(url);
            client.AddDefaultHeader("X-MBX-APIKEY", _apiKey);
            var response = client.Execute(new RestRequest());
            return response.Content;
        }

        private async Task<string> GetDataAsync(string restOfUrl, bool hMacSha256Enabled, RequestMethod method, string args = null)
        {
            string url = _mainUrl + restOfUrl;
            if (args != null)
            {
                if (hMacSha256Enabled)
                {
                    var signature = args.CreateHMAC256Signature(_secretKey);
                    url += $"?{args}&signature={signature}";
                }
                else
                {
                    url += $"?{args}";
                }
            }
            var client = new RestClient(url);
            client.AddDefaultHeader("X-MBX-APIKEY", _apiKey);
            IRestResponse response;
            if(method == RequestMethod.Get)
            {
                response = await client.ExecuteAsync(new RestRequest());
            }           
            else if(method == RequestMethod.Post)
            {
                response = await client.ExecutePostAsync(new RestRequest());
            }
            else
            {
                response = await client.ExecuteAsync(new RestRequest());
            }
            return response.Content;
        }

        #endregion

    }
}
