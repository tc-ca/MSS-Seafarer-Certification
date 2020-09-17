﻿namespace CDNApplication.PageValidators
{
    using System.Resources;
    using System.Text.RegularExpressions;
    using CDNApplication.Models.PageModels;
    using FluentValidation;
    using ValidationResources = CDNApplication.Resources.Validation;

    /// <summary>
    ///     Validator for the upload document page model.
    /// </summary>
    /// <see cref="UploadDocumentPageModel"/>
    public class UploadDocumentValidator : AbstractValidator<UploadDocumentPageModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadDocumentValidator"/> class.
        /// </summary>
        public UploadDocumentValidator()
        {
            var localizer = new ResourceManager(typeof(ValidationResources.UploadDocumentErrorMessages));

            this.RuleFor(m => m.CdnNumber)
                .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage(localizer.GetString("CdnNumberNotEmptyText"))
                    .MinimumLength(6)
                    .WithMessage(localizer.GetString("CdnLengthText"))
                    .MaximumLength(7)
                    .WithMessage(localizer.GetString("CdnLengthText"))
                    .Matches(new Regex("^[a-zA-Z0-9]*$"))
                    .WithMessage(localizer.GetString("CdnFormatText"));

            this.RuleFor(m => m.CertificateType)
                .NotEmpty()
                .WithMessage(localizer.GetString("CertificateTypeNotEmptyText"));

            this.RuleFor(m => m.UploadedFiles)
                .NotEmpty()
                .WithMessage(localizer.GetString("UploadedFilesNotEmptyText"));
        }
    }
}