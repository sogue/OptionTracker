using System.Collections.Generic;

namespace FlowService.Models.ChainModels
{
    public class HistoricalChain
    {
        public int Id { get; set; }
        public int TickerId { get; set; }
        public string ChainSymbol { get; set; }
        public ICollection<HistoricalDate> Dates { get; set; } =
        new List<HistoricalDate>();

    }
}