﻿using CSF.Common.Library.Azure;
using CSF.SRDashboard.Client.DTO;
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
        public IAzureBlobService AzureBlobService { get; set; }

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

            WorkItemDTO = await this.WorkLoadService.GetByWorkItemById(RequestId);


            RequestModel = new RequestModel
            {
                RequestID = WorkItemDTO.Id,
                CertificateType = Constants.CertificateTypes.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.CertificateType, StringComparison.OrdinalIgnoreCase)).Single().Id,
                RequestType = Constants.RequestTypes.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.RequestType, StringComparison.OrdinalIgnoreCase)).Single().Id,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.SubmissionMethod, StringComparison.OrdinalIgnoreCase)).Single().Id,
                Status = Constants.RequestStatuses.Where(x => x.Text.Equals(WorkItemDTO.WorkItemStatus.StatusAdditionalDetails, StringComparison.OrdinalIgnoreCase)).Single().Id,
                Language = Constants.Languages.Where(x => x.Text.Equals(WorkItemDTO.ItemDetail.Language, StringComparison.OrdinalIgnoreCase)).Single().Id,
                ProcessingPhase = WorkItemDTO.ItemDetail.ProcessingPhase,
                DueDate = WorkItemDTO.ItemDetail.DueDate
            };

            RequestModel.AssigneeId = (WorkItemDTO.WorkItemAssignment == null) ? null : WorkItemDTO.WorkItemAssignment.AssignedEmployeeId;
            this.EditContext = new EditContext(RequestModel);

            var documentIds = (await this.WorkLoadService.GetAllAttachmentsByRequestId(RequestId)).Select(x => x.DocumentId).ToList();
            var documentInfos = await this.DocumentService.GetDocumentsWithDocumentIds(documentIds);
            foreach (var docFromDB in documentInfos)
            {
                var uploadLoaded = new UploadedDocument
                {
                    DocumentId = docFromDB.DocumentId,
                    Language = docFromDB.Language,
                    FileName = docFromDB.FileName,
                    DocumentTypes = docFromDB.DocumentTypes,
                    Description = docFromDB.Description, 
                    DownloadLink = await this.AzureBlobService.GetDownloadLinkAsync("documents", docFromDB.DocumentUrl, DateTime.UtcNow.AddHours(8))
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
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "?tab=requestLink");
        }
    }
}
