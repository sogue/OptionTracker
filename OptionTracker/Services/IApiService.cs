using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OptionTracker.Models;

namespace OptionTracker.Services
{
    public interface IApiService
    {
        IList<OptionResult> GetChainsByTickerName(string ticker);
    }
}
