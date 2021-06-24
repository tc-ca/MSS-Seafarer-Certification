﻿using CSF.SRDashboard.Client.DTO;
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

        private string previousAssigneeId;

        private string titleInfo { get; set; }
        [Inject]
        public IDocumentService DocumentService { get; set; }
        public bool MostRecentCommentsIsCollapsed { get; private set; }
        public List<UploadedDocument> DocumentForm { get; set; } = new List<UploadedDocument>();
        public IUploadDocumentHelper UploadService { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsEditMode = true;
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.RequestModel = PopulateRequestmodel(EditRequestId, this.Applicant.Cdn);
            previousAssigneeId = this.RequestModel.AssigneeId;

            this.EditContext = new EditContext(RequestModel);
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

            var added = await this.InsertDocumentOnRequest();
            var RequestToSend = new RequestModel
            {
                RequestID = EditRequestId,
                Cdn = Applicant.Cdn,
                CertificateType = Constants.CertificateTypes.Where(x => x.Id.Equals(RequestModel.CertificateType)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.Id.Equals(RequestModel.RequestType)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Id.Equals(RequestModel.SubmissionMethod)).Single().Text,
                Status = Constants.RequestStatuses.Where(x => x.Id.Equals(RequestModel.Status)).Single().Text,
                AssigneeId = RequestModel.AssigneeId
            };

            var updatedWorkItem = WorkLoadService.UpdateWorkItemForRequestModel(RequestToSend, GatewayService);

            //Work load management service does not support updating an assignment within a work item.
            // we have to update the work item seperately.
            if(previousAssigneeId != this.RequestModel.AssigneeId)
            {
                var assignment = WorkLoadService.GetAssignmentFromRequestModel(RequestModel);

                // if the empty option is selected, we need to delete the assignee from the work request
                if(RequestModel.AssigneeId == null)
                {
                    var workItemId = RequestModel.RequestID;
                    var assignmentToBeDeleted = WorkLoadService.GetAssingmentByWorkItemId(workItemId);
                    WorkLoadService.UpdateAssignment(assignmentToBeDeleted, true);
                }
                else
                {
                    WorkLoadService.UpdateAssignment(assignment, false);
                }
            }
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + RequestModel.RequestID + "/" + Constants.Updated + "?tab=requestLink");

        }
        private async Task<List<Document>> InsertDocumentOnRequest()
        {
            List<Document> addedDocuments = new List<Document>();
            if(this.DocumentForm == null)
            {
                return addedDocuments;
            }
            foreach (var document in this.DocumentForm)
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
            return addedDocuments;
        }
    
        public void ViewProfile()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn);
        }

        public void Cancel()
        {
            // Go to Seafarer profile and show message
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
            }

            return requestModel;
        }
    }
}
