using System.Collections.Generic;

namespace Core.Entities.Legacy
{
    public class Trader : BaseEntity
    {
        public string IdentityUserId { get; set; }
        public ICollection<Ticker> Tickers { get; set; }
    }
}
