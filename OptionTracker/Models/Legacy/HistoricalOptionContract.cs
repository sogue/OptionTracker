using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Legacy
{
    public class HistoricalOptionContract : BaseEntity
    {
        public int HistoricalDateId { get; set; }
        public string TickerName { get; set; }
        public ICollection<OptionContract> Contracts { get; set; }

    }
}