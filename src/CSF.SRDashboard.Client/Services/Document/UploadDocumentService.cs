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

        public UploadDocumentService()
        {
           
        }
        public async Task<List<Guid>> UploadDocument(UploadedDocument document)
        {
           
            this.DocumentTypes = document.DocumentTypeList.Where(i => i.Value == true).Select(i => i.Text).ToList();
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
    }
}
