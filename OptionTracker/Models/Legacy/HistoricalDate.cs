using System;
using System.Collections.Generic;

namespace Core.Entities.Legacy
{
    public class HistoricalDate : BaseEntity
    {
        public int HistoricalChainId { get; set; }
        public string DateString { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ICollection<HistoricalOptionContract> OptionContracts { get; set; } =
            new List<HistoricalOptionContract>();

    }
}