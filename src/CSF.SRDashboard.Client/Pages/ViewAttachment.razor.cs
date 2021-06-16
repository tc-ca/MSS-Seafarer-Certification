using CSF.API.Services.Repositories;
using CSF.Common.Library.Azure;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class ViewAttachment
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

        public bool Found { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = GatewayService.GetApplicantInfoByCdn(Cdn);

            await GetDocumentAsync();

            this.EditContext = new EditContext(UploadedDocuments);

            StateHasChanged();
        }

        public void Edit()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + Cdn + "/edit-attachment/" + UploadedDocuments[0].DocumentId);
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
                Found = false;
                return;
            }

            var documentResult = await DocumentService.GetDocumentsWithDocumentIds(new List<Guid> { document.DocumentId });

            if (!documentResult.Any())
            {
                return;
            }

            var documentModel = documentResult.FirstOrDefault();

            var doc = new UploadedDocument
            {
                Cdn = this.Cdn,
                DocumentId = documentModel.DocumentId,
                Description = documentModel.Description,
                FileName = documentModel.FileName,
                Language = documentModel.Language,
                DownloadLink = await this.AzureBlobService.GetDownloadLinkAsync("documents", documentModel.DocumentUrl, DateTime.UtcNow.AddHours(8))

            };

            if (documentModel.DocumentTypes != null && documentModel.DocumentTypes.Any())
            {
                // To ensure we only show the types if we have them
                doc.DocumentTypes = documentModel.DocumentTypes;

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
