using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowService.Models.ChainModels
{
    public class HistoricalDate
    {
        public int Id { get; set; }
        public int HistoricalChainId { get; set; }
        public string DateSymbol { get; set; }

        public ICollection<HistoricalOptionContract> OptionContracts { get; set; } =
            new List<HistoricalOptionContract>();

    }
}