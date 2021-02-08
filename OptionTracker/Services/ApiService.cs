using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OptionTracker.Models;

namespace OptionTracker.Services
{
    public class ApiService : IApiService
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("ApiKey");
        public Task<JsonDocument> GetContractsByTickerName(string ticker)
        {
            string url =
                "https://api.tdameritrade.com/v1/marketdata/chains?apikey="
                + _apiKey
                + "&symbol="
                + ticker
                + "&contractType=CALL&optionType=S";

            HttpClient client = new HttpClient();

            var response = client.GetFromJsonAsync<JsonDocument>(url);
            return response;

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
