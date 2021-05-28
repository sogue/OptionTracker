using System;
using System.Collections.Generic;

namespace OptionTracker.Models.Anal
{
    public class VolumeAnal
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public ICollection<VolumeData> VolumeDatas { get; set; }
    }

}