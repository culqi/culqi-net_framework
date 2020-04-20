using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class ChargeCaptureOptions : BaseOptions
    {
        [JsonProperty("capture")]
        public bool Captured { get; set; }
    }
}
