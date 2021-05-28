using Org.OpenAPITools.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models.Deribit
{
    public class CspSummary : BookSummary
    {
        public decimal? Percentage { get; set; }

        public decimal? LiqStrike { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? RiskUsd { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? PremiumUsd { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? CapitalMultiUsd { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? PremiumMultiUsd { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? PremiumMultiUsdMonth { get; set; }
        public int Multiplier { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? ActualCapital { get; set; }
        public decimal? ActualLiqStrike { get; set; }
        public decimal? ActualLeverage { get; set; }
    }
}
