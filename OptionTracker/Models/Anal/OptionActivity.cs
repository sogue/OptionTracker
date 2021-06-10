using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowService.Models.Anal
{
    public class OptionActivity
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal CallVolume { get; set; }
        public decimal PutVolume { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal CallPutRatio  { get; set; }
    }
}
