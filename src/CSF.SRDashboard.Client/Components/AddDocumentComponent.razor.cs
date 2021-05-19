using CSF.API.Data.Entities;
using CSF.API.Services.Repositories;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
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
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class AddDocumentComponent
    {
        [Parameter]
        public IBrowserFile File { get; set; }
        public IFormFile FileToUpload { get; set; }
        [Parameter]
        public string ProfileName { get; set; }
        public string FileName { get { if (File == null) return String.Empty; return File.Name; } }
        public List<string> DocumentTypes { get; set; }
        [Parameter]
        public AddDocumentModel DocumentForm { get; set; }
        [Parameter]
        public EditContext EditContext { get; set; }
        [Inject]
        public SessionState State { get; set; }
        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }
        [Inject]
        public IDocumentService DocumentServe { get; set; }
        public DocumentInfo DocumentInfo { get; set; }
        public string MultipleSelectTitle { get; set; }
        private bool AccordionExpanded { get; set; } = true;
        public string ArrowClass { get; set; } = "fas fa-angle-down";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DocumentForm = new AddDocumentModel();
            this.MultipleSelectTitle = "Select";
        }

        private void OnInputChange(EventArgs e)
        {
         
        }
        private void HandleValidSubmit()
        {
            var isValid = EditContext.Validate();
            this.DocumentTypes = PopulateDocumentTypes(this.DocumentForm.DocumentTypeList);
            if(this.DocumentTypes.Count <= 0)
            {

            }
            Console.WriteLine("Valid Submit");
            if (this.File != null)
            {
                this.FileToUpload = this.PopulateFormFile(this.File);

                var result = documentService.InsertDocument(1, "John Wick", FileToUpload, string.Empty, "My Test file", "FAX", "EN", new List<string>(), string.Empty).ConfigureAwait(false).GetAwaiter().GetResult();
                this.DocumentInfo = PopulateDocumentInfo(this.State.mpdisApplicant);

                ClientXrefDocumentRepository.Insert(this.DocumentInfo);
            }
        }

        private string FormatSelectTitle(List<string> strings)
        {
            string formattedString="";
            foreach(var i in strings)
            {
                formattedString = i+" ";
            }
            return formattedString;
        }
        
        private List<string> PopulateDocumentTypes(List<SelectListItem> list)
        {
            List<string> DocumentTypes = new List<string>();
            foreach (var i in list)
            {
                if (i.Value)
                {
                    DocumentTypes.Add(i.Text);
                }
            }
            return DocumentTypes;
        }
        private DocumentInfo PopulateDocumentInfo(MpdisApplicantDto applicantInfo)
        {
            this.DocumentInfo = new DocumentInfo();

            this.DocumentInfo.Cdn = applicantInfo.Cdn;
            this.DocumentInfo.DocumentId = Guid.NewGuid();
            this.DocumentInfo.DateStartDte = DateTime.UtcNow;
            
            
            return DocumentInfo;

        }
        private FormFile PopulateFormFile(IBrowserFile file)
        {
            var result = ClientXrefDocumentRepository.GetDocumentsByCdn("00123456");

            Stream stream;
            stream = file.OpenReadStream(file.Size);

            FormFile FileToUpload = new FormFile(stream,0,file.Size,file.Name, file.Name);
            stream.Dispose();
            return FileToUpload;

        }
        private void UpdateSelectTitle()
        {
            Console.WriteLine("lol");
        }
        public void FlipArrow()
        {
            this.AccordionExpanded = !this.AccordionExpanded;
            if (this.AccordionExpanded) {
                this.ArrowClass = "fas fa-angle-down";
                    }
            else
            {
                this.ArrowClass = "fas fa-angle-right";
            }

        }
       public void RemoveAttachment()
        {
            this.File = null;
        }
    }
   
}
