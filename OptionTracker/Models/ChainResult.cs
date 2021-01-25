using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models
{
    public class ChainResult
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<OptionResult> OptionsResults { get; set; }
    }

    
}
