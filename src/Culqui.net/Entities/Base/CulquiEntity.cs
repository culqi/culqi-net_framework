using Culqui.Entities.Interfaces;
using Culqui.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Culqui.Entities.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CulquiEntity : ICulquiEntity
    {
        [JsonIgnore]
        private JObject rawJObject;

        [JsonIgnore]
        public JObject RawJObject
        {
            get
            {
                if (rawJObject == null)
                {
                    if (CulquiResponse == null)
                    {
                        return null;
                    }

                    rawJObject = JObject.Parse(this.CulquiResponse.Content);
                }

                return rawJObject;
            }

            protected set
            {
                rawJObject = value;
            }
        }

        [JsonIgnore]
        public CulquiResponse CulquiResponse { get; set; }

        public static IHasObject FromJson(string value)
        {
            return JsonUtils.DeserializeObject<IHasObject>(value, CulquiConfiguration.SerializerSettings);
        }

        public static T FromJson<T>(string value) where T : ICulquiEntity
        {
            return JsonUtils.DeserializeObject<T>(value, CulquiConfiguration.SerializerSettings);
        }

        public override string ToString()
        {
            return string.Format("<{0}@{1} id={2}> JSON: {3}", GetType().FullName, RuntimeHelpers.GetHashCode(this), GetIdString(), ToJson());
        }

        public string ToJson()
        {
            return JsonUtils.SerializeObject(this, Formatting.Indented, CulquiConfiguration.SerializerSettings);
        }

        protected static ExpandableField<T> SetExpandableFieldId<T>(string id, ExpandableField<T> expandable) where T : IHasId
        {
            if (expandable == null)
            {
                expandable = new ExpandableField<T>();
                expandable.Id = id;
            }
            else if (expandable.Id != id)
            {
                expandable.ExpandedObject = default;
                expandable.Id = id;
            }

            return expandable;
        }

        protected static ExpandableField<T> SetExpandableFieldObject<T>(T obj, ExpandableField<T> expandable) where T : IHasId
        {
            if (expandable == null)
            {
                expandable = new ExpandableField<T>();
            }

            expandable.ExpandedObject = obj;

            return expandable;
        }

        private object GetIdString()
        {
            foreach (var property in this.GetType().GetTypeInfo().DeclaredProperties)
            {
                if (property.Name == "Id")
                {
                    return property.GetValue(this);
                }
            }

            return null;
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic variant")]
    public abstract class CulquiEntity<T> : CulquiEntity where T : CulquiEntity<T>
    {
        public static new T FromJson(string value)
        {
            return FromJson<T>(value);
        }
    }
}
