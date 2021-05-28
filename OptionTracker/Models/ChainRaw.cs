using System.ComponentModel.DataAnnotations.Schema;

namespace OptionTracker.Models
{
    public class ChainRaw
    {
        public int Id { get; set; }

        [Column(TypeName = "jsonb")]
        public Chain Chain { get; set; }
    }
}