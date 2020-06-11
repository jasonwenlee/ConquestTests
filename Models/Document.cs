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
        [JsonProperty("DocumentID", NullValueHandling = NullValueHandling.Ignore)]
        public int DocumentId { get; set; }

        [JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("ContentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        [JsonProperty("CreatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty("CreateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("DocumentDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentDescription { get; set; }

        [JsonProperty("Hashes", NullValueHandling = NullValueHandling.Ignore)]
        public object Hashes { get; set; }

        [JsonProperty("ObjectKey", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectKey ObjectKey { get; set; }
    }
}
