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
            RuleFor(x => x.RequestType).NotEmpty().WithMessage("Request type is required");
            RuleFor(x => x.CertificateType).NotEmpty().WithMessage("Certificate type is required");
            RuleFor(x => x.SubmissionMethod).NotEmpty().WithMessage("Submission method is required");
            RuleFor(x => x.ProcessingPhase).NotEmpty().WithMessage("Processing phase is required");
        }
    }
}
