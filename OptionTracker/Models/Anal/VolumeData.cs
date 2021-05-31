using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Org.OpenAPITools.Models;

namespace OptionTracker.Models.Anal
{
    public class VolumeData
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int Volume { get; set; }
        public Instrument.OptionTypeEnum? OptionType { get; set; }

    }
}