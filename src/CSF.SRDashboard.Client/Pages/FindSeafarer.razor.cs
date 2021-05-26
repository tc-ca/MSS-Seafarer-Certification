using CSF.SRDashboard.Client.PageValidators;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using FluentValidation;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class FindSeafarer
    {
        protected EditContext EditContext;

        [Inject]
        public SessionState State { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public ApplicantSearchResult SearchResult { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        public ApplicantSearchCriteria SearchCriteria = new ApplicantSearchCriteria();

        public bool IsSubmitting { get; set; } = false;

        public string ButtonDisabled { get; set; }

        public SearchValidator validator = new SearchValidator();

        public SearchErrorObject SearchError = new SearchErrorObject();
      
        public bool Error { get; set; } = true;
        public ValidationMessageStore ValidationMessageStore { get; private set; }

        public string CssError { get; set; }

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
            var result = this.validator.Validate(this.SearchCriteria, options => options.IncludeRuleSets("Criteria"));
            if (result.IsValid)
            {
                this.SearchError.HideError();
                if (this.IsSubmitting)
                    return;
                _ = JS.InvokeAsync<string>("DisableSeafarerSearchButton", null);
                this.IsSubmitting = true;
                this.ButtonDisabled = "disabled";
                this.State.SearchCriteria = this.SearchCriteria;
                this.State.ApplicantSearchResult = GatewayService.SearchForApplicants(this.SearchCriteria);
                if (this.State.ApplicantSearchResult != null && this.State.ApplicantSearchResult.TotalCount > 0)
                {
                    this.NavigationManager.NavigateTo("/SearchResults");
                }
                else
                {
                    this.IsSubmitting = false;
                    this.EditContext = new EditContext(this.SearchCriteria);
                    
                    this.SearchCriteria.IsInvalid = true;
                    
                    var results = this.validator.Validate(this.SearchCriteria, options => options.IncludeRuleSets("NoMatch"));
                   
                    if (this.SearchCriteria.DateOfBirth != null)
                    {
                        this.CssError = "invalid";
                    }

                    this.ShowErrorMessages(results);
                    this.SearchError.ShowError();
                    this.SearchError.Error = ErrorType.NO_RESULT;
                   
                    
                }
            }
            else
            {
                this.SearchError.ShowError();
                this.SearchError.Error = ErrorType.CRITERIA;
            }
            this.SearchCriteria.IsInvalid = false;
        }     

        /// <summary>
        /// Clears the search field
        /// </summary>
        public void Clear()
        {
            SearchCriteria = new ApplicantSearchCriteria();
            this.EditContext = new EditContext(this.SearchCriteria);
            State.SearchCriteria = null;
            this.ClearMessages();
        }
        /// <summary>
        /// Displays the errors if the specified search criteria was not found
        /// </summary>
        /// <param name="errors"></param>
        private void ShowErrorMessages(FluentValidation.Results.ValidationResult errors)
        { 
            this.ValidationMessageStore = new ValidationMessageStore(this.EditContext);
            foreach(var i in errors.Errors)
            {
                var fieldIdentfier = new FieldIdentifier(this.SearchCriteria, i.PropertyName);
                ValidationMessageStore.Add(fieldIdentfier, i.ErrorMessage);
            }
            this.EditContext.NotifyValidationStateChanged();
           
        }
        public void ClearMessages()
        {
            this.CssError = "";
            if (this.ValidationMessageStore != null)
            {
                this.ValidationMessageStore.Clear();
            }
        }
    }
}
