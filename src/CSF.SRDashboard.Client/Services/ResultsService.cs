using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public class ResultsService : IResultsService
    {
        public List<ApplicantSearchResultItem> list { get; set; }
        public ResultsService()
        {
        }

        public void LoadData(ApplicantSearchResult result)
        {
            this.list = result.Items;
        }
    }
}
