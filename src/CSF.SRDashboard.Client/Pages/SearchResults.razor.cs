using CSF.SRDashboard.Client.Services;
using DSD.MSS.Blazor.Components.Table;
using DSD.MSS.Blazor.Components.Table.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
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
        public IResultsService resultService { get; set; }
        protected TableSettings<ApplicantSearchResultItem> tableSettings { get; set; }
        public bool ShowFilterHeader { get; set; } = true;
        private const string tableSettingkey = "TableSettings";
        protected Table<ApplicantSearchResultItem> TableRef { get; set; }
        protected List<ApplicantSearchResultItem> tableData = new List<ApplicantSearchResultItem>();
        private IMemoryCache memoryCache;
       
        protected override void OnInitialized()
        {
            base.OnInitialized();
            tableData = resultService.list;

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

