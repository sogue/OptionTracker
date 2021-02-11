using System.Collections.Generic;

namespace OptionTracker.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public ICollection<string> TickerList = new List<string>();
    }
}
