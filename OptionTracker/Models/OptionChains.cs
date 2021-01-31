using System;
using System.ComponentModel.DataAnnotations.Schema;
using OptionTracker.Models;

namespace OptionTracker.Models
{
    public class OptionChains
    {
        public int Id { get; set; }

        public int Symbol { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [Column(TypeName = "jsonb")]
        public OptionContract[] OptionContracts { get; set; }

    }
}