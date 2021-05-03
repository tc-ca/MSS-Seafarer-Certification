using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using MPDIS.API.Wrapper.Services.MPDIS;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class FindSeafarer
    {
        protected EditContext EditContext;

        [Inject]
        public SessionState State { get; set; }

        [Inject]
        public IMpdisService MpdisService { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public ApplicantSearchResult SearchResult { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public ApplicantSearchCriteria SearchCriteria = new ApplicantSearchCriteria();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (State.SearchCriteria != null)
            {
                SearchCriteria = State.SearchCriteria;
            }

            State.ApplicantSearchResult = null;
            EditContext = new EditContext(SearchCriteria);
        }

        public void Search()
        {
            State.SearchCriteria = SearchCriteria;
            
            var searchResult = GatewayService.Search(SearchCriteria);
            State.ApplicantSearchResult = searchResult;

            NavigationManager.NavigateTo("/SearchResults");
        }

    }
}
