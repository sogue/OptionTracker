using OptionTracker.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace OptionTracker.Services
{
    public interface IApiService
    {
        Task<JsonDocument> GetContractsByTickerName(string ticker);
    }
}
