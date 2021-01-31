using System.ComponentModel.DataAnnotations.Schema;

namespace OptionTracker.Models
{
    public class OptionChainRaw
    {
        public int Id { get; set; }

        [Column(TypeName = "jsonb")]
        public string Data { get; set; }

    }
}