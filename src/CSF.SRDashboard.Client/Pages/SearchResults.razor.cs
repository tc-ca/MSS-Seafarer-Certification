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
        public ApplicantSearchResult ApplicantSearchResult { get; set; }
        protected TableSettings<ApplicantSearchResultItem> tableSettings { get; set; }
        public bool ShowFilterHeader { get; set; } = false;
        public bool ShowFirstNameFilter { get; set; } = true;
        public bool ShowLastNameFilter { get; set; } = true;
        public bool ShowCDNFilter { get; set; } = true;
        public bool ShowDOBFilter { get; set; } = true;
        private const string tableSettingkey = "TableSettings";
        protected Table<ApplicantSearchResultItem> TableRef { get; set; }
        protected List<ApplicantSearchResultItem> TableData = new List<ApplicantSearchResultItem>();
        [Inject]
        public IMemoryCache memoryCache { get; set; }
        NavigationManager navigationManager { get; set; }
        public bool showTable { get; set; } = false;
        public bool showError { get; set; } = true;



        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (State.ApplicantSearchResult == null)
            {
                State.ApplicantSearchResult = new ApplicantSearchResult();
            }

            ApplicantSearchResult = State.ApplicantSearchResult;

            TableData = ApplicantSearchResult.Items;
            if (TableData.Count == 0)
            {
                showTable = true;
                showError = false;
            }
            else
            {

                StateHasChanged();
            }
        }

        protected void OnAfterTableDataLoaded()
        {
            if (tableSettings != null)
            {
                TableRef.ResetTableSettings(tableSettings);
                tableSettings = null;
            }
        }
        public void showProfile(string cdn)
        {
            navigationManager.NavigateTo($"SeafarerProfile/{cdn}");
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
        private void HandleHeaderFilterChanged()
        {
            StateHasChanged();

            
        }
        private void ToggleLastNameFilter()
        {
            ShowLastNameFilter = !ShowLastNameFilter;
        }
        private void ToggleFirstNameFilter()
        {
            ShowFirstNameFilter = !ShowFirstNameFilter;
        }
        private void ToggleCDNFilter()
        {
            ShowCDNFilter = !ShowCDNFilter;
        }
        private void ToogleDOBFilter()
        {
            ShowDOBFilter = ShowDOBFilter;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (memoryCache.TryGetValue("TableSettings", out TableSettings<ApplicantSearchResultItem> settings))
                {
                    tableSettings = settings;
                    TableRef.Update();
                }
            }
        }
    }
}

