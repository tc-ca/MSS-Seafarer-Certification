namespace CDNApplication.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using CDNApplication.Data.DTO.MTAPI;
    using CDNApplication.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class MtoaServices : IMtoaServices
    {
        private readonly IConfiguration _configuration;
        private readonly IRestClient _restClient;
        private readonly ILogger<MtoaServices> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaServices"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="restClient"></param>
        /// <param name="logger"></param>
        public MtoaServices(IConfiguration configuration, IRestClient restClient, ILogger<MtoaServices> logger)
        {
            this._configuration = configuration;
            this._restClient = restClient;
            this._logger = logger;
        }

        /// <summary>
        /// This procedure posts the submission email template to MTOA on application startup.
        /// </summary>
        /// <returns>Returns nothing (void).</returns>
        public async Task PostSubmissionEmailNotificationTemplateAsync()
        {
            string emailNotificationServicePath = this._configuration.GetSection("MtoaServiceSettings")["EmailNotificationTemplatePath"];

            MtoaEmailNotificationTemplateDTO mtoaEmailNotificationTemplate = this.GetSubmissionEmailNotificationTemplate();

            try
            {
                var result = await this._restClient.PostAsync<MtoaEmailNotificationTemplateDTO>(ServiceDomain.Mtoa, emailNotificationServicePath, mtoaEmailNotificationTemplate);
            }
            catch
            {
                // TODO: Log the error and continue
                return;
            }
        }

        private MtoaEmailNotificationTemplateDTO GetSubmissionEmailNotificationTemplate()
        {
            MtoaEmailNotificationTemplateDTO mtoaEmailNotificationTemplate = new MtoaEmailNotificationTemplateDTO();

            mtoaEmailNotificationTemplate.Name = this._configuration.GetSection("MtoaServiceSettings")["EmailSubmissionTemplateName"];
            mtoaEmailNotificationTemplate.ServiceName = this._configuration.GetSection("MtoaServiceSettings")["SeafarerCertificationServiceName"];
            mtoaEmailNotificationTemplate.HasSubjectParameters = true;
            mtoaEmailNotificationTemplate.SubjectTextEnglish = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateSubjectTextEnglish.html");
            mtoaEmailNotificationTemplate.SubjectTextFrench = File.ReadAllText($"Resources/EmailTemplates/SubmissionEmailTemplateSubjectTextFrench.html");
            mtoaEmailNotificationTemplate.HasBodyParameters = true;
            mtoaEmailNotificationTemplate.BodyTextEnglish.AddRange(File.ReadAllLines($"Resources/EmailTemplates/SubmissionEmailTemplateBodyTextEnglish.html"));
            mtoaEmailNotificationTemplate.BodyTextFrench.AddRange(File.ReadAllLines($"Resources/EmailTemplates/SubmissionEmailTemplateBodyTextEnglish.html"));

            return mtoaEmailNotificationTemplate;
        }
    }
}