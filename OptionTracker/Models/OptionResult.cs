using System.ComponentModel.DataAnnotations;

namespace OptionTracker.Models
{
    public class OptionResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Display(Name = "Open Interest")]
        public int OpenInterest { get; set; }
        [Display(Name = "Close Price")]
        [DataType(DataType.Currency)]
        public decimal ClosePrice { get; set; }
        [Display(Name = "Total Value")]
        [DataType(DataType.Currency)]
        public decimal TotalValue => ClosePrice * OpenInterest;
    }
}