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
            this.RuleFor(x => x).Must(x => x.atLeastOne()).WithMessage("* At least one search criteria is required");

        }
    }
}
