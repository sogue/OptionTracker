using System;
using System.ComponentModel.DataAnnotations;

namespace OptionTracker.Models
{
    public class OptionResultViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ChartCode { get; set; }
        [Display(Name = "Open Interest")]
        public int OpenInterest { get; set; }
        public int OpenInterestChange { get; set; }
        public decimal ClosePriceChange { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Close Price")]
        public decimal ClosePrice { get; set; }
        [Display(Name = "Total Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalValue => ClosePrice * OpenInterest * 100;

        public decimal Volatility { get; set; }
        public decimal VolatilityChange { get; set; }
        public DateTime CompareDate { get; set; }
    }
}