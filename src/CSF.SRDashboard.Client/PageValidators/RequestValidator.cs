using CSF.SRDashboard.Client.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.PageValidators
{
    public class RequestValidator : AbstractValidator<RequestModel>
    {

        public RequestValidator()
        {
            RuleFor(x => x.RequestType).NotEmpty();
            RuleFor(x => x.CertificateType).NotEmpty();
            RuleFor(x => x.SubmissionMethod).NotEmpty();
            RuleFor(x => x.ApplicantFullName).NotEmpty();
            RuleFor(x => x.Cdn).NotEmpty();
            RuleFor(x => x.Cdn).NotEmpty();
            RuleFor(x => x.Cdn).NotEmpty();

        }


    }
}
