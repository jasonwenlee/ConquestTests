using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class HierachyNode
    {
        [JsonProperty("IncludeAncestors", NullValueHandling = NullValueHandling.Ignore)]
        public bool IncludeAncestors { get; set; }

        [JsonProperty("IncludeChildren", NullValueHandling = NullValueHandling.Ignore)]
        public bool IncludeChildren { get; set; }

        [JsonProperty("IncludeDescendents", NullValueHandling = NullValueHandling.Ignore)]
        public long IncludeDescendents { get; set; }

        [JsonProperty("IncludeSiblings", NullValueHandling = NullValueHandling.Ignore)]
        public bool IncludeSiblings { get; set; }

        [JsonProperty("IncludeSubItems", NullValueHandling = NullValueHandling.Ignore)]
        public bool IncludeSubItems { get; set; }

        [JsonProperty("ObjectKey", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectKey ObjectKey { get; set; }
    }
}
