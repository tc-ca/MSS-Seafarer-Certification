namespace CSF.Web.Client.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using CSF.Common.Library;
    using CSF.Web.Client.Data;
    using CSF.Web.Client.Data.DTO;
    using CSF.Web.Client.Data.DTO.MTAPI;
    using CSF.Web.Client.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class containing all MTOA service calls needed by the application.
    /// </summary>
    public class MtoaServices : IMtoaServices
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<MtoaServices> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaServices"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration service.</param>
        /// <param name="restClient">REST client service.</param>
        /// <param name="logger">Application logger service.</param>
        public MtoaServices(IConfiguration configuration, IRestClient restClient, ILogger<MtoaServices> logger)
        {
            this.configuration = configuration;
            this.restClient = restClient;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task PostSubmissionEmailNotificationTemplateAsync()
        {
            string emailNotificationServicePath = this.configuration.GetSection("MtoaServiceSettings")["EmailNotificationTemplatePath"];

            MtoaEmailNotificationTemplateDto mtoaEmailNotificationTemplate = this.GetSubmissionEmailNotificationTemplate();

            try
            {
                await this.restClient.PostAsync<MtoaEmailNotificationTemplateDto>(ServiceLocatorDomain.Mtoa, emailNotificationServicePath, mtoaEmailNotificationTemplate).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task PostSendEmailNotificationAsync(MtoaEmailNotificationDto mtoaEmailNotification)
        {
            string sendEmailNotificationPath = this.configuration.GetSection("MtoaServiceSettings")["EmailNotificationPath"];

            try
            {
                await this.restClient.PostAsync<MtoaEmailNotificationDto>(ServiceLocatorDomain.Mtoa, sendEmailNotificationPath, mtoaEmailNotification).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <inheritdoc/>
        public ServiceRequestCreationResult PostServiceRequests()
        {
            string serviceRequestsPath = this.GetServiceRequestsPath();

            try
            {
                return this.restClient.PostAsync<ServiceRequestCreationResult>(ServiceLocatorDomain.Mtoa, serviceRequestsPath, null).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                throw;
            }
        }

        /// <inheritdoc/>
        public Task PostFileAttachmentsAsync()
        {
            // string fileAttachmentsPath = this.configuration.GetSection("MtoaServiceSettings")["FileAttachmentsPath"];
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task PostSeafarerArtifactInformationAsync(SeafarersArtifactDto seafarerArtifactInformation)
        {
            string artifactPath = this.GetArtifactPath(seafarerArtifactInformation.ServiceRequestId);

            var restClientRequestOptions = new RestClientRequestOptions()
            {
                ServiceName = ServiceLocatorDomain.Mtoa,
                Path = artifactPath,
                DataObject = seafarerArtifactInformation,
                ParameterContentType = "application/octet-stream",
            };

            try
            {
                return this.restClient.PostAsync<object>(restClientRequestOptions);
            }
            catch (Exception e)
            {
                this.logger.LogError(e.Message, e);
                throw;
            }
        }

        private string GetArtifactPath(int serviceRequestId)
        {
            string artifactPath = this.configuration.GetSection("MtoaServiceSettings")["ArtifactPath"];
            string userId = this.configuration.GetSection("MtoaServiceSettings")["UserId"];

            return string.Format("{0}?artifactType={1}&version={2}&serviceRequestId={3}&userId={4}", artifactPath, ArtifactType.JsonDocument.ToString(), 1, serviceRequestId, userId);
        }

        private string GetServiceRequestsPath()
        {
            string userId = this.configuration.GetSection("MtoaServiceSettings")["UserId"];
            string serviceId = this.configuration.GetSection("MtoaServiceSettings")["ServiceId"];
            string serviceNameEnglish = this.configuration.GetSection("MtoaServiceSettings")["ServiceNameInEnglish"];
            string serviceNameFrench = this.configuration.GetSection("MtoaServiceSettings")["ServiceNameInFrench"];
            string serviceRequestStatus = this.configuration.GetSection("MtoaServiceSettings")["ProgressStatus"];
            return string.Format(string.Format("api/v1/servicerequests?userId={0}&serviceId={1}&englishName={2}&frenchName={3}&serviceRequestStatus={4}", userId, serviceId, serviceNameEnglish, serviceNameFrench, serviceRequestStatus));
        }

        private MtoaEmailNotificationTemplateDto GetSubmissionEmailNotificationTemplate()
        {
            MtoaEmailNotificationTemplateDto mtoaEmailNotificationTemplate = new MtoaEmailNotificationTemplateDto();

            mtoaEmailNotificationTemplate.Name = this.configuration.GetSection("MtoaServiceSettings")["EmailSubmissionTemplateName"];
            mtoaEmailNotificationTemplate.ServiceName = this.configuration.GetSection("MtoaServiceSettings")["SeafarerCertificationServiceName"];
            mtoaEmailNotificationTemplate.HasSubjectParameters = true;
            mtoaEmailNotificationTemplate.SubjectTextEnglish = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateSubjectTextEnglish.html");
            mtoaEmailNotificationTemplate.SubjectTextFrench = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateSubjectTextFrench.html");
            mtoaEmailNotificationTemplate.HasBodyParameters = true;
            mtoaEmailNotificationTemplate.BodyTextEnglish.AddRange(File.ReadAllLines($"Resources/EmailTemplates/SubmissionEmailTemplateBodyTextEnglish.html"));
            mtoaEmailNotificationTemplate.BodyTextFrench.AddRange(File.ReadAllLines($"Resources/EmailTemplates/SubmissionEmailTemplateBodyTextFrench.html"));

            return mtoaEmailNotificationTemplate;
        }
    }
}