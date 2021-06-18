using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System.Threading.Tasks;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.PageValidators;
using System.Text.Json;
using Microsoft.JSInterop;
using Microsoft.Extensions.Localization;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class EditNewRequest
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }

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

            IsEditMode = true;
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.RequestModel = PopulateRequestmodel(EditRequestId, this.Applicant.Cdn);

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

            var RequestToSend = new RequestModel
            {
                RequestID = EditRequestId,
                Cdn = Applicant.Cdn,
                CertificateType = Constants.CertificateTypes.Where(x => x.ID.Equals(RequestModel.CertificateType)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.ID.Equals(RequestModel.RequestType)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.ID.Equals(RequestModel.SubmissionMethod)).Single().Text
            };

            var updatedWorkItem = WorkLoadService.UpdateWorkItemForRequestModel(RequestToSend, GatewayService);
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + RequestModel.RequestID + "/" + Constants.Updated);

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

            if (workItem.Detail != null)
            {
                var detail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);

                requestModel.CertificateType = Constants.CertificateTypes.Where(x => x.Text.Equals(detail.CertificateType)).Single().ID;
                requestModel.RequestType = Constants.RequestTypes.Where(x => x.Text.Equals(detail.RequestType)).Single().ID;
                requestModel.SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(detail.SubmissionMethod)).Single().ID;
            }

            return requestModel;
        }
    }
}
