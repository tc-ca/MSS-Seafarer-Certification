using CSF.SRDashboard.Client.DTO.DocumentStorage;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services.Document
{
    public class UploadDocumentService : IUploadDocumentService
    {
        public string Language { get; private set; }
        public IDocumentService DocumentServe { get; set; }
        private List<UploadedDocument> DocumentForm { get; set; }
        private List<DocumentTypeDTO> DocumentTypes { get; set; } = new List<DocumentTypeDTO>();
        
        public UploadDocumentService(IDocumentService documentService)
        {
            this.DocumentServe = documentService;
        }

        public async Task<List<Guid>> UploadDocument(UploadedDocument document)
        {
            this.DocumentTypes = this.PopulateDocumentTypes(document.DocumentTypeList);
            this.Language = document.Languages.Where(i => i.Id == document.SelectValue.ToString()).Select(i => i.Text).FirstOrDefault();
            var addedDocumentIds = await DocumentServe.InsertDocument(1, "User", document.FormFile, document.FormFile.ContentType, document.Description, string.Empty, this.Language, this.DocumentTypes, string.Empty);
            return addedDocumentIds;
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
