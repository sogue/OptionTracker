using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models.Crypto
{
    public class StockOptionHistory
    {
        public int Id { get; set; }
        //GME May 28 2021 10 Put
        public string ContractName { get; set; }

        public string Type { get; set; }
        //GME May 28 2021 
        public StockOption StockOption { get; set; }

        //GME May 28 2021 10 Put -  Daily
        public ICollection<OptionContract> OptionContracts { get; set; }
    }
}
