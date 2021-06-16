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
using System.Collections.Generic;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;

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
        public SessionState State { get; set; }

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
        [Inject]
        public IWorkLoadManagementService WorkLoadManagementService { get; set; }

        private string titleInfo { get; set; }
        [Inject]
        public IDocumentService DocumentService { get; set; }
        public bool MostRecentCommentsIsCollapsed { get; private set; }
        public List<UploadedDocument> DocumentForm { get; set; } = new List<UploadedDocument>();
        public IUploadDocumentService UploadService { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsEditMode = true;
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.RequestModel = PopulateRequestmodel(EditRequestId, this.Applicant.Cdn);

            this.EditContext = new EditContext(RequestModel);
            this.UploadService = new UploadDocumentService(this.DocumentService);
            StateHasChanged();
        }

        public async void SaveChanges()
        {
            var isValid = EditContext.Validate();
            if (!isValid)
            {
                return;
            }

            await JS.InvokeAsync<string>("SetBusyCursor", null);
            var added = await this.InsertDocumentOnRequest();
            var RequestToSend = new RequestModel
            {
                RequestID = EditRequestId,
                Cdn = Applicant.Cdn,
                CertificateType = Constants.CertificateTypes.Where(x => x.ID.Equals(RequestModel.CertificateType)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.ID.Equals(RequestModel.RequestType)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Id.Equals(RequestModel.SubmissionMethod)).Single().Text, 
                Documents = added
            };

            var updatedWorkItem = WorkLoadService.UpdateWorkItemForRequestModel(RequestToSend, GatewayService);
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + RequestModel.RequestID + "/" + Constants.Updated);

        }
        private async Task<List<Document>> InsertDocumentOnRequest()
        {
            List<Document> addedDocuments = new List<Document>();
            if(this.State.DocumentForm.Count <= 0)
            {
                return addedDocuments;
            }
            foreach (var document in this.State.DocumentForm)
            {

                var addedDocument = await this.UploadService.UploadDocument(document);
                WorkItemAttachmentDTO workItemAttachmentDTO = new WorkItemAttachmentDTO() 
                { DocumentId = addedDocument.DocumentId, WorkItemId = this.EditRequestId };
                await this.WorkLoadManagementService.AddWorkItemAttachment(workItemAttachmentDTO);
                addedDocuments.Add(new Document()
                {
                    DocumentId = addedDocument.DocumentId,
                    FileName = document.FileName,
                    Language = document.Languages.Where(i => i.Id == document.SelectValue.ToString()).Select(i => i.Text).FirstOrDefault(),
                    RequestID = this.EditRequestId.ToString()
                });

            }
            return addedDocuments;
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
                requestModel.SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(detail.SubmissionMethod)).Single().Id;
            }

            return requestModel;
        }
    }
}
