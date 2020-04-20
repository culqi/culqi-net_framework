using Culqui.Entities.Base;
using Culqui.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqui.Entities
{
    public class CulquiError : CulquiEntity<CulquiError>, IHasObject
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("charge_id")]
        public string ChargeId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("decline_code")]
        public string DeclineCode { get; set; }

        [JsonProperty("merchant_message")]
        public string Message { get; set; }

        [JsonProperty("user_message")]
        public string UserMessage { get; set; }

        [JsonProperty("param")]
        public string Parameter { get; set; }
    }
}
