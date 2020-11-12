namespace CDNApplication.PageModels
{
    using System;
    using CDNApplication.Data.Services;
    using CDNApplication.Models;
    using CDNApplication.Models.PageModels;
    using CDNApplication.Services;
    using CDNApplication.Utilities;
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Defines the page model for the review document.
    /// </summary>
    public class ReviewDocumentPageModel : BaseComponent
    {
        protected UploadDocumentPageModel model;

        protected string previousPage => NavigationManager.BaseUri + "" + LanguageCode + "/upload";

        protected int _confirmationGuid;

        [Inject]
        protected SessionState state { get; set; }

        [Inject]
        protected MtoaEmailService emailService { get; set; }

        [Inject]
        protected UploadDocumentsStepper UploadDocumentsStepper { get; set; }

        protected void Submit()
        {

            Console.WriteLine(model);

            state.UploadDocumentPage = null;

            if (!String.IsNullOrEmpty(model.EmailAddress))
            {
                emailService.SendEmailToApplicant(model).Wait();
            }

            NavigationManager.NavigateTo($"{NavigationManager.BaseUri}{LanguageCode}/confirmation/{_confirmationGuid}");

        }

        protected override void OnInitialized()
        {

            base.OnInitialized();

            this.UploadDocumentsStepper.Stepper.ActivateStepAtIndex(1);

            _confirmationGuid = new Random().Next(100000, 9999999);

            if (state.UploadDocumentPage == null)
            {
                state.UploadDocumentPage = new UploadDocumentPageModel();
            }

            model = state.UploadDocumentPage;

            model.ConfirmationNumber = _confirmationGuid.ToString();
        }
    }
}
