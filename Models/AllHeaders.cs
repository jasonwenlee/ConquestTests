using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class AllHeaders
    {
        [JsonProperty("Headers", NullValueHandling = NullValueHandling.Ignore)]
        public Header[] Headers { get; set; }
    }

    public class Header
    {
        [JsonProperty("ObjectName", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectName { get; set; }
        [JsonProperty("ObjectKey", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectKey ObjectKey { get; set; }
        [JsonProperty("ParentKey", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectKey ParentKey { get; set; }
    }
}
