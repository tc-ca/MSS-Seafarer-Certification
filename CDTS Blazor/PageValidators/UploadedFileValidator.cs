using System.Resources;
using System.Text.RegularExpressions;
using CDNApplication.Data;
using CDNApplication.Models.PageModels;
using FluentValidation;
using ValidationResources = CDNApplication.Resources.Validation;

namespace CDNApplication.PageValidators
{
    /// <summary>
    /// Validator for the uploaded file model.
    /// </summary>
    /// <see cref="UploadedFile"/>
    public class UploadedFileValidator : AbstractValidator<UploadedFile>
    {

        public UploadedFileValidator()
        {
            var localizer = new ResourceManager(typeof(ValidationResources.UploadDocumentErrorMessages));

            this.RuleFor(m => m.Description)
                .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage(localizer.GetString("FileDescriptionNotEmptyText"));

        }

    }
}
