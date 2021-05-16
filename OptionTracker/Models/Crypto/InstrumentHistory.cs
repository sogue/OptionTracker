using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models.Crypto
{
    public class InstrumentHistory
    {
        public int Id { get; set; }
        public string InstrumentName { get; set; }
        public Instrument ActualInstrument { get; set; }
        public ICollection<BookSummary> BookSummaries { get; set; }
    }
}
