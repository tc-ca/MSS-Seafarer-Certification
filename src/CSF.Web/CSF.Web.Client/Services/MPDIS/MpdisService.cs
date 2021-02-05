namespace CSF.Web.Client.Services.MPDIS
{
    using System;
    using System.Net.Http;
    using CSF.Web.Client.Data.DTO.MPDIS;
    using CSF.Web.Client.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;


    /// <summary>
    /// The Mpdis service.
    /// </summary>
    public class MpdisService : IMpdisService
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<MpdisService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MpdisService"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration service.</param>
        /// <param name="restClient">REST client service.</param>
        /// <param name="logger">Application logger service.</param>
        public MpdisService(IConfiguration configuration, IRestClient restClient, ILogger<MpdisService> logger)
        {
            this.configuration = configuration;
            this.restClient = restClient;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public ApplicantInformationDto GetApplicantByCdn(string cdn)
        {
            var serviceRequestPath = string.Format("applicants/cdn/{0}", cdn);
            try
            {
                return this.restClient.GetAsync<ApplicantInformationDto>(ServiceLocatorDomain.Mpdis, serviceRequestPath).GetAwaiter().GetResult();
            }
            catch (HttpRequestException httpRequestException)
            {
                this.logger.LogError(httpRequestException.Message, httpRequestException);
                throw;
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                // We want to retry once
                try
                {
                    return this.restClient.GetAsync<ApplicantInformationDto>(ServiceLocatorDomain.Mpdis, serviceRequestPath).GetAwaiter().GetResult();
                } catch(Exception exception)
                {
                    this.logger.LogError(exception.Message, exception);
                    throw;
                }
            }
        }
    }
}
