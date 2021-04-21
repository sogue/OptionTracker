using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptionTracker.Models
{
    public class DateChain
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string ExpDate { get; set; }

        [Column(TypeName = "jsonb")]
        public ICollection<OptionContract> OptionContracts { get; set; }
    }
}