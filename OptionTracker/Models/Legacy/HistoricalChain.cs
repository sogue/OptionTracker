using System.Collections.Generic;

namespace Core.Entities.Legacy
{
    public class HistoricalChain : BaseEntity
    {
        public int TickerId { get; set; }
        public string TickerName { get; set; }
        public ICollection<HistoricalDate> Dates { get; set; } =
        new List<HistoricalDate>();

    }
}