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
        [JsonProperty("IncludeAncestors")]
        public bool IncludeAncestors { get; set; }

        [JsonProperty("IncludeChildren")]
        public bool IncludeChildren { get; set; }

        [JsonProperty("IncludeDescendents")]
        public long IncludeDescendents { get; set; }

        [JsonProperty("IncludeSiblings")]
        public bool IncludeSiblings { get; set; }

        [JsonProperty("IncludeSubItems")]
        public bool IncludeSubItems { get; set; }

        [JsonProperty("ObjectKey")]
        public ObjectKey ObjectKey { get; set; }
    }
}
