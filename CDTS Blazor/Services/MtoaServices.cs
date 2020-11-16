namespace CDNApplication.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using CDNApplication.Data.DTO.MTAPI;
    using CDNApplication.Utilities;
    using Microsoft.ApplicationInsights.Extensibility.Implementation;
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
                this.logger.LogError(e.Message);
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
                this.logger.LogError(e.Message);
                throw;
            }
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