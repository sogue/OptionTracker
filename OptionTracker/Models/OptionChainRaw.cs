using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OptionTracker.Models
{
    public class OptionChainRaw
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Created { get; set; }

        [Column(TypeName = "jsonb")]
        public JsonDocument Data { get; set; }

    }
}