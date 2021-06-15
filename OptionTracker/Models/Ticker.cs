using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Core.Entities;
using FlowService.Models.ChainModels;

namespace OptionTracker.Models
{
    [DataContract]
    public class Ticker
    {

        [DataMember(Name = "assetType", EmitDefaultValue = false)]
        public string AssetType { get; set; }

        public int Id { get; set; }
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Market Cap")]
        [DataMember(Name = "marketCap", EmitDefaultValue = false)]
        public int MarketCap { get; set; }
        [DataMember(Name = "lastOptionVolume", EmitDefaultValue = false)]
        public int LastOptionVolume { get; set; }
        public DateTime NextEarnings { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Close Price")]
        [DataMember(Name = "closePrice", EmitDefaultValue = false)]
        public decimal ClosePrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public decimal Price { get; set; }
        public HistoricalChain Chain { get; set; }
        public ICollection<Trader> Traders { get; set; } = new List<Trader>();

        [DataMember(Name = "optionVolumeChange", EmitDefaultValue = false)]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal OptionVolumeChange { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
   
    }

 
}
