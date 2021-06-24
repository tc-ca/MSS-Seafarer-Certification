﻿using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public IDocumentService DocumentService { get; set; }
        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public WorkItemDTO WorkItemDTO { get; set; }

        public RequestModel RequestModel { get; set; }
        public List<UploadedDocument> UploadedDocuments { get; set; } = new List<UploadedDocument>();


        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto();

            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);

            WorkItemDTO = this.WorkLoadService.GetByWorkItemById(RequestId);

            RequestModel = new RequestModel();
            RequestModel.RequestID = WorkItemDTO.Id;
            if (WorkItemDTO.Detail != null)
            {
                var detail = JsonSerializer.Deserialize<WorkItemDetail>(WorkItemDTO.Detail);
                RequestModel.CertificateType = Constants.CertificateTypes.Where(x => x.Text.Equals(detail.CertificateType, StringComparison.OrdinalIgnoreCase)).Single().Id;
                RequestModel.RequestType = Constants.RequestTypes.Where(x => x.Text.Equals(detail.RequestType, StringComparison.OrdinalIgnoreCase)).Single().Id;
                RequestModel.SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(detail.SubmissionMethod, StringComparison.OrdinalIgnoreCase)).Single().Id;
            }           
            RequestModel.Status = Constants.RequestStatuses.Where(x => x.Text.Equals(WorkItemDTO.WorkItemStatus.StatusAdditionalDetails, StringComparison.OrdinalIgnoreCase)).Single().Id;
            RequestModel.AssigneeId = (WorkItemDTO.WorkItemAssignment == null) ? null : WorkItemDTO.WorkItemAssignment.AssignedEmployeeId;

            this.EditContext = new EditContext(RequestModel);

            var documentIds = this.WorkLoadService.GetAllAttachmentsByRequestId(RequestId).Select(x => x.DocumentId).ToList();
            var documentInfos = await this.DocumentService.GetDocumentsWithDocumentIds(documentIds);
            foreach (var docFromDB in documentInfos)
            {
                var uploadLoaded = new UploadedDocument
                {
                    DocumentId = docFromDB.DocumentId,
                    Language = docFromDB.Language,
                    FileName = docFromDB.FileName,
                    DocumentTypes = docFromDB.DocumentTypes,
                    Description = docFromDB.Description
                };
                if (docFromDB.DocumentTypes != null && docFromDB.DocumentTypes.Any())
                {
                    // To ensure we only show the types if we have them
                    uploadLoaded.DocumentTypes = docFromDB.DocumentTypes;
                    foreach (var item in uploadLoaded.DocumentTypeList)
                    {
                        if (docFromDB.DocumentTypes.Where(x => x.Id.Equals(item.Id)).SingleOrDefault() != null)
                        {
                            item.Value = true;
                        }
                    }
                }
                this.UploadedDocuments.Add(uploadLoaded);
            }

            StateHasChanged();
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
    }
}
