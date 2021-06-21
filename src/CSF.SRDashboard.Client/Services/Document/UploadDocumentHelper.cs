using CSF.SRDashboard.Client.DTO.DocumentStorage;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services.Document.Entities;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services.Document
{
    public class UploadDocumentHelper : IUploadDocumentHelper
    {
        public string Language { get; private set; }
        public IDocumentService DocumentServe { get; set; }
        private List<UploadedDocument> DocumentForm { get; set; }
        private List<DocumentTypeDTO> DocumentTypes { get; set; } = new List<DocumentTypeDTO>();
        
        public UploadDocumentHelper(IDocumentService documentService)
        {
            this.DocumentServe = documentService;
        }

        public async Task<DocumentInfo> UploadDocument(UploadedDocument document)
        {
            this.DocumentTypes = this.PopulateDocumentTypes(document.DocumentTypeList);
            
            if(this.DocumentTypes.Count <= 0)
            {
                return null;
            }
            var language = Constants.Languages.Where(x => x.Id.Equals(document.Language, StringComparison.OrdinalIgnoreCase)).Single().Text;
          
            if(language == null)
            {
                return null;
            }
            var documentInfo = await DocumentServe.InsertDocument(1, "User", document.FormFile, document.FormFile.ContentType, document.Description, string.Empty, language, this.DocumentTypes, string.Empty);
            return documentInfo;
        }

        private bool ValidateTypes(UploadedDocument upload)
        {
            var typeList = upload.DocumentTypeList.Where(i => i.Value).ToList();
            return typeList.Any();
        }
        public bool ValidateUpload(List<UploadedDocument> upload)
        {
            var language = upload.Where(i => string.IsNullOrEmpty(i.Language) || string.Equals(i.Language, "-1")).Select(i => i.Language).ToList();
            if (upload == null)
            {
                return true;
            }

            foreach (var i in upload)
            {
                if (!this.ValidateTypes(i))
                {
                    return false;
                }

            }
            if (language.Any())
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Checks if the form is validated
        /// </summary>
        /// <returns></returns>
        private bool Validate() => !(this.DocumentForm.Count <= 0);
        private List<DocumentTypeDTO> PopulateDocumentTypes(List<SelectListItem> selectListItems)
        {
            List<DocumentTypeDTO> documentTypes = new List<DocumentTypeDTO>();
            foreach(var i in selectListItems)
            {
                if (i.Value)
                {
                    documentTypes.Add(
                        new DocumentTypeDTO(){
                            Id = i.Id,
                            Description = i.Text
                        });
                }
            }
            return documentTypes;
        }
    }
}
