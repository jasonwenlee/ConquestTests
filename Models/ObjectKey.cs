using Newtonsoft.Json;
using System;

namespace ConquestTests.Models
{
    public class ObjectKey
    {
        [JsonProperty("int32Value", NullValueHandling = NullValueHandling.Ignore)]
        public int Int32Value { get; set; }

        [JsonProperty("objectType", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectType { get; set; }

        //[JsonProperty("stringValue", NullValueHandling = NullValueHandling.Ignore)]
        //public string StringValue { get; set; }

        //[JsonProperty("timestampValue", NullValueHandling = NullValueHandling.Ignore)]
        //public DateTimeOffset TimestampValue { get; set; }
    }
}