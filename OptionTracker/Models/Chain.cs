using System;

namespace OptionTracker.Models
{
    public class Chain
    {
        public string Symbol { get; set; }
        public decimal UnderlyingPrice { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public OptionContract[] OptionContracts { get; set; }
    }
}