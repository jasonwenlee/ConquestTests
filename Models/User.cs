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
        [JsonProperty("ChangeSet")]
        public ChangeSet ChangeSet { get; set; }
    }
    public class ChangeSet
    {
        [JsonProperty("Changes")]
        public string[] Changes { get; set; }

        [JsonProperty("LastEdit")]
        public DateTimeOffset LastEdit { get; set; }

        [JsonProperty("Updated")]
        public Updated Updated { get; set; }
    }

    public class Updated
    {
        [JsonProperty("EntryDate")]
        public DateTimeOffset EntryDate { get; set; }

        [JsonProperty("OrganisationUnitID")]
        public int OrganisationUnitID { get; set; }

        [JsonProperty("RequestDetail")]
        public string RequestDetail { get; set; }

        [JsonProperty("RequestorName")]
        public string RequestorName { get; set; }
    }
}
