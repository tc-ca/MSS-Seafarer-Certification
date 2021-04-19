using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDIS.API.Wrapper.Services.MPDIS.Entities
{
    public class ApplicantSearchResult
    {

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; } = 0;


        [JsonProperty("items")]
        public  List<ApplicantSearchResultItem> Items { get; set; }

    }
}
