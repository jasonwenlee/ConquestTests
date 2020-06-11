using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class Asset
    {
        [JsonProperty("AssetDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string AssetDescription { get; set; }

        [JsonProperty("Proposed", NullValueHandling = NullValueHandling.Ignore)]
        public bool Proposed { get; set; }
    }
}
