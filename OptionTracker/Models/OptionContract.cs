#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models
{
    public class OptionContract
    {
        public int Id { get; set; }
        public string PutCall { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string ExchangeName { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
        public decimal Mark { get; set; }
        public int BidSize { get; set; }
        public int AskSize { get; set; }
        public string? BidAskSize { get; set; }
        public string? LastSize { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public int TotalVolume { get; set; }
        public string? TradeDate { get; set; }
        public long? TradeTimeInLong { get; set; }
        public long? QuoteTimeInLong { get; set; }
        public decimal NetChange { get; set; }
        public string Volatility { get; set; }
        public string Delta { get; set; }
        public string Gamma { get; set; }
        public string Theta { get; set; }
        public string Vega { get; set; }
        public string Rho { get; set; }
        public int OpenInterest { get; set; }
        public decimal TimeValue { get; set; }
        public string TheoreticalOptionValue { get; set; }
        public string TheoreticalVolatility { get; set; }
        public string? OptionDeliverablesList { get; set; }
        public decimal StrikePrice { get; set; }
        public long ExpirationDate { get; set; }
        public int DaysToExpiration { get; set; }
        public string? ExpirationType { get; set; }
        public long LastTradingDay { get; set; }
        public decimal Multiplier { get; set; }
        public string? SettlementType { get; set; }
        public string? DeliverableNote { get; set; }
        public string? IsIndexOption { get; set; }
        public decimal PercentChange { get; set; }
        public decimal MarkChange { get; set; }
        public decimal MarkPercentChange { get; set; }
        public bool NonStandard { get; set; }
        public bool Mini { get; set; }
        public bool InTheMoney { get; set; }
    }
}
