using CSF.SRDashboard.Client.PageValidators;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
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
using FluentValidation;
using Microsoft.JSInterop;

namespace CSF.SRDashboard.Client.Pages
{
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
        public string buttonDisabled { get; set; }
        public SearchErrorObj searchError = new SearchErrorObj();
        public bool error { get; set; } = true;

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
            var validator = new SearchValidator();

            var result = validator.Validate(SearchCriteria, options => options.IncludeRuleSets("criteria"));
            if (result.IsValid)
            {
                if (this.IsSubmitting)
                    return;
                _ = JS.InvokeAsync<string>("DisableSeafarerSearchButton", null);
                this.IsSubmitting = true;
                this.ButtonDisabled = "disabled";
                this.State.SearchCriteria = SearchCriteria;
                this.State.ApplicantSearchResult = MpdisService.Search(this.SearchCriteria);
                this.NavigationManager.NavigateTo("/SearchResults");
            }
            else
            {
                this.searchError.error = errorType.CRITERIA;
            }
           
        }

        public void showError()
        {
            this.error = !this.error;
        }
          
        }     
    }
