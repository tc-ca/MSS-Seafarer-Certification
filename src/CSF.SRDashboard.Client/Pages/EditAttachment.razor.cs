using CSF.API.Services.Repositories;
using CSF.Common.Library.Azure;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class EditAttachment
    {
        protected EditContext EditContext;

        [Parameter]
        public string Cdn { get; set; }
        [Parameter]
        public Guid DocumentId { get; set; }

        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }

        [Inject]
        public IDocumentService DocumentService { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        [Inject]
        public IAzureBlobService AzureBlobService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public List<UploadedDocument> UploadedDocuments { get; set; } = new List<UploadedDocument>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = GatewayService.GetApplicantInfoByCdn(Cdn);

            await GetDocumentAsync();

            this.EditContext = new EditContext(this.UploadedDocuments);

            StateHasChanged();
        }

        public async void SaveChanges()
        {
            var isValid = EditContext.Validate();

            if (!isValid)
            {
                return;
            }

            var document = this.UploadedDocuments[0];

            document.DocumentTypes = document.DocumentTypeList.Where(x => x.Value).Select(d => new DocumentTypes { Id = d.Id, Description = d.Text }).ToList();

            document.Language = Constants.Languages.Where(x => x.ID.Equals(document.Language, StringComparison.OrdinalIgnoreCase)).Single().Text;

            var result = await this.DocumentService.UpdateMetadataForDocument(document.DocumentId, null, null, null, document.Description, null, document.Language, JsonConvert.SerializeObject(document.DocumentTypes));

            if (result == null)
            {
                return;
            }

            // Go to Seafarer profile and show message
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "?fileName=" + document.FileName);
        }

        /// <summary>
        /// Gets the document and adds it to the list
        /// </summary>
        /// <returns></returns>
        private async Task GetDocumentAsync()
        {
            var documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();

            var document = documents.Where(x => x.DocumentId == DocumentId).SingleOrDefault();

            if (document == null)
            {
                return;
            }

            var documentResult = await DocumentService.GetDocumentsWithDocumentIds(new List<Guid> { document.DocumentId });

            if (!documentResult.Any())
            {
                return;
            }

            var documentModel = documentResult.FirstOrDefault();

            if (documentModel.Language.Equals("EN"))
            {
                documentModel.Language = "English";
            }
            else if (documentModel.Language.Equals("FR"))
            {
                documentModel.Language = "French";
            }

            var doc = new UploadedDocument
            {
                Cdn = this.Cdn,
                DocumentId = documentModel.DocumentId,
                Description = documentModel.Description,
                FileName = documentModel.FileName,
                Language = Constants.Languages.Where(x => x.Text.Equals(documentModel.Language, StringComparison.OrdinalIgnoreCase)).Single().ID,
                DownloadLink = await this.AzureBlobService.GetDownloadLinkAsync("documents", documentModel.DocumentUrl, DateTime.UtcNow.AddHours(8))
            };

            if (documentModel.DocumentTypes != null && documentModel.DocumentTypes.Any())
            {
                foreach (var item in doc.DocumentTypeList)
                {

                    if (documentModel.DocumentTypes.Where(x => x.Id.Equals(item.Id)).SingleOrDefault() != null)
                    {
                        item.Value = true;
                    }
                }

            }

            UploadedDocuments.Add(doc);
        }
    }
}
