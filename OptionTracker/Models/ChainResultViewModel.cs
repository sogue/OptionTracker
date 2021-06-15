using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OptionTracker.Models
{
    public class ChainResultViewModel
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public DateTime Created { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Close Price")]
        public decimal ClosePrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Market Cap")]
        public int MarketCap { get; set; }
        public TimeSpan TimeChange { get; set; }
        public ICollection<OptionResultViewModel> OptionsResults { get; set; } = new List<OptionResultViewModel>();
    }


}
