﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Infrastructure
{
    public class DateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteRawValue(((DateTime)value).ConvertDateTimeToEpoch().ToString());
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                return EpochTime.ConvertEpochToDateTime((long)reader.Value);
            }

            return DateTime.Parse(reader.Value.ToString());
        }
    }
}
