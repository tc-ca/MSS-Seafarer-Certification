namespace CSF.SRDashboard.Client.Pages
{
    using CSF.SRDashboard.Client.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using MPDIS.API.Wrapper.Services.MPDIS;
    using MPDIS.API.Wrapper.Services.MPDIS.Entities;

    public partial class FindSeafarer
    {
        protected EditContext EditContext;

        [Inject]
        public SessionState State { get; set; }

        [Inject]
        public IMpdisService MpdisService { get; set; }

        public ApplicantSearchResult SearchResult { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public ApplicantSearchCriteria SearchCriteria = new ApplicantSearchCriteria();

        public string buttonDisabled { get; set; }

        public bool error { get; set; } = true;

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

          /// <summary>
          /// Runs a search after the criteria is met
          /// </summary>
        public void Search()
        {
                buttonDisabled = "disabled";
                //State.SearchCriteria = SearchCriteria;
                //State.ApplicantSearchResult = MpdisService.Search(SearchCriteria);
                //NavigationManager.NavigateTo("/SearchResults");
        }     
    }
}