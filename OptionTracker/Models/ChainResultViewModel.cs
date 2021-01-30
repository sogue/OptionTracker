using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
