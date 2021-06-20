using System.Collections.Generic;
using OptionTracker.Models;

namespace Core.Entities.Legacy
{
    public class Trader : BaseEntity
    {
        public string IdentityUserId { get; set; }
        public ICollection<TickerSymbol> Tickers { get; set; }
    }
}
