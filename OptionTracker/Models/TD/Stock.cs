using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models.Crypto
{
    public class Stock
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public ICollection<StockOption> StockOptions { get; set; }
    }
}
