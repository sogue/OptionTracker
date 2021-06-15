using System;
using System.Collections.Generic;
using OptionTracker.Models;

namespace Core.Entities.Legacy
{
    public class Ticker : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string AssetType { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public int MarketCap { get; set; }
        public decimal ClosePrice { get; set; }

        public int LastOptionVolume { get; set; }
        public decimal OptionVolumeChange { get; set; }
  
        public HistoricalChain Chain { get; set; }
        public DateTime NextEarnings { get; set; }
        
        public ICollection<Trader> Traders { get; set; } = new List<Trader>();
       
        public TickerType TickerType { get; set; }
        public TickerSector TickerSector { get; set; }
       
        public int TickerSectorId { get; set; }
        public int TickerTypeId { get; set; }
        
    }
}