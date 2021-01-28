using System.ComponentModel.DataAnnotations;

namespace OptionTracker.Models
{
    public class OptionResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int OpenInterest { get; set; }
        [DisplayFormat(DataFormatString="{0:C}")]
        [DataType(DataType.Currency)]
        public decimal ClosePrice { get; set; }
        [Display(Name = "Total Value")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal TotalValue => ClosePrice * OpenInterest * 100;
    }
}