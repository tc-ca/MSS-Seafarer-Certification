using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{

    public class ApplicantSearchCriteria
    {
        [JsonProperty("page")]
        public int Page { get; set; } = 0;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 20;

        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get { return DateOfBirthObj?.ToString("yyyy-MM-dd"); } }

        [JsonIgnore]
        public DateTime? DateOfBirthObj { get; set; }

        [JsonIgnore]
        public bool IsInvalid { get; set; } = false;


        public bool atLeastOne()
        {
            if (!String.IsNullOrWhiteSpace(this.Cdn) || !String.IsNullOrWhiteSpace(this.DateOfBirth) || !String.IsNullOrWhiteSpace(this.FirstName) || !String.IsNullOrWhiteSpace(this.LastName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
