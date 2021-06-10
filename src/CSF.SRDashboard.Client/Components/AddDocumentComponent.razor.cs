using CSF.API.Data.Entities;
using CSF.API.Services.Repositories;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class AddDocumentComponent
    {


        [Inject]
        public NavigationManager NavigationManager { get; private set; }

        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }
        [Inject]
        public IWorkLoadManagementService WorkLoadManagementService { get; set; }
        [Inject]
        public IDocumentService DocumentServe { get; set; }

        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public bool AllowMultipleUploads { get; set; }

        [Parameter]
        public int RequestId { get; set; } = -1;
        public List<UploadedDocument> DocumentForm { get; set; } = new List<UploadedDocument>();

        public List<string> DocumentTypes { get; set; }

        public EditContext EditContext { get; set; }

        public DocumentInfo DocumentInfo { get; set; }

        public string Language { get; set; }

        public ValidationMessageStore ValidationMessageStore { get; private set; }

        public void FileUploaded()
        {
            this.StateHasChanged();
        }
        private async void HandleValidSubmit()
        {
            if (!string.IsNullOrEmpty(this.Cdn))
            {
                this.uploadToSeafarer();
            }
            else if (this.RequestId != -1)
            {
                this.uploadToWorkRequest();
            }

        }
        private async void uploadToWorkRequest()
        {
            var isValid = Validate();

            foreach (var document in this.DocumentForm)
            {

                var addedDocumentIds = await this.uploadDocument(document);
                WorkItemAttachmentDTO workItemAttachmentDTO = new WorkItemAttachmentDTO() { DocumentId = addedDocumentIds[0], WorkItemId = this.RequestId };
                await this.WorkLoadManagementService.AddWorkItemAttachment(workItemAttachmentDTO);
            }
        }



        private async void uploadToSeafarer()
        {
            var isValid = Validate();

            foreach (var document in this.DocumentForm)
            {

                var addedDocumentIds = await this.uploadDocument(document);
                if (addedDocumentIds.Count > 0)
                {
                    this.DocumentInfo = new DocumentInfo
                    {
                        Cdn = this.Cdn,
                        DateStartDte = DateTime.UtcNow,
                        DocumentId = addedDocumentIds[0]
                    };

                }
                ClientXrefDocumentRepository.Insert(this.DocumentInfo);
            }

            this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Cdn}");
        }
        private async Task<List<Guid>> uploadDocument(UploadedDocument document)
        {
            this.DocumentTypes = PopulateDocumentTypes(document.DocumentTypeList);
            var SelectedLanguage = document.Languages[document.SelectValue - 1].Text;
            this.Language = SelectedLanguage;
            var addedDocumentIds = await DocumentServe.InsertDocument(1, "User", document.FormFile, string.Empty, document.Description, string.Empty, this.Language, this.DocumentTypes, string.Empty);
            return addedDocumentIds;
        }
        /// <summary>
        /// Checks if the form is validated
        /// </summary>
        /// <returns></returns>
        private bool Validate() => !(this.DocumentForm.Count <= 0);

        /// <summary>
        /// populates the list of document types from the form
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<string> PopulateDocumentTypes(List<SelectListItem> documentType)
        {
            List<string> DocumentTypes = new List<string>();


            foreach (var i in documentType)
            {

                if (i.Value)
                {
                    DocumentTypes.Add(i.Text);
                }
            }

            return DocumentTypes;
        }

        /// <summary>
        /// cancels and returns to the profile page
        /// </summary>
        public void HandleCancel()
        {
            this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Cdn}");
        }

        /// <summary>
        /// Removes the attachment
        /// </summary>
        public void RemoveAttachment()
        {
            this.ValidationMessageStore.Clear();
        }
    }
}