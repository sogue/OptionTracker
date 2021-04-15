using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptionTracker.Models
{
    public class HistoricalOptionContract
    {
        public int Id { get; set; }
        public string Symbol { get; set; }

        [Column(TypeName = "jsonb")]
        public ICollection<OptionContract> Contracts { get; set; }
        
    }
}