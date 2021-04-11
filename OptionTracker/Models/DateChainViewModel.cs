using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OptionTracker.Models
{
    public class DateChainViewModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string ExpDate { get; set; }
        public decimal[] Strikes { get; set; }
    }
}