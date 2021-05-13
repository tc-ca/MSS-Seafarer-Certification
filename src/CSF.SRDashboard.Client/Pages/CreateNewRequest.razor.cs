using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class CreateNewRequest
    {

        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        protected override void OnInitialized()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);

        }


    }
}
