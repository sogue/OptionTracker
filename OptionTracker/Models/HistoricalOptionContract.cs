using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OptionTracker.Models;

namespace FlowService.Models.ChainModels
{
    public class HistoricalOptionContract
    {
        public int Id { get; set; }
        public int HistoricalDateId { get; set; }
        public string ContractSymbol { get; set; }
        [Column(TypeName = "jsonb")]
        public ICollection<OptionContract> Contracts { get; set; }

    }
}