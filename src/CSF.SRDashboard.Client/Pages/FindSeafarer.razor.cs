namespace CSF.SRDashboard.Client.Pages
{
    using CSF.SRDashboard.Client.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;
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

        [Inject]
        IJSRuntime JS { get; set; }

        public ApplicantSearchCriteria SearchCriteria = new ApplicantSearchCriteria();

        public bool IsSubmitting { get; set; } = false;

        public string ButtonDisabled { get; set; }

        public bool Error { get; set; } = true;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (this.State.SearchCriteria != null)
            {
                this.SearchCriteria = State.SearchCriteria;
            }

            this.State.ApplicantSearchResult = null;
            this.EditContext = new EditContext(this.SearchCriteria);
        }

        /// <summary>
        /// Runs a search after the criteria is met
        /// </summary>
        public void Search()
        {
            if(this.IsSubmitting)
                return;
            _ = JS.InvokeAsync<string>("DisableSeafarerSearchButton", null);
            this.IsSubmitting = true;
            this.ButtonDisabled = "disabled";
            this.State.SearchCriteria = SearchCriteria;
            this.State.ApplicantSearchResult = MpdisService.Search(this.SearchCriteria);
            this.NavigationManager.NavigateTo("/SearchResults");
        }

        /// <summary>
        /// Clears the search field
        /// </summary>
        public void Clear()
        {
            SearchCriteria = new ApplicantSearchCriteria();

            State.SearchCriteria = null;
        }
    }
}