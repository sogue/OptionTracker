using System;
using System.Collections.Generic;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models
{
    public class Ticker
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public int MarketCap { get; set; }
        public DateTime NextEarnings { get; set; }
        public float ClosePrice { get; set; }
        public HistoricalChain Chain { get; set; }
        public ICollection<Trader> Traders { get; set; } = new List<Trader>();
    }
}
