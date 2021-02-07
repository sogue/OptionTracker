using System;
using System.Collections.Generic;

namespace OptionTracker.Models
{
    public class ChainResultViewModel
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public DateTime Created { get; set; }

        public TimeSpan TimeChange { get; set; }
        public ICollection<OptionResultViewModel> OptionsResults { get; set; } = new List<OptionResultViewModel>();
    }


}
