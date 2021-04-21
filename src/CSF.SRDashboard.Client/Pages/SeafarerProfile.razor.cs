using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using MPDIS.API.Wrapper.Services.MPDIS;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Routing;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Table;
using DSD.MSS.Blazor.Components.Table.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }
        [Inject]
        public IMpdisService MpdisService { get; set; }
        public ApplicantInformation applicant = new ApplicantInformation();
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
        public SeafarerProfile()
        {

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.LoadData();
            TableData.Add(new Document
            {
                FileName = "mmeReport.pdf", 
                Language = "English", 
                RequestID = "123456",
                Type = "MME Report",
                DateUploaded =  new DateTime(2020,2,10)
            });

            TableData.Add(new Document
            {
                FileName = "EyeExam.pdf",
                Language = "French",
                RequestID = "654321",
                Type = "Test results",
                DateUploaded = new DateTime(2019, 12, 14)
            });
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
            DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(this.applicant.DateOfBith);
            DOB = offset.DateTime;
            fullDob = DOB.ToString("MMMM dd, yyyy");

        }
    }
}