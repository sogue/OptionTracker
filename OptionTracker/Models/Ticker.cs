using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models
{
    [DataContract]
    public class Ticker
    {
        public string AssetType { get; set; }

        public int Id { get; set; }
        [DataMember(Name = "symbol", EmitDefaultValue = false)]
        public string Symbol { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Market Cap")]
        [DataMember(Name = "marketCap", EmitDefaultValue = false)]
        public int MarketCap { get; set; }

        public int LastOptionVolume { get; set; }
        public DateTime NextEarnings { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Close Price")]
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public float ClosePrice { get; set; }
        public HistoricalChain Chain { get; set; }
        public ICollection<Trader> Traders { get; set; } = new List<Trader>();

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal OptionVolumeChange { get; set; }
    }
}
