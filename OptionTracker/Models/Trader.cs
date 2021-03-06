﻿using System.Collections.Generic;

namespace OptionTracker.Models
{
    public class Trader
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public ICollection<TickerSymbol> Tickers { get; set; }
    }
}
