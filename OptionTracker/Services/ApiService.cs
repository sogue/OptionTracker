using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace OptionTracker.Services
{
    public class ApiService : IApiService
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("ApiKey") ?? "HUG3TYZLIYML6S1FDFEJC0RZF7GPAMVG";
        public Task<JsonDocument> GetContractsByTickerName(string ticker)
        {
            string url =
                "https://api.tdameritrade.com/v1/marketdata/chains?apikey="
                + _apiKey
                + "&symbol="
                + ticker
                + "&optionType=S";

            HttpClient client = new HttpClient();

            var response = client.GetFromJsonAsync<JsonDocument>(url);

            return response;

        }
    }


}
