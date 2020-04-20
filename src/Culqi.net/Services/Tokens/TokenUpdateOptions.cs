using Culqi.Entities.Interfaces;
using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class TokenUpdateOptions : BaseOptions, IHasMetadata
    {

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }
    }
}
