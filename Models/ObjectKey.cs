using Newtonsoft.Json;
using System;

namespace ConquestTests.Models
{
    public class ObjectKey
    {
        [JsonProperty("int32Value")]
        public int Int32Value { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get; set; }

        [JsonProperty("stringValue")]
        public string StringValue { get; set; }

        [JsonProperty("timestampValue")]
        public DateTimeOffset TimestampValue { get; set; }
    }
}