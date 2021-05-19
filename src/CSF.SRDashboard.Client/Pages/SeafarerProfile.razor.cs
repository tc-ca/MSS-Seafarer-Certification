namespace CSF.SRDashboard.Client.Pages
{
    using CSF.API.Data.Entities;
    using CSF.API.Services.Repositories;
    using CSF.Common.Library.Azure;
    using CSF.SRDashboard.Client.DTO;
    using CSF.SRDashboard.Client.Models;
    using CSF.SRDashboard.Client.Services;
    using CSF.SRDashboard.Client.Services.Document;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public partial class SeafarerProfile
    {
       
        [Parameter]
        public string Cdn { get; set; }
        [Inject]
        public IGatewayService GatewayService { get; set; }
        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }
        [Inject]
        public IDocumentService DocumentService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        public IAzureBlobService AzureBlobService { get; set; }
        public MpdisApplicantDto Applicant { get; set; }

        public List<DocumentInfo> Documents { get; set; }

        public DateTime DOB;
        public bool ShowFilterHeader { get; set; } = true;

        protected List<Document> TableData = new List<Document>();

        public List<Services.Document.Entities.DocumentInfo> DocumentInfos { get; set; }

        public string currentRelativePath;

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.LoadData();
            var Documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();

            var documentIds = Documents.Select(x => x.DocumentId).ToList();

            // Call document servie to get info for each document
            DocumentInfos = await DocumentService.GetDocumentsWithDocumentIds(documentIds);

            foreach (var documentInfo in DocumentInfos)
            {

                var link = await this.AzureBlobService.GetDownloadLinkAsync("documents", documentInfo.DocumentUrl, DateTime.UtcNow.AddHours(8));

                this.TableData.Add(new Document
                {
                    DocumentId = documentInfo.DocumentId,
                    FileName = documentInfo.FileName,
                    Language = documentInfo.Language,
                    Type = documentInfo.FileType,
                    DateUploaded = documentInfo.DateCreated.Value,
                    DocumentUrl = documentInfo.DocumentUrl,
                    DownloadLink = link
                });
            }
        }

        protected void HandleHeaderFilterChanged()
        {
            StateHasChanged();
        }

        private void LoadData()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.state.mpdisApplicant = this.Applicant;

        }
    }
}