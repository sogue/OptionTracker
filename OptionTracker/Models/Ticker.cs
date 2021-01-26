using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public ICollection<Ticker> TickerList = new List<Ticker>();
    }
}
