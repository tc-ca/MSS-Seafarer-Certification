namespace CDNApplication.Services.EmailNotification
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CDNApplication.Data.DTO.MTAPI;
    using CDNApplication.Models.PageModels;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Defines the email builder for the seafarer's document submission email.
    /// </summary>
    public class SeafarersDocumentSubmissionEmailBuilder : IEmailNotificationBuilder<UploadDocumentPageModel>
    {
        private readonly IConfiguration configuration;
        private readonly string languageCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeafarersDocumentSubmissionEmailBuilder"/> class.
        /// </summary>
        /// <param name="configuration">The application's configuration object.</param>
        /// <param name="languageCode">The language code.</param>
        public SeafarersDocumentSubmissionEmailBuilder(IConfiguration configuration, string languageCode)
        {
            this.configuration = configuration;
            this.languageCode = languageCode;
        }

        /// <inheritdoc/>
        public string TemplateName => this.configuration.GetSection("MtoaServiceSettings")["EmailSubmissionTemplateName"];

        /// <inheritdoc/>
        public MtoaEmailNotificationDto Build(UploadDocumentPageModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var seafarersDocumentSubmissionEmail = new MtoaSeafarersSubmissionEmailParametersDto()
            {
                ConfirmationNumber = model.ConfirmationNumber,
                CdnNumber = model.CdnNumber,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                CertificateType = model.CertificateType,
                EnglishIntroduction = string.Format(File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateIntroductionEnglish.html"), model.ConfirmationNumber),
                EnglishSignature = File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateSignatureEnglish.html"),
                FrenchIntroduction = string.Format(File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateIntroductionFrench.html"), model.ConfirmationNumber),
                FrenchSignature = File.ReadAllText($"Resources/EmailTemplates/Parameters/SubmissionEmailTemplateSignatureFrench.html"),
            };

            var mtoaEmailNotification = new MtoaEmailNotificationDto()
            {
                NotificationTemplateName = this.TemplateName,
                ServiceRequestId = int.Parse(this.configuration.GetSection("MtoaServiceSettings")["ServiceRequestId"]),
                UserId = int.Parse(this.configuration.GetSection("MtoaServiceSettings")["UserId"]),
                UserName = "Nobody",
                Language = this.languageCode.Equals("fr", StringComparison.OrdinalIgnoreCase) ? "French" : "English",
                From = this.configuration.GetSection("MtoaServiceSettings")["ReplyEmail"],
                To = model.EmailAddress,
                IsHtml = true,
            };

            var mtoaParameterExtractor = new MtoaParameterExtractor();
            var parameters = mtoaParameterExtractor.ExtractParameters(seafarersDocumentSubmissionEmail);
            mtoaEmailNotification.Parameters.AddRange(parameters);

            var documentParameter = new KeyValuePair<string, string>("DOCUMENT", model.ToMtoaDocumentString);

            mtoaEmailNotification.Parameters.Add(documentParameter);

            return mtoaEmailNotification;
        }
    }
}
