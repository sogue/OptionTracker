using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Core.Entities;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models
{
    public class TickerSymbol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string AssetType { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public int MarketCap { get; set; }
        public decimal ClosePrice { get; set; }

        public int LastOptionVolume { get; set; }
        public decimal OptionVolumeChange { get; set; }

     //   public HistoricalChain Chain { get; set; }
        public DateTime NextEarnings { get; set; }

        public ICollection<Trader> Traders { get; set; } = new List<Trader>();

         public TickerType TickerType { get; set; }
         public TickerSector TickerSector { get; set; }

         public int TickerSectorId { get; set; }
         public int TickerTypeId { get; set; }

    }

 
}
