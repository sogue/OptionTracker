namespace OptionTracker.Models
{
    public class DateChainViewModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string ExpDate { get; set; }
        public decimal[] Strikes { get; set; }
    }
}