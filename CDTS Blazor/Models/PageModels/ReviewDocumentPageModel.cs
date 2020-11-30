namespace CDNApplication.PageModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using CDNApplication.Data.DTO.MTAPI;
    using CDNApplication.Data.Services;
    using CDNApplication.Models;
    using CDNApplication.Models.PageModels;
    using CDNApplication.Services;
    using CDNApplication.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Defines the page model for the review document.
    /// </summary>
    public class ReviewDocumentPageModel : BaseComponent
    {
        private int confirmationGuid;

        /// <summary>
        /// Gets or sets the upload document page model.
        /// </summary>
        protected UploadDocumentPageModel Model { get; set; }

        /// <summary>
        /// Gets the previous page link.
        /// </summary>
        protected string PreviousPage => string.Format("{0}{1}/upload", this.NavigationManager.BaseUri, this.LanguageCode);

        /// <summary>
        /// Gets the confirmation page link.
        /// </summary>
        protected string ConfirmationPageLink => string.Format("{0}{1}/confirmation/{2}", this.NavigationManager.BaseUri, this.LanguageCode, this.confirmationGuid);

        /// <summary>
        /// Gets or sets the configuration object see appsettings.json.
        /// </summary>
        [Inject]
        protected IConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets or sets the session state.
        /// </summary>
        [Inject]
        protected SessionState State { get; set; }

        /// <summary>
        /// Gets or sets the mtoa email service.
        /// </summary>
        [Inject]
        protected IMtoaServices MtoaService { get; set; }

        /// <summary>
        /// Gets or sets the upload document stepper.
        /// </summary>
        [Inject]
        protected UploadDocumentsStepper UploadDocumentsStepper { get; set; }

        /// <summary>
        /// Method that is called when the reiew document page is submitted.
        /// </summary>
        protected void Submit()
        {

            Console.WriteLine(this.Model);

            this.State.UploadDocumentPage = null;

            var seafarersDocumentSubmissionEmail = new MtoaSeafarersDocumentSubmissionEmailParametersDto()
            {
                ConfirmationNumber = this.Model.ConfirmationNumber,
                CdnNumber = this.Model.CdnNumber,
                PhoneNumber = this.Model.PhoneNumber,
                EmailAddress = this.Model.EmailAddress,
                CertificateType = this.Model.CertificateType,
            };

            var mtoaEmailNotification = new MtoaEmailNotificationDto()
            {
                NotificationTemplateName = "Seafarers_Document_Submission_Email",
                ServiceRequestId = int.Parse(this.Configuration.GetSection("MtoaServiceSettings")["ServiceRequestId"]),
                UserId = int.Parse(this.Configuration.GetSection("MtoaServiceSettings")["UserId"]),
                UserName = "Nobody",
                Language = this.LanguageCode.Equals("fr", StringComparison.OrdinalIgnoreCase) ? "French" : "English",
                From = this.Configuration.GetSection("MtoaServiceSettings")["ReplyEmail"],
                To = this.Model.EmailAddress,
                IsHtml = true,
            };

            var mtoaParameterExtractor = new MtoaParameterExtractor();
            var parameters = mtoaParameterExtractor.ExtractParameters(seafarersDocumentSubmissionEmail);
            mtoaEmailNotification.Parameters.AddRange(parameters);

            var documentParameter = new KeyValuePair<string, string>("DOCUMENT", this.Model.ToMtoaDocumentString);

            mtoaEmailNotification.Parameters.Add(documentParameter);
            this.MtoaService.PostSendEmailNotificationAsync(mtoaEmailNotification);

            this.NavigationManager.NavigateTo(this.ConfirmationPageLink);

        }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {

            base.OnInitialized();

            this.UploadDocumentsStepper.Stepper.ActivateStepAtIndex(1);
            if (this.State.UploadDocumentPage == null)
            {
                this.State.UploadDocumentPage = new UploadDocumentPageModel();
            }

            this.Model = this.State.UploadDocumentPage;
            this.confirmationGuid = this.Model.MtoaServiceRequestId;
        }
    }
}
