using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

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