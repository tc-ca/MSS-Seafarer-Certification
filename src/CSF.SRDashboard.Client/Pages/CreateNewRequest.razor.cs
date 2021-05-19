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

        public string Comment { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }

        public List<Dropdown> RequestTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "FAX" },
            new Dropdown { ID = "2", Text = "MAIL"},
            new Dropdown{ID = "3", Text = "EMAIL"},
            new Dropdown{ID = "4", Text = "FAX"}

        };

        public List<Dropdown> SubmissionMethods = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "New certificate" },
            new Dropdown { ID = "2", Text = "Renewal certificate" }

        };

        public List<Dropdown> CertificateTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "Pdf" },
            new Dropdown { ID = "2", Text = "doc" }

        };

        bool show = false;

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto { FullName = "Chris Ikongo", Cdn = Cdn };

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
