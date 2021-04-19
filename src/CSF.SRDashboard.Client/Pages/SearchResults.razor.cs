using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using DSD.MSS.Blazor.Components.Table;
using DSD.MSS.Blazor.Components.Table.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using MPDIS.API.Wrapper.Services.MPDIS;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SearchResults
    {
        [Inject]
        public SessionState State { get; set; }
        [Inject]
        public IMpdisService MpdisService { get; set; }

        [Inject]
        public IResultsService resultService { get; set; }

        public ApplicantSearchResult ApplicantSearchResult { get; set; }
        protected TableSettings<ApplicantSearchResultItem> tableSettings { get; set; }
        public bool ShowFilterHeader { get; set; } = true;
        private const string tableSettingkey = "TableSettings";
        protected Table<ApplicantSearchResultItem> TableRef { get; set; }

        protected List<ApplicantSearchResultItem> TableData = new List<ApplicantSearchResultItem>();
        private readonly IMemoryCache memoryCache;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (State.ApplicantSearchResult == null)
            {
                State.ApplicantSearchResult = new ApplicantSearchResult();
            }

            ApplicantSearchResult = State.ApplicantSearchResult;

            TableData = ApplicantSearchResult.Items;

            StateHasChanged();
        }

        protected void OnAfterTableDataLoaded()
        {
            if (tableSettings != null)
            {
                TableRef.ResetTableSettings(tableSettings);
                tableSettings = null;
            }
        }
        public void OnFilterChanged(TableSettings<ApplicantSearchResultItem> settings)
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

    }
}

