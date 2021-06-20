using System.Collections.Generic;
using OptionTracker.Models;

namespace Core.Entities.Legacy
{
    public class Watchlist : BaseEntity
    {
        public ICollection<TickerSymbol> TickerList = new List<TickerSymbol>();
    }
}
