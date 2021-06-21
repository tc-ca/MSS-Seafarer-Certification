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
using System;
using CSF.SRDashboard.Client.DTO.DocumentStorage;

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
        public int InitialDocumentCount { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsEditMode = true;
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.RequestModel = PopulateRequestmodel(EditRequestId, this.Applicant.Cdn);

            this.EditContext = new EditContext(RequestModel);
            this.UploadService = new UploadDocumentService(this.DocumentService);

            var documentIds = this.WorkLoadService.GetAllAttachmentsByRequestId(EditRequestId).Select(x => x.DocumentId).ToList();
            var documentInfos = await this.DocumentService.GetDocumentsWithDocumentIds(documentIds);
            this.DocumentForm = documentInfos.Select(x => new UploadedDocument()
            {
                DocumentId = x.DocumentId,
                Language = x.Language,
                FileName = x.FileName,
                DocumentTypes = x.DocumentTypes,
                Description = x.Description
            }).ToList();
            foreach( var Document in DocumentForm)
            {

                Document.SelectValue = int.Parse(Document.Languages.Where(x => x.Text.Equals(Document.Language, StringComparison.OrdinalIgnoreCase)).Single().Id);

                if (Document.Language.Equals("EN"))
                {
                    Document.Language = "English";
                }
                else if (Document.Language.Equals("FR"))
                {
                    Document.Language = "French";
                }

            }

            InitialDocumentCount = documentIds.Count; 


            StateHasChanged();
        }

        public void Cancel()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "?tab=requests");
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
                CertificateType = Constants.CertificateTypes.Where(x => x.Id.Equals(RequestModel.CertificateType)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.Id.Equals(RequestModel.RequestType)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Id.Equals(RequestModel.SubmissionMethod)).Single().Text,
                Status = Constants.RequestStatuses.Where(x => x.Id.Equals(RequestModel.Status)).Single().Text,
               
            };

            var updatedWorkItem = WorkLoadService.UpdateWorkItemForRequestModel(RequestToSend, GatewayService);


            for (int i = 0; i < InitialDocumentCount; i++)
            {
                var document = this.DocumentForm[i];

                document.DocumentTypes = document.DocumentTypeList.Where(x => x.Value).Select(d => new DocumentTypeDTO { Id = d.Id, Description = d.Text }).ToList();

                document.Language = document.Languages.Where(x => x.Id.Equals(document.SelectValue.ToString())).Single().Text;

                var result = await this.DocumentService.UpdateMetadataForDocument(document.DocumentId, null, null, null, document.Description, null, document.Language, document.DocumentTypes, null);
            }


            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + RequestModel.RequestID + "/" + Constants.Updated);

        }
        private async Task<List<Document>> InsertDocumentOnRequest()
        {
            List<Document> addedDocuments = new List<Document>();
            if(this.State.DocumentForm == null)
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
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "?tab=documents");
        }

        private RequestModel PopulateRequestmodel(int requestId, string cdn)
        {
            var workItem = this.WorkLoadService.GetByWorkItemById(requestId);
            var requestModel = new RequestModel();
            requestModel.Cdn = cdn;
            requestModel.RequestID = requestId;
            if(workItem.WorkItemStatus.StatusAdditionalDetails != null)
            {
                requestModel.Status = Constants.RequestStatuses.Where(x => x.Text.Equals(workItem.WorkItemStatus.StatusAdditionalDetails, StringComparison.OrdinalIgnoreCase)).Single().Id;
            }

            if (workItem.Detail != null)
            {
                var detail = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);

                requestModel.CertificateType = Constants.CertificateTypes.Where(x => x.Text.Equals(detail.CertificateType, StringComparison.OrdinalIgnoreCase)).Single().Id;
                requestModel.RequestType = Constants.RequestTypes.Where(x => x.Text.Equals(detail.RequestType, StringComparison.OrdinalIgnoreCase)).Single().Id;
                requestModel.SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(detail.SubmissionMethod, StringComparison.OrdinalIgnoreCase)).Single().Id;
            }

            return requestModel;
        }
    }
}
