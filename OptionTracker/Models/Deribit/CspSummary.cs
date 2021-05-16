using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models.Deribit
{
    public class CspSummary : BookSummary
    {
        public decimal? Percentage { get; set; }
        public decimal? RiskUsd { get; set; }
        public decimal? PremiumUsd { get; set; }
        public decimal? CapitalMultiUsd { get; set; }
        public decimal? PremiumMultiUsd { get; set; }
    }
}
