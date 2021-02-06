using System;
using System.ComponentModel.DataAnnotations.Schema;
using OptionTracker.Models;

namespace OptionTracker.Models
{
    public class OptionChain
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal UnderlyingPrice { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [Column(TypeName = "jsonb")]
        public OptionContract[] OptionContracts { get; set; }

    }
}