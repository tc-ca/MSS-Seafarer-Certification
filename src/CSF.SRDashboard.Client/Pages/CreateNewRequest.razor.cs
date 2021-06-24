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
using System.Collections.Generic;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;
using DSD.MSS.Blazor.Components.Core.Models;

using System;

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

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        [Inject]
        public IDocumentService DocumentService { get; set; }

        [Inject]
        public SessionState State { get; set; }

        public IUploadDocumentHelper UploadService { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public RequestModel RequestModel { get; set; }

        public RequestValidator validator = new RequestValidator();


        public List<UploadedDocument> DocumentForm { get; set; } = new List<UploadedDocument>();

        public WorkItemDTO UploadedRequest { get; set; }

        public string Comment { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = new MpdisApplicantDto();

            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);

            RequestModel = new RequestModel
            {
                Cdn = Applicant.Cdn,
            };

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

            if (!this.UploadService.ValidateUpload(this.DocumentForm))
            {
                return;
            }

            var RequestToSend = new RequestModel
            {
                Cdn = Applicant.Cdn,
                CertificateType = Constants.CertificateTypes.Where(x => x.Id.Equals(RequestModel.CertificateType, StringComparison.OrdinalIgnoreCase)).Single().Text,
                RequestType = Constants.RequestTypes.Where(x => x.Id.Equals(RequestModel.RequestType, StringComparison.OrdinalIgnoreCase)).Single().Text,
                SubmissionMethod = Constants.SubmissionMethods.Where(x => x.Id.Equals(RequestModel.SubmissionMethod, StringComparison.OrdinalIgnoreCase)).Single().Text,
                Status = Constants.RequestStatuses.Where(x => x.Id.Equals(RequestModel.Status)).Single().Text,
                AssigneeId = RequestModel.AssigneeId
            };

            UploadedRequest = WorkLoadService.PostRequestModel(RequestToSend, GatewayService);
            var addedDocuments = await this.InsertDocumentOnRequest(UploadedRequest.Id);
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/" + UploadedRequest.Id);
        }

        private async Task<List<Document>> InsertDocumentOnRequest(int id)
        {
            List<Document> addedDocuments = new List<Document>();
           
            if (this.DocumentForm == null)
            {
                return addedDocuments;
            }
          
            foreach (var document in this.DocumentForm)
            {
                var addedDocument = await this.UploadService.UploadDocument(document);

                if (addedDocument != null)
                {
                    WorkItemAttachmentDTO workItemAttachmentDTO = new WorkItemAttachmentDTO()
                    { DocumentId = addedDocument.DocumentId, WorkItemId = id };
                    await this.WorkLoadService.AddWorkItemAttachment(workItemAttachmentDTO);
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

    }
}
