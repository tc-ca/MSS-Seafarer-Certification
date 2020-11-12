﻿namespace CDNApplication.PageModels
{
    using System;
    using CDNApplication.Data.Services;
    using CDNApplication.Models;
    using CDNApplication.Models.PageModels;
    using CDNApplication.Services;
    using CDNApplication.Utilities;

    /// <summary>
    /// Defines the page model for the review document.
    /// </summary>
    public class ReviewDocumentPageModel : BaseComponent
    {
        protected UploadDocumentPageModel model;

        protected string previousPage => NavigationManager.BaseUri + "" + LanguageCode + "/upload";

        protected int _confirmationGuid;

        protected SessionState state;
        protected MtoaEmailService emailService;
        protected UploadDocumentsStepper UploadDocumentsStepper;

        public ReviewDocumentPageModel(SessionState state, MtoaEmailService emailService, UploadDocumentsStepper uploadDocumentsStepper)
        {
            this.state = state;
            this.emailService = emailService;
            this.UploadDocumentsStepper = uploadDocumentsStepper;
        }

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
