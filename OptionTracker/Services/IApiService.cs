using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using OptionTracker.Models;

namespace OptionTracker.Services
{
    public interface IApiService
    {
        Task<JsonDocument> GetContractsByTickerName(string ticker);
        IList<OptionResult> CreateResults(IEnumerable<OptionContract> chainsByInterest);
    }
}
