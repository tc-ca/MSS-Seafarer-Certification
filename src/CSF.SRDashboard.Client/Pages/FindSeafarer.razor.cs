﻿using CSF.SRDashboard.Client.Services;
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

        public ApplicantSearchCriteria SearchCriteria = new ApplicantSearchCriteria();

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
                State.SearchCriteria = SearchCriteria;

                State.ApplicantSearchResult = MpdisService.Search(SearchCriteria);

                NavigationManager.NavigateTo("/SearchResults");
        }     
    }
}