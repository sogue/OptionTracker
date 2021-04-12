﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OptionTracker.Models
{
    public class Ticker
    {
        public int Id { get; set; }
        public string Symbol { get; set; }

        
        public int MarketCap { get; set; }
        public DateTime NextEarnings { get; set; }
        
        public float ClosePrice { get; set; }
    }
}
