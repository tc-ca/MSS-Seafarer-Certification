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

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IMpdisService MpdisService { get; set; }

        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }

        public ApplicantInformation applicant = new ApplicantInformation();

        public List<DocumentInfo> Documents { get; set; }

        public DateTime DOB;
        protected TableSettings<Document> tableSettings { get; set; }
        public bool ShowFilterHeader { get; set; } = true;
        protected Table<Document> TableRef { get; set; }
        protected List<Document> TableData = new List<Document>();
        private readonly IMemoryCache memoryCache;
        public string fullDob { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        public string currentRelativePath;


        protected override void OnInitialized()
        {
            this.LoadData();
            // Get all documents
            Documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();

            //TODO:: Call document servie to get info for each document

            foreach (var x in Documents)
            {
                TableData.Add(new Document
                {
                    FileName = x.DocumentId.ToString(),
                    Language = "English",
                    RequestID = "123456",
                    Type = "MME Report",
                    DateUploaded = x.DateStartDte
                });
            }
            //ClientXrefDocumentRepository.Insert(new API.Data.Entities.DocumentInfo { Cdn = Cdn, DocumentId = new Guid("483584a1-05e8-44f4-9e86-0eb11533bdff"), DateStartDte = DateTime.UtcNow });

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
            this.applicant = this.MpdisService.GetApplicantByCdn(Cdn);
        }
    }
}