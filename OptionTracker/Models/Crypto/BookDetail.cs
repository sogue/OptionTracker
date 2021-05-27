using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models.Crypto
{



    public class BookDetail
    {
        public int Id { get; set; }

        public DateTime RequestTime { get; set; }
        public decimal? underlying_price { get; set; }
        public string underlying_index { get; set; }
        public long timestamp { get; set; }
        public Stats stats { get; set; }
        public string state { get; set; }
        public decimal? settlement_price { get; set; }
        public decimal? open_interest { get; set; }
        public decimal? min_price { get; set; }
        public decimal? max_price { get; set; }
        public decimal? mark_price { get; set; }
        public decimal? mark_iv { get; set; }
        public decimal? last_price { get; set; }
        public decimal? interest_rate { get; set; }
        public string instrument_name { get; set; }
        public decimal? index_price { get; set; }
        public Greeks greeks { get; set; }
        public decimal? estimated_delivery_price { get; set; }
        public decimal? bid_iv { get; set; }
        public decimal? best_bid_price { get; set; }
        public decimal? best_bid_amount { get; set; }
        public decimal? best_ask_price { get; set; }
        public decimal? best_ask_amount { get; set; }
        public decimal? ask_iv { get; set; }
    }

    public class Stats
    {
        public int Id { get; set; }
        public decimal? volume { get; set; }
        public decimal? price_change { get; set; }
        public decimal? low { get; set; }
        public decimal? high { get; set; }
    }

    public class Greeks
    {
        public int Id { get; set; }
        public decimal? vega { get; set; }
        public decimal? theta { get; set; }
        public decimal? rho { get; set; }
        public decimal? gamma { get; set; }
        public decimal? delta { get; set; }
    }
}