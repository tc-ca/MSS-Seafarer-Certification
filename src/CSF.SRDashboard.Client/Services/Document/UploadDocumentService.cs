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
        public List<string> DocumentTypes { get; set; }
        public string Language { get; private set; }
        [Inject]
        public IDocumentService DocumentServe { get; set; }
        private List<UploadedDocument> DocumentForm { get; set; }

        public UploadDocumentService(List<UploadedDocument> uploadedDocuments)
        {
            this.DocumentForm = uploadedDocuments;
        }

        public async Task<List<Guid>> UploadDocument(UploadedDocument document)
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
    }
}
