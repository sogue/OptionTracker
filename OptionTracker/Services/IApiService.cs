using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptionTracker.Models;

namespace OptionTracker.Services
{
    public interface IApiService
    {
        IEnumerable<OptionContract> GetContractsByTickerName(string ticker);
        IList<OptionResult> CreateResults(IEnumerable<OptionContract> chainsByInterest);
    }
}
