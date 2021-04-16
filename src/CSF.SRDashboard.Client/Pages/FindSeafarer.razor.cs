using DSD.MSS.Blazor.Components.Table;
using DSD.MSS.Blazor.Components.Table.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Caching.Memory;
using MPDIS.API.Wrapper.Services.MPDIS;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class FindSeafarer
    {
        protected EditContext editContext;

        [Inject]
        public IMpdisService MpdisService { get; set; }
        protected Table<ApplicantSearchResultItem> TableRef { get; set; }
        protected List<ApplicantSearchResultItem> tableData = new List<ApplicantSearchResultItem>();
        public ApplicantSearchResult searchResult { get; set; }
        public string json { get; set; }
        public bool ShowFilterHeader { get; set; } = true;
        private const string tableSettingkey = "TableSettings";
        protected TableSettings<ApplicantSearchResultItem> tableSettings { get; set; }
        public ApplicantSearchCriteria searchCriteria = new ApplicantSearchCriteria();
        public bool showTable { get; set; } = true;
        public bool showForm { get; set; } = false;
        private IMemoryCache memoryCache;

        protected override void OnInitialized()
        {
            base.OnInitialized();
           
            editContext = new EditContext(searchCriteria);
            var isValid = editContext.Validate();

            //this.Search();
        }

        public void Search()
        {

            showTable = false;
            showForm = true;
            var result =  this.MpdisService.Search(searchCriteria);
            
            tableData = (List<ApplicantSearchResultItem>)LoadData(result);
            //TableRef.Update();

            json = JsonConvert.SerializeObject(result.Items, Formatting.Indented);

        }
       private IEnumerable<ApplicantSearchResultItem> LoadData(ApplicantSearchResult result) 
        {
        List<ApplicantSearchResultItem> list = result.Items;

            return list;
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
