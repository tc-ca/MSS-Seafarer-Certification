using CSF.SRDashboard.Client.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.PageValidators
{
    public class AddAttachmentValidator : AbstractValidator<AddDocumentModel>
    {
        public AddAttachmentValidator()
        {
            this.RuleFor(x => x.SelectValue).GreaterThan(-1).WithMessage("Please select at least one value");

        }
    }
}
