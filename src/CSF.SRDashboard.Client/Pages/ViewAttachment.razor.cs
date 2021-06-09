using CSF.API.Services.Repositories;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class ViewAttachment
    {
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

        public MpdisApplicantDto Applicant { get; set; }

        public List<UploadedDocument> UploadedDocuments { get; set; } = new List<UploadedDocument>();

        public bool Found { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.Applicant = GatewayService.GetApplicantInfoByCdn(Cdn);

            var documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();

            var document = documents.Where(x => x.DocumentId == DocumentId).SingleOrDefault();

            if (document == null)
            {
                Found = false;
                return;
            }

            var documentResult = await DocumentService.GetDocumentsWithDocumentIds(new List<Guid> { document.DocumentId });

            var x = documentResult[0];
            UploadedDocuments.Add(new UploadedDocument
            {
                Cdn = this.Cdn,
                DocumentId = x.DocumentId,
                Description = x.Description,
                FileName = x.FileName,
                Language = x.Language
            });
        }
    }
}
