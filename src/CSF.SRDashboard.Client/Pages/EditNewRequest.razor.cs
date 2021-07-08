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
using DSD.MSS.Blazor.Components.Core.Models;
using CSF.SRDashboard.Client.DTO.DocumentStorage;
using CSF.SRDashboard.Client.Services.WorkloadRequest;

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

        public int InitialDocumentCount { get; set; }

        public int CurrentDocumentNum { get; set; }

        private string previousAssigneeId;

        private string titleInfo { get; set; }
        [Inject]
        public IDocumentService DocumentService { get; set; }
        public bool MostRecentCommentsIsCollapsed { get; private set; }
        public List<UploadedDocument> DocumentForm { get; set; } = new List<UploadedDocument>();
        public IUploadDocumentHelper UploadService { get; set; }
        public List<StatusHistoryItem> StatusHistories { get; set; } = new List<StatusHistoryItem>();
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsEditMode = true;
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.RequestModel = PopulateRequestmodel(EditRequestId, this.Applicant.Cdn);
            previousAssigneeId = this.RequestModel.AssigneeId;

            this.EditContext = new EditContext(RequestModel);
            this.UploadService = new UploadDocumentHelper(this.DocumentService);

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
            foreach (var Document in DocumentForm)
            {
                if (Document.Language.Equals("EN"))
                {
                    Document.Language = "English";
                }
                else if (Document.Language.Equals("FR"))
                {
                    Document.Language = "French";
                }

                Document.Language = Constants.Languages.Where(x => x.Text.Equals(Document.Language, StringComparison.OrdinalIgnoreCase)).Single().Id;
            }
            this.StatusHistories = RequestModel.StatusHistories;
            this.RequestModel.UploadedDocuments = DocumentForm;
            InitialDocumentCount = documentIds.Count;
            this.UploadService = new UploadDocumentHelper(this.DocumentService);
            StateHasChanged();
        }

        public async void SaveChanges()
        {
            if (this.RequestModel.UploadedDocuments != null)
            {
                this.DocumentForm = this.RequestModel.UploadedDocuments;
            }
            var isValid = EditContext.Validate();

            if (!isValid)
            {
                return;
            }

            await JS.InvokeAsync<string>("SetBusyCursor", null);
            if (!this.UploadService.ValidateUpload(this.DocumentForm))
            {
                return;
            }

            ProcessingPhaseUtility processingPhaseUtility = new ProcessingPhaseUtility();

            var RequestToSend = new RequestModel
            {
                RequestID = EditRequestId,
                Cdn = Applicant.Cdn,
                CertificateType = Constants.CertificateTypes.Where(x => x.Id.Equals(RequestModel.CertificateType)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.Id.Equals(RequestModel.RequestType)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Id.Equals(RequestModel.SubmissionMethod)).Single().Text,
                Status = Constants.RequestStatuses.Where(x => x.Id.Equals(RequestModel.Status)).Single().Text,
                ProcessingPhase = processingPhaseUtility.FindProcessingPhaseById(RequestModel),
                AssigneeId = RequestModel.AssigneeId
            };

            var updatedWorkItem = WorkLoadService.UpdateWorkItemForRequestModel(RequestToSend, GatewayService);

            CurrentDocumentNum = 0;

            foreach (var Document in DocumentForm)
            {
                Document.DocumentTypes = Document.DocumentTypeList.Where(x => x.Value).Select(d => new DocumentTypeDTO { Id = d.Id, Description = d.Text }).ToList();

                if (CurrentDocumentNum < InitialDocumentCount)
                {
                    Document.Language = Constants.Languages.Where(x => x.Id.Equals(Document.Language, StringComparison.OrdinalIgnoreCase)).Single().Text;
                    var result = await this.DocumentService.UpdateMetadataForDocument(Document.DocumentId, null, null, null, Document.Description, null, Document.Language, Document.DocumentTypes, null);
                }
                else
                {
                    var added = await this.InsertDocumentOnRequest();
                }

                CurrentDocumentNum++;
            }

            if (previousAssigneeId != this.RequestModel.AssigneeId)
            {
                var new_assignment_toPost = WorkLoadService.GetAssignmentFromRequestModel(RequestModel);

                // when blank option is selected, we need to delete the old assignee from the work request
                if (RequestModel.AssigneeId == Constants.NotSelected)
                {
                    var workItemId = RequestModel.RequestID;
                    var oldAssignment_toDelete = WorkLoadService.GetMostRecentAssingmentForWorkItem(workItemId);
                    WorkLoadService.DeleteOrPost(oldAssignment_toDelete, true);
                }
                else
                {
                    // old assignee is deleted and the new assignee is posted.
                    var workItemId = RequestModel.RequestID;
                    var oldAssignment_toDelete = WorkLoadService.GetMostRecentAssingmentForWorkItem(workItemId);
                    if (oldAssignment_toDelete != null) // this is the scenario where there is no old assignee and we are assigning a person
                    {
                        WorkLoadService.DeleteOrPost(oldAssignment_toDelete, true);
                    }
                    WorkLoadService.DeleteOrPost(new_assignment_toPost, false);
                }
            }

            this.DocumentForm = null;
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + RequestModel.RequestID + "/" + Constants.Updated + "?tab=requestLink");

        }
        private async Task<List<Document>> InsertDocumentOnRequest()
        {
            List<Document> addedDocuments = new List<Document>();
            if(this.DocumentForm == null)
            {
                return addedDocuments;
            }

            CurrentDocumentNum = 0;
            foreach (var document in this.DocumentForm)
            {
               
                if (CurrentDocumentNum >= InitialDocumentCount)
                {
                    var addedDocument = await this.UploadService.UploadDocument(document);
                    if (addedDocument != null)
                    {
                        WorkItemAttachmentDTO workItemAttachmentDTO = new WorkItemAttachmentDTO()
                        { DocumentId = addedDocument.DocumentId, WorkItemId = this.EditRequestId };
                        await this.WorkLoadService.AddWorkItemAttachment(workItemAttachmentDTO);
                    }
                    else
                    {
                        return null;
                    }
                }
                CurrentDocumentNum++;
            }
            return addedDocuments;
        }

        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }

        public void Cancel()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "?tab=requestLink");
        }

        private RequestModel PopulateRequestmodel(int requestId, string cdn)
        {
            var workItem = this.WorkLoadService.GetByWorkItemById(requestId);
            var requestModel = new RequestModel();
            requestModel.Cdn = cdn;
            requestModel.RequestID = requestId;

            if(workItem.WorkItemAssignment != null)
            {
                requestModel.AssigneeId = workItem.WorkItemAssignment.AssignedEmployeeId;
            }

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
                if (detail.ProcessingPhase != null)
                {
                    requestModel.ProcessingPhase = GetProcessingPhase(requestModel, detail.ProcessingPhase);
                }
            }
            requestModel.StatusHistories = workItem.ItemDetail.Status;
            return requestModel;
        }

       

        /// <summary>
        /// Gets Processing Phase id from table Processing Phase text
        /// </summary>
        public string GetProcessingPhase(RequestModel requestModel, String ProcessingPhase)
        {
            if (requestModel.Status.Equals(Constants.RequestStatuses[0].Id))
            {
                return Constants.ProcessingPhaseNew.Where(x => x.Text.Equals(ProcessingPhase)).Single().Id;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[1].Id))
            {
                return Constants.ProcessingPhaseInProgress.Where(x => x.Text.Equals(ProcessingPhase)).Single().Id;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[2].Id))
            {
                return Constants.ProcessingPhasePending.Where(x => x.Text.Equals(ProcessingPhase)).Single().Id;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[3].Id))
            {
                return Constants.ProcessingPhaseComplete.Where(x => x.Text.Equals(ProcessingPhase)).Single().Id;
            }
            else if (requestModel.Status.Equals(Constants.RequestStatuses[4].Id))
            {
                return Constants.ProcessingPhaseCancelled.Where(x => x.Text.Equals(ProcessingPhase)).Single().Id;
            }
            else
            {
                return null;
            }
        }
    }
}
