using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;

namespace CSF.SRDashboard.Client.Services
{
    /// <summary>
    /// Interface for handling service calls going to wwwappsext.
    /// </summary>
    public interface IGatewayService
    {
        /// <summary>
        /// Returns seafarer information for a specific cdn.
        /// </summary>
        /// <param name="cdn">Candidate Document Number of the seafarer.</param>
        /// <returns></returns>
        TrimmedApplicantInformation GetApplicantInfoByCdn(string cdn);

        /// <summary>
        /// Returns seafarer search results according to search criteria.
        /// </summary>
        /// <param name="searchCriteria">Object containing valid search criteria against the API endpoint.</param>
        /// <returns></returns>
        ApplicantSearchResult SearchForApplicants(ApplicantSearchCriteria searchCriteria);
    }
}