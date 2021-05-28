using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OptionTracker.Models
{
    public class TickerOptionHistory
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public Ticker Ticker { get; set; }

    }
}