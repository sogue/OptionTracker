using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OptionTracker.Models;

namespace OptionTracker.Services
{
    public class ApiService : IApiService
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("ApiKey");
        public IEnumerable<OptionContract> GetContractsByTickerName(string ticker)
        {
            string url =
                "https://api.tdameritrade.com/v1/marketdata/chains?apikey="
                + _apiKey
                + "&symbol="
                + ticker
                + "&contractType=CALL&range=OTM&optionType=S";

            HttpClient client = new HttpClient();

            Task<HttpResponseMessage> response = client.GetAsync(url);
            JsonDocument myDeserializedClass = JsonDocument.Parse(response.Result.Content.ReadAsByteArrayAsync().Result);
            JsonElement s = myDeserializedClass.RootElement.GetProperty("callExpDateMap");
            Dictionary<string, Dictionary<string, OptionContract[]>> result =
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(s.ToString());

            var chainsByInterest = result.SelectMany(x => x.Value.SelectMany(o => o.Value));
            return chainsByInterest;
        }

        public IList<OptionResult> CreateResults(IEnumerable<OptionContract> chainsByInterest)
        {
            var optionResults = chainsByInterest
                .Select(x => new OptionResult
                {
                    OpenInterest = x.OpenInterest,
                    ClosePrice = x.ClosePrice,
                    Description = x.Description
                }).OrderBy(x => x.OpenInterest);

            return optionResults.ToList();
        }
    }


}
