using System.Collections.Generic;

namespace Core.Entities.Legacy
{
    public class Watchlist : BaseEntity
    {
        public ICollection<Ticker> TickerList = new List<Ticker>();
    }
}
