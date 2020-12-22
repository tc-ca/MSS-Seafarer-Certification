namespace CSF.Web.Client.Services.EmailNotification
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CSF.Web.Client.Data.DTO.MTAPI;
    using CSF.Web.Client.Models.PageModels;
    using CSF.Web.Client.Shared;
    using CSF.Web.Client.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// Defines the email builder for the seafarer's document submission email.
    /// </summary>
    public class SeafarersDocumentSubmissionEmailBuilder : IEmailNotificationBuilder<UploadDocumentPageModel>
    {
        private readonly IStringLocalizer<Common> commonPageLocalizer;
        private readonly IConfiguration configuration;
        private readonly string languageCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeafarersDocumentSubmissionEmailBuilder"/> class.
        /// </summary>
        /// <param name="configuration">The application's configuration object.</param>
        /// <param name="languageCode">The language code.</param>
        public SeafarersDocumentSubmissionEmailBuilder(IStringLocalizer<Common> commonPageLocalizer, IConfiguration configuration, string languageCode)
        {
            this.commonPageLocalizer = commonPageLocalizer;
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

            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;
            var currentUICulture = System.Globalization.CultureInfo.CurrentUICulture;

            var englishCulture = new System.Globalization.CultureInfo("en");
            var frenchCulture = new System.Globalization.CultureInfo("fr");

            System.Globalization.CultureInfo.CurrentCulture = englishCulture;
            System.Globalization.CultureInfo.CurrentUICulture = englishCulture;

            var submissionTypeEnglish = this.commonPageLocalizer[model.SubmissionType.GetValue()];

            System.Globalization.CultureInfo.CurrentCulture = frenchCulture;
            System.Globalization.CultureInfo.CurrentUICulture = frenchCulture;

            var submissionTypeFrench = this.commonPageLocalizer[model.SubmissionType.GetValue()];

            System.Globalization.CultureInfo.CurrentCulture = currentCulture;
            System.Globalization.CultureInfo.CurrentUICulture = currentUICulture;

            var seafarersDocumentSubmissionEmail = new MtoaSeafarersSubmissionEmailParametersDto()
            {
                ConfirmationNumber = model.ConfirmationNumber,
                CdnNumber = model.CdnNumber,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                CertificateType = model.CertificateType,
                SubmissionTypeEnglish = submissionTypeEnglish,
                SubmissionTypeFrench = submissionTypeFrench,
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
