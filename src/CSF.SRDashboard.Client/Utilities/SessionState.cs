using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Utilities
{
    /// <summary>
    /// A class that is used to transfer data from one page to another.
    /// </summary>
    public class SessionState
    {

        public ApplicantSearchResult ApplicantSearchResult { get; set; }

        public ApplicantSearchCriteria SearchCriteria { get; set; }

       public string UserDisplayName { get; set; }
    }
}
