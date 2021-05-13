using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MPDIS.API.Wrapper.Services.MPDIS;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Table;
using DSD.MSS.Blazor.Components.Table.Models;
using Microsoft.Extensions.Caching.Memory;
using CSF.API.Services.Repositories;
using CSF.API.Data.Entities;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Services.Document;

namespace CSF.SRDashboard.Client.Pages
{
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

        public MpdisApplicantDto Applicant { get; set; }

        public List<DocumentInfo> Documents { get; set; }

        public DateTime DOB;
        protected TableSettings<Document> tableSettings { get; set; }
        public bool ShowFilterHeader { get; set; } = true;
        protected Table<Document> TableRef { get; set; }
        protected List<Document> TableData = new List<Document>();
        private readonly IMemoryCache memoryCache;

        public List<Services.Document.Entities.DocumentInfo> DocumentInfos { get; set; }
        public string currentRelativePath;

        protected override void OnInitialized()
        {
            this.LoadData();
            // Get all documents
            Documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();

            var documentIds = Documents.Select(x => x.DocumentId).ToList();


            // Call document servie to get info for each document
            DocumentInfos = DocumentService.GetDocumentsWithDocumentIds(documentIds).ConfigureAwait(false).GetAwaiter().GetResult();

            foreach (var x in DocumentInfos)
            {
                TableData.Add(new Document
                {
                    DocumentId = x.DocumentId,
                    FileName = x.FileName,
                    Language = x.Language,
                    Type = x.FileType,
                    DateUploaded = x.DateCreated.Value,
                    DocumentUrl = x.DocumentUrl
                });
            }

            //ClientXrefDocumentRepository.Insert(new DocumentInfo
            //{
            //    Cdn = Cdn,
            //    DocumentId = new Guid("74d98d84-eaab-450c-bbe2-9b8ba9025bc4"),
            //    DateStartDte = DateTime.UtcNow
            //});

        }

        protected void OnAfterTableDataLoaded()
        {
            if (tableSettings != null)
            {
                TableRef.ResetTableSettings(tableSettings);
                tableSettings = null;
            }
        }
        public void OnFilterChanged(TableSettings<Document> settings)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2)
            };

            if (TableRef != null)
            {
                memoryCache.Set("TableSettings", settings);
            }
        }
        protected void HandleHeaderFilterChanged()
        {
            StateHasChanged();
        }

        private void LoadData()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
        }
    }
}