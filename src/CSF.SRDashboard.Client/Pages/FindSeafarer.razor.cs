using CSF.SRDashboard.Client.Services;
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
        [Inject]
        public IResultsService resultService { get; set; }
        public ApplicantSearchResult searchResult { get; set; }
        public string json { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        public ApplicantSearchCriteria searchCriteria = new ApplicantSearchCriteria();
  
        protected override void OnInitialized()
        {
            base.OnInitialized();

            editContext = new EditContext(searchCriteria);
            var isValid = editContext.Validate();

            //this.Search();
        }

        public void Search()
        {
            var result = this.MpdisService.Search(searchCriteria);
            resultService.LoadData(result);
            navigationManager.NavigateTo("/SearchResults");
            json = JsonConvert.SerializeObject(result.Items, Formatting.Indented);

        }

    }
}
