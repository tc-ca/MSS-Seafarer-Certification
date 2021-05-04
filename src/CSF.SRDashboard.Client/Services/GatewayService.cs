using CSF.Common.Library;
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
            this.gatewayRestClient = restClientCollection.First(o => o.GetType() == typeof(GatewayRestClient));
            this.logger = logger;
        }

        /// <inheritdoc/>
        public ApplicantPersonalInfo GetApplicantInfoByCdn(string cdn)
        {
            ApplicantPersonalInfo aplicantPeronalInfo = null;
            string requestPath = $"Applicant/{cdn}";

            try
            {
                aplicantPeronalInfo = this.gatewayRestClient.GetAsync<ApplicantPersonalInfo>(ServiceLocatorDomain.GatewayToMpdis, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return aplicantPeronalInfo;
        }

        /// <inheritdoc/>
        public ApplicantSearchResult Search(ApplicantSearchCriteria searchCriteria)
        {
            ApplicantSearchResult searchResult = null;
            string requestPath = "search";

            try
            {
                searchResult = this.gatewayRestClient.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, requestPath).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message + "\n" + ex.InnerException);
            }

            return searchResult;
        }
    }
}
