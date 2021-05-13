using CSF.Common.Library;
using CSF.SRDashboard.Client.DTO;
using Microsoft.Extensions.Logging;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.SRDashboard.Client.Services
{
    /// <inheritdoc/>
    public class GatewayService : IGatewayService
    {
        private readonly IRestClient gatewayRestClient;
        private readonly ILogger<GatewayService> logger;

        public GatewayService(IEnumerable<IRestClient> restClientCollection, ILogger<GatewayService> logger)
        {
            this.gatewayRestClient = restClientCollection.First(o => o is GatewayRestClient);
            this.logger = logger;
        }

        /// <inheritdoc/>
        public MpdisApplicantDto GetApplicantInfoByCdn(string cdn)
        {
            MpdisApplicantDto aplicantPeronalInfo = null;
            string requestPath = $"Applicant/{cdn}";

            if (string.IsNullOrEmpty(cdn))
                return null;

            try
            {
                aplicantPeronalInfo = this.gatewayRestClient.GetAsync<MpdisApplicantDto>(ServiceLocatorDomain.GatewayToMpdis, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return aplicantPeronalInfo;
        }

        /// <inheritdoc/>
        public ApplicantSearchResult SearchForApplicants(ApplicantSearchCriteria applicantSearchCriteria)
        {
            ApplicantSearchResult searchResult = null;
            string requestPath = "search";

            if(applicantSearchCriteria == null || IsApplicantSearchCriteriaEmpty(applicantSearchCriteria))
                    return new ApplicantSearchResult();

            try
            {
                searchResult = this.gatewayRestClient.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, requestPath, applicantSearchCriteria).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return searchResult;
        }

        private bool IsApplicantSearchCriteriaEmpty(ApplicantSearchCriteria applicantSearchCriteria)
        {
            if (
                string.IsNullOrEmpty(applicantSearchCriteria.Cdn) &&
                string.IsNullOrEmpty(applicantSearchCriteria.FirstName) &&
                string.IsNullOrEmpty(applicantSearchCriteria.LastName) &&
                string.IsNullOrEmpty(applicantSearchCriteria.DateOfBirth))
                return true;
            else
            {
                return false;
            }

        }
    }
}
