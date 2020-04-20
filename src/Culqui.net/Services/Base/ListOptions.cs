using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqui.Services.Base
{
    public class ListOptions : BaseOptions
    {
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }

        [JsonProperty("bin")]
        public long? Bin { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("limit")]
        public long? Limit { get; set; }

        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }
    }
}
