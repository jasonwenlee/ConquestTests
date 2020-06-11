using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConquestTests.Models
{
    public class User
    {
        [JsonProperty("ChangeSet", NullValueHandling = NullValueHandling.Ignore)]
        public ChangeSet ChangeSet { get; set; }
    }
    public class ChangeSet
    {
        [JsonProperty("Changes", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Changes { get; set; }

        [JsonProperty("LastEdit", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastEdit { get; set; }

        [JsonProperty("Updated", NullValueHandling = NullValueHandling.Ignore)]
        public Updated Updated { get; set; }
    }

    public class Updated
    {
        [JsonProperty("EntryDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset EntryDate { get; set; }

        [JsonProperty("OrganisationUnitID", NullValueHandling = NullValueHandling.Ignore)]
        public int OrganisationUnitID { get; set; }

        [JsonProperty("RequestDetail", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestDetail { get; set; }

        [JsonProperty("RequestorName", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestorName { get; set; }
    }
}
