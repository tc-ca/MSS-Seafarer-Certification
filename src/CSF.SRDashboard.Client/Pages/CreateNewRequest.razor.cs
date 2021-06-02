using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services;
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
using CSF.SRDashboard.Client.PageValidators;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class CreateNewRequest
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public int RequestId { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Comment { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }

        public RequestValidator validator = new RequestValidator();

        public WorkItemDTO UploadedRequest { get; set; }

        public bool MostRecentCommentsIsCollapsed { get; private set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto();

            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);

            if(RequestId.Equals(0))
            {
                RequestModel = new RequestModel
                {
                    Cdn = Applicant.Cdn
                };
            }
            else
            {
                RequestModel = PopulateRequestmodel(this.Applicant);
            }

           

            this.EditContext = new EditContext(RequestModel);

            StateHasChanged();
        }

        public void SaveChanges()
        {
            var isValid = EditContext.Validate();

            UploadedRequest = WorkLoadService.PostRequestModel(RequestModel, GatewayService);

            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn +"/" +UploadedRequest.Id);
        }

        private void SetMostRecentCommentsCollapseState()
        {
            MostRecentCommentsIsCollapsed = !MostRecentCommentsIsCollapsed;
        }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }

        public RequestModel PopulateRequestmodel(MpdisApplicantDto Applicant)
        {
            var WorkItem = this.WorkLoadService.GetByWorkItemById(RequestId);

            RequestModel = new RequestModel
            {
                RequestID = WorkItem.Id,
                CertificateType = Constants.CertificateTypes.Where(x => x.Text.Equals(WorkItem.ItemDetail.CertificateType)).Single().ID,
                RequestType = Constants.RequestTypes.Where(x => x.Text.Equals(WorkItem.ItemDetail.RequestType)).Single().ID,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(WorkItem.ItemDetail.SubmissionMethod)).Single().ID
            };

            return RequestModel;
        }
    }
}
