using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class ObjectKey
    {
        //[JsonProperty("int32Value")]
        //public long Int32Value { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get; set; }

        //[JsonProperty("stringValue")]
        //public string StringValue { get; set; }

        //[JsonProperty("timestampValue")]
        //public DateTimeOffset TimestampValue { get; set; }
    }
}
