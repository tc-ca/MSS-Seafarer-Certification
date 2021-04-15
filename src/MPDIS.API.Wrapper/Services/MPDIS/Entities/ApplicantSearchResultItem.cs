using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{
    public class ApplicantSearchResultItem
    {
        [JsonProperty("applicantId")]
        public string ApplicantId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("deceasedStatus")]
        public bool DeceasedStatus { get; set; }

    }
}
