using System.ComponentModel.DataAnnotations.Schema;
using OptionTracker.Models;

namespace OptionTracker.Models
{
    public class OptionChain
    {
        public int Id { get; set; }

        [Column(TypeName = "jsonb")]
        public OptionContract OptionContract { get; set; }

    }
}