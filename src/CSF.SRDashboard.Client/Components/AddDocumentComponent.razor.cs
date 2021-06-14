using CSF.API.Data.Entities;
using CSF.API.Services.Repositories;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.DTO.DocumentStorage;
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

namespace CSF.SRDashboard.Client.Components
{
    public partial class AddDocumentComponent
    {
        [Parameter]
        public string Cdn { get; set; }
       
        public IFormFile FileToUpload { get; set; }
        
        [Parameter]
        public string ProfileName { get; set; }
        
        public List<DocumentTypeDTO> DocumentTypes { get; set; }
        
        [Parameter]
        public AddDocumentModel DocumentForm { get; set; }
        
        [Parameter]
        public EditContext EditContext { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; private set; }
        
        [Inject]
        public SessionState State { get; set; }
        
        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }
        
        [Inject]
        public IDocumentService DocumentServe { get; set; }
        
        [Inject]
        public IGatewayService GatewayService { get; set; }
        
        public MpdisApplicantDto Applicant { get; set; }
        
        public DocumentInfo DocumentInfo { get; set; }
        
        public string MultipleSelectTitle { get; set; }
        
        public string Language { get; set; }
        
        public ValidationMessageStore ValidationMessageStore { get; private set; }
        
        private bool AccordionExpanded { get; set; } = true;
        
        public string FileName
        {
            get
            {
                if (FileToUpload == null)
                    return String.Empty;
                return FileToUpload.FileName;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            this.Applicant = new MpdisApplicantDto();
            this.ValidationMessageStore = new ValidationMessageStore(this.EditContext);
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.DocumentForm = new AddDocumentModel();
            this.MultipleSelectTitle = "Select";
        }

        private async void HandleValidSubmit()
        {
            this.DocumentTypes = PopulateDocumentTypes(this.DocumentForm.DocumentTypeList);
            var isValid = Validate();

            if (isValid)
            {
                var SelectedLanguage = this.DocumentForm.Languages[this.DocumentForm.SelectValue - 1].Text;
                this.Language = SelectedLanguage; 
                var addedDocumentIds = await DocumentServe.InsertDocument(1, "User", FileToUpload, string.Empty, this.DocumentForm.Description, "FAX", this.Language, this.DocumentTypes, string.Empty);
                if (addedDocumentIds.Count > 0)
                {
                    this.DocumentInfo = new DocumentInfo
                    {
                        Cdn = this.Applicant.Cdn,
                        DateStartDte = DateTime.UtcNow,
                        DocumentId = addedDocumentIds[0]
                    };
                   
                }
                ClientXrefDocumentRepository.Insert(this.DocumentInfo);
                this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Applicant.Cdn}");
            }
        } 

        /// <summary>
        /// Checks if the form is validated
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            if (this.DocumentForm.SelectValue <= -1)
            {
               return false;
            }
           
            if (this.DocumentTypes.Count <= 0)
            {
                return false;
            }

            if(this.FileToUpload == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// populates the list of document types from the form
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<DocumentTypeDTO> PopulateDocumentTypes(List<SelectListItem> list)
        {
            List<DocumentTypeDTO> DocumentTypes = new List<DocumentTypeDTO>();
            foreach (var i in list)
            {
                if (i.Value)
                {
                    DocumentTypes.Add(
                        new DocumentTypeDTO() { 
                            Id = i.Id,
                            Description = i.Text
                        });
                }
            }
            return DocumentTypes;
        }
        
        /// <summary>
        /// cancels and returns to the profile page
        /// </summary>
        public void HandleCancel()
        {
            this.FileToUpload = null;
            this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Cdn}");
        }

       /// <summary>
       /// Removes the attachment
       /// </summary>
        public void RemoveAttachment()
        {
            this.ValidationMessageStore.Clear();
            this.FileToUpload = null;
        }
    }
}