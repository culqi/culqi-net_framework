using Culqi.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Base
{
    // Base class for Culqi options classes, i.e. classes representing parameters for Culqi API requests.
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseOptions : INestedOptions
    {
        [JsonProperty("expand", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Expand { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> ExtraParams { get; set; }
            = new Dictionary<string, object>();

        public void AddExpand(string value)
        {
            if (this.Expand == null)
            {
                this.Expand = new List<string>();
            }

            this.Expand.Add(value);
        }

        public void AddRangeExpand(IEnumerable<string> values)
        {
            if (this.Expand == null)
            {
                this.Expand = new List<string>();
            }

            this.Expand.AddRange(values);
        }

        public void AddExtraParam(string key, object value)
        {
            this.ExtraParams.Add(key, value);
        }
    }
}
