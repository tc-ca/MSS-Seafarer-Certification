using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;

namespace CSF.SRDashboard.Client.PageValidators
{
    /// <summary>
    /// Validator for the search page
    /// </summary>
    public class SearchValidator : AbstractValidator<ApplicantSearchCriteria>
    {
        public SearchValidator()
        {
            this.RuleSet("Criteria", () => { this.RuleFor(x => x).Must(x => x.atLeastOne()); });

            this.RuleSet("NoMatch", () =>
            {
                this.RuleFor(x => x.Cdn).Empty().When(x => x.IsInvalid).WithMessage("Sorry, we couldn't find that CDN. Try another?");
                this.RuleFor(x => x.LastName).Empty().When(x => x.IsInvalid).WithMessage("Sorry, we couldn't find that Last Name. Try another?");
                this.RuleFor(x => x.DateOfBirth).Empty().When(x => x.IsInvalid).WithMessage("Sorry, we couldn't find that Date Of Birth. Try another?");
                this.RuleFor(x => x.FirstName).Empty().When(x => x.IsInvalid).WithMessage("Sorry, we couldn't find that First Name. Try another?");
            });
            
        }
    }
}
