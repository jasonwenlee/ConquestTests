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
        [JsonProperty("AssetDescription")]
        public string AssetDescription { get; set; }

        [JsonProperty("Proposed")]
        public bool Proposed { get; set; }
    }
}
