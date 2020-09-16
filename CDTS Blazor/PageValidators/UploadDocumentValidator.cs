namespace CDNApplication.PageValidators
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

            // CDN number is required
            this.RuleFor(m => m.CdnNumber)
                .NotEmpty()
                .WithMessage(localizer.GetString("CdnNumberNotEmptyText"));

            // CDN number is composed of alphanumeric only (format)
            this.RuleFor(m => m.CdnNumber)
                .Matches(new Regex("^[a-zA-Z0-9]*$"))
                .WithMessage(localizer.GetString("CdnFormatText"));

            // CDN number is composed of 6 to 7 digits (length)
            this.RuleFor(m => m.CdnNumber)
                .MinimumLength(6)
                .MaximumLength(7)
                .WithMessage(localizer.GetString("CdnLengthText"));

            // Certificate type is required
            this.RuleFor(m => m.CertificateType)
                .NotEmpty()
                .WithMessage(localizer.GetString("CertificateTypeNotEmptyText"));

            // There must be at least 1 uploaded file
            this.RuleFor(m => m.UploadedFiles)
                .NotEmpty()
                .WithMessage(localizer.GetString("UploadedFilesNotEmptyText"));
        }
    }
}