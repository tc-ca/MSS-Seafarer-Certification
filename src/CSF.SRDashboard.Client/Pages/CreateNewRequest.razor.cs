using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System.Threading.Tasks;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.PageValidators;
using CSF.SRDashboard.Client.Components.Icons.Constants;
using CSF.SRDashboard.Client.Components.Icons.Utilities;
using System.Text.Json;
using Microsoft.JSInterop;
using Microsoft.Extensions.Localization;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class CreateNewRequest
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public int RequestId { get; set; }

        [Parameter]
        public string EditRequestCdn { get; set; }

        [Parameter]
        public int EditRequestId { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }

        public RequestValidator validator = new RequestValidator();

        public WorkItemDTO UploadedRequest { get; set; }
        public bool IsEditMode { get; set; }

        public string Comment { get; set; }

        private string titleInfo { get; set; }

        public bool MostRecentCommentsIsCollapsed { get; private set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (this.EditRequestCdn != null)
            {
                IsEditMode = true;
                this.Applicant = this.GatewayService.GetApplicantInfoByCdn(EditRequestCdn);
                this.RequestModel = PopulateRequestmodel(EditRequestId, EditRequestCdn);
                RequestModel.Cdn = this.Applicant.Cdn;
                Cdn = this.Applicant.Cdn;
                titleInfo = this.Applicant.FullName +"-" + Localizer["EditRequest"] + " "+ RequestModel.RequestID;
            }
            else
            {
                this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
                titleInfo = this.Applicant.FullName + "-" + Localizer["AddRequest2"];
                RequestModel = new RequestModel
                {
                    Cdn = Applicant.Cdn
                };
            }

            this.EditContext = new EditContext(RequestModel);
            StateHasChanged();
        }

        public void SaveChanges()
        {
            var isValid = EditContext.Validate();
            if (!isValid)
            {
                return;
            }

            JS.InvokeAsync<string>("SetBusyCursor", null);
            if (IsEditMode)
            {
                var updatedWorkItem = WorkLoadService.UpdateWorkItemForRequestModel(RequestModel, GatewayService);
                this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + RequestModel.RequestID + "/" + Constants.Updated);
            }
            else
            {
                UploadedRequest = WorkLoadService.PostRequestModel(RequestModel, GatewayService);
                this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + UploadedRequest.Id);
            }
        }

        private void SetMostRecentCommentsCollapseState()
        {
            MostRecentCommentsIsCollapsed = !MostRecentCommentsIsCollapsed;
        }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }

        private RequestModel PopulateRequestmodel(int requestId, string cdn)
        {
            var workItem = this.WorkLoadService.GetByWorkItemById(requestId);
            var requestModel = new RequestModel();
            requestModel.Cdn = cdn;
            requestModel.RequestID = requestId;
            requestModel.Status = workItem.WorkItemStatus.StatusAdditionalDetails;

            if (workItem.Detail != null)
            {
                var detail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);
                requestModel.CertificateType = detail.CertificateType;
                requestModel.RequestType = detail.RequestType;
                requestModel.SubmissionMethod = detail.SubmissionMethod;
            }

            return requestModel;
        }
    }
}
