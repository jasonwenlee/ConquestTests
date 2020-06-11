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
        [JsonProperty("Headers")]
        public Header[] Headers { get; set; }
    }

    public class Header
    {
        [JsonProperty("ObjectName")]
        public string ObjectName { get; set; }
        [JsonProperty("ObjectKey")]
        public ObjectKey ObjectKey { get; set; }
        [JsonProperty("ParentKey")]
        public ObjectKey ParentKey { get; set; }
    }
}
