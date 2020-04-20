using Culqui.Entities.Base;
using Culqui.Entities.Interfaces;
using Culqui.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Culqui.Entities
{
    [JsonObject]
    public class CulquiList<T> : CulquiEntity<CulquiList<T>>, IEnumerable<T>
    {
        [JsonProperty("data", ItemConverterType = typeof(CulquiObjectConverter))]
        public List<T> Data { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public void Reverse()
        {
            this.Data.Reverse();
        }
    }
}
