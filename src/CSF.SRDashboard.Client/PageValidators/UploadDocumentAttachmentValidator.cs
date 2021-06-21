﻿using CSF.SRDashboard.Client.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.PageValidators
{
    public class UploadDocumentAttachmentValidator : AbstractValidator<UploadedDocument>
    {
        public UploadDocumentAttachmentValidator()
        {
            this.RuleFor(x => x.Language).NotEmpty().WithMessage("Please select a value");
            this.RuleFor(x => x.DocumentTypeList).Must(x => x.Any(y => y.Value == true)).WithMessage("Please select at least one value");
        }
     }
}
