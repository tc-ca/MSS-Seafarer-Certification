using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using DSD.MSS.Blazor.Components.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSF.SRDashboard.Client.Models;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class CreateNewRequest
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }

        bool show = false;

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto { FullName = "Chris", Cdn = Cdn };

            //this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            RequestModel = new RequestModel
            {
                RequestID = "5",
                Cdn = "1"

            };
            this.EditContext = new EditContext(RequestModel);

            show = true;
            StateHasChanged();

        }

        public void SaveChanges()
        {

        }

    }
}
