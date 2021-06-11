using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class ViewRequestDetails
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public int RequestId { get; set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public WorkItemDTO WorkItemDTO { get; set; }

        public RequestModel RequestModel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto();

            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);

            WorkItemDTO = this.WorkLoadService.GetByWorkItemById(RequestId);
            
            RequestModel = new RequestModel
            {
                RequestID = WorkItemDTO.Id,
                CertificateType = Constants.CertificateTypes.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.CertificateType, StringComparison.OrdinalIgnoreCase)).Single().ID,
                RequestType = Constants.RequestTypes.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.RequestType, StringComparison.OrdinalIgnoreCase)).Single().ID,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.SubmissionMethod, StringComparison.OrdinalIgnoreCase)).Single().ID
            };

            this.EditContext = new EditContext(RequestModel);

            StateHasChanged();
        }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }
    }
}
