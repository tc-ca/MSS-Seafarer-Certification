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

            this.RuleFor(x => x).Cascade(CascadeMode.Stop).NotEmpty();
           

        }
    }
}
