using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class Document
    {
        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("ContentType")]
        public string ContentType { get; set; }

        [JsonProperty("CreateTime")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("DocumentDescription")]
        public string DocumentDescription { get; set; }

        [JsonProperty("Hashes")]
        public object Hashes { get; set; }

        [JsonProperty("ObjectKey")]
        public ObjectKey ObjectKey { get; set; }
    }
}
