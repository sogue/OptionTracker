using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OptionTracker.Models.Crypto
{


    [DataContract]
    public class BookDetail
    {
        public int Id { get; set; }

        public DateTime RequestTime { get; set; }

        [DataMember(Name = "underlying_price", EmitDefaultValue = false)]
        public decimal? UnderlyingPrice { get; set; }

        [DataMember(Name = "underlying_index", EmitDefaultValue = false)]
        public string UnderlyingIndex { get; set; }

        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        public long Timestamp { get; set; }

        [DataMember(Name = "stats", EmitDefaultValue = false)]
        public Stats Stats { get; set; }

        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(Name = "settlement_price", EmitDefaultValue = false)]
        public decimal? SettlementPrice { get; set; }
        [DataMember(Name = "open_interest", EmitDefaultValue = false)]
        public decimal? OpenInterest { get; set; }
        [DataMember(Name = "min_price", EmitDefaultValue = false)]
        public decimal? MinPrice { get; set; }
        [DataMember(Name = "max_price", EmitDefaultValue = false)]
        public decimal? MaxPrice { get; set; }
        [DataMember(Name = "mark_price", EmitDefaultValue = false)]
        public decimal? MarkPrice { get; set; }
        [DataMember(Name = "mark_iv", EmitDefaultValue = false)]
        public decimal? MarkIv { get; set; }
        [DataMember(Name = "last_price", EmitDefaultValue = false)]
        public decimal? LastPrice { get; set; }
        [DataMember(Name = "interest_rate", EmitDefaultValue = false)]
        public decimal? InterestRate { get; set; }
        [DataMember(Name = "instrument_name", EmitDefaultValue = false)]
        public string InstrumentName { get; set; }
        [DataMember(Name = "index_price", EmitDefaultValue = false)]
        public decimal? IndexPrice { get; set; }
        [DataMember(Name = "greeks", EmitDefaultValue = false)]
        public Greeks Greeks { get; set; }
        [DataMember(Name = "estimated_delivery_price", EmitDefaultValue = false)]
        public decimal? EstimatedDeliveryPrice { get; set; }
        [DataMember(Name = "bid_iv", EmitDefaultValue = false)]
        public decimal? BidIv { get; set; }
        [DataMember(Name = "best_bid_price", EmitDefaultValue = false)]
        public decimal? BestBidPrice { get; set; }
        [DataMember(Name = "best_bid_amount", EmitDefaultValue = false)]
        public decimal? BestBidAmount { get; set; }
        [DataMember(Name = "best_ask_price", EmitDefaultValue = false)]
        public decimal? BestAskPrice { get; set; }
        [DataMember(Name = "best_ask_amount", EmitDefaultValue = false)]
        public decimal? BestAskAmount { get; set; }
        [DataMember(Name = "ask_iv", EmitDefaultValue = false)]
        public decimal? AskIv { get; set; }
    }

    public class Stats
    {
        public int Id { get; set; }
        public int? BookDetailId { get; set; }
        public DateTime RequestTime { get; set; }

        public string InstrumentName { get; set; }
        [DataMember(Name = "volume", EmitDefaultValue = false)]
        public decimal? Volume { get; set; }
        [DataMember(Name = "price_change", EmitDefaultValue = false)]
        public decimal? PriceChange { get; set; }
        [DataMember(Name = "low", EmitDefaultValue = false)]
        public decimal? Low { get; set; }
        [DataMember(Name = "high", EmitDefaultValue = false)]
        public decimal? High { get; set; }
    }

    public class Greeks
    {
        public int Id { get; set; }
        public int? BookDetailId { get; set; }
        public DateTime RequestTime { get; set; }

        public string InstrumentName { get; set; }
        [DataMember(Name = "vega", EmitDefaultValue = false)]
        public decimal? Vega { get; set; }
        [DataMember(Name = "theta", EmitDefaultValue = false)]
        public decimal? Theta { get; set; }
        [DataMember(Name = "rho", EmitDefaultValue = false)]
        public decimal? Rho { get; set; }
        [DataMember(Name = "gamma", EmitDefaultValue = false)]
        public decimal? Gamma { get; set; }
        [DataMember(Name = "delta", EmitDefaultValue = false)]
        public decimal? Delta { get; set; }
    }
}