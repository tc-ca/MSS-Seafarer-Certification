using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System.Collections.Generic;

namespace CSF.SRDashboard.Client.Services
{
    public interface IResultsService
    {
        List<ApplicantSearchResultItem> list { get; set; }

        void LoadData(ApplicantSearchResult result);
    }
}