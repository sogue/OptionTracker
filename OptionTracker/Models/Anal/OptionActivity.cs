using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FlowService.Models.Anal
{
    public class OptionActivity
    {
        public int Id { get; set; }
        public int TickerId { get; set; } 
        public string Ticker { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal CallVolume { get; set; }
        public decimal PutVolume { get; set; }
        public decimal TotalVolume { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal CallPutRatio  { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal OptionVolumeChange { get; set; }
        public string AssetType { get; internal set; }
    }
}
