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
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class CreateNewRequest
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

        public string Comment { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }
        public bool MostRecentCommentsIsCollapsed { get; private set; }

        public List<Dropdown> RequestTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "FAX" },
            new Dropdown { ID = "2", Text = "MAIL"},
            new Dropdown { ID = "3", Text = "EMAIL"},
            new Dropdown { ID = "4", Text = "EMER"}

        };

        public List<Dropdown> SubmissionMethods = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "New certificate" },
            new Dropdown { ID = "2", Text = "Renewal certificate" }

        };

        public List<Dropdown> CertificateTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "Marine Medical Cerficate - 2 year validity" }
        };


        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto();

            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            RequestModel = new RequestModel
            {
                Cdn = this.Cdn,
                SubmissionMethod = "1",
                CertificateType = "1",
                RequestType = "2"

            };
            this.EditContext = new EditContext(RequestModel);

            StateHasChanged();

        }

        public void SaveChanges()
        {
            var uploadedRequest = WorkLoadService.PostRequestModel(RequestModel, GatewayService);
        }

        private void SetMostRecentCommentsCollapseState()
        {
            MostRecentCommentsIsCollapsed = !MostRecentCommentsIsCollapsed;
        }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }
    }
}
