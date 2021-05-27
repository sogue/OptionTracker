using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models.Crypto
{
    public class StockOption
    {
        public int Id { get; set; }

        //GME May 28 2021
        public string ContractName { get; set; }

        // May 28 2021 
        public DateTime ExpireTime { get; set; }

        //GME May 28 2021 P C
        public ICollection<StockOptionHistory> StockOptionHistories { get; set; }
    }
}
