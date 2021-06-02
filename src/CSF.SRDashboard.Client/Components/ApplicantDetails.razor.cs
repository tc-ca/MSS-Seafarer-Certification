using CSF.SRDashboard.Client.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class ApplicantDetails
    {
        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Applicant.Cdn);
        }

    }
}
