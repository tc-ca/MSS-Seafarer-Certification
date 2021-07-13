using CSF.SRDashboard.Client.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.PageValidators
{
    public class CommentValidator : AbstractValidator<RequestComment>
    {
        public CommentValidator() {
            this.RuleFor(i => i.Comment).NotEmpty().WithMessage("Please enter a comment before submitting");
                 }
    }
}
