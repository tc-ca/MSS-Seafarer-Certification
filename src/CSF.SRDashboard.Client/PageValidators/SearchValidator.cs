using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;

namespace CSF.SRDashboard.Client.PageValidators
{
    public class SearchValidator : AbstractValidator<ApplicantSearchCriteria>
    {
        public SearchValidator()
        {

            /*this.RuleFor(x => x.Cdn).NotEmpty().When(x => String.IsNullOrEmpty(x.DateOfBirth)).When(x => String.IsNullOrEmpty(x.LastName)).When(x => String.IsNullOrEmpty(x.FirstName)).WithMessage("At least one is required");
            this.RuleFor(x => x.LastName).NotEmpty().When(x => String.IsNullOrEmpty(x.Cdn)).When(x => String.IsNullOrEmpty(x.DateOfBirth)).When(x => String.IsNullOrEmpty(x.FirstName)).WithMessage("At least one is required");
            this.RuleFor(x => x.FirstName).NotEmpty().When(x => String.IsNullOrEmpty(x.Cdn)).When(x => String.IsNullOrEmpty(x.DateOfBirth)).When(x => String.IsNullOrEmpty(x.LastName)).WithMessage("At least one is required");
            this.RuleFor(x => x.DateOfBirth).NotEmpty().When(x => String.IsNullOrEmpty(x.Cdn)).When(x => String.IsNullOrEmpty(x.FirstName)).When(x => String.IsNullOrEmpty(x.LastName)).WithMessage("At least one is required");
*/
            this.RuleFor(x => x).Must(x => x.atLeastOne()).WithMessage("* At least one search criteria is required");

        }
    }
}
