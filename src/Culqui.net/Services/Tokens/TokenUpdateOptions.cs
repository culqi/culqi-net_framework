using Culqui.Entities.Interfaces;
using Culqui.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqui
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
