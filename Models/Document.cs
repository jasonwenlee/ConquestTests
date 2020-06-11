using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class DocDataObject
    {
        [JsonProperty("Document", NullValueHandling = NullValueHandling.Ignore)]
        public Document Document { get; set; }

        [JsonProperty("UploadUri", NullValueHandling = NullValueHandling.Ignore)]
        public string UploadUri { get; set; }

        [JsonProperty("UploadMethod", NullValueHandling = NullValueHandling.Ignore)]
        public string UploadMethod { get; set; }

    }

    public class Document
    {
        [JsonProperty("DocumentID", NullValueHandling = NullValueHandling.Ignore)]
        public long DocumentId { get; set; }
        [JsonProperty("Order", NullValueHandling = NullValueHandling.Ignore)]
        public long Order { get; set; }
        [JsonProperty("DocumentDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentDescription { get; set; }
        [JsonProperty("ContentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }
        [JsonProperty("CreatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }
        [JsonProperty("CreateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset CreateTime { get; set; }
        [JsonProperty("ObjectKey", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectKey ObjectKey { get; set; }
    }
}
