using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using MPDIS.API.Wrapper.Services.MPDIS;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Table;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IMpdisService MpdisService { get; set; }
        public ApplicantInformation applicant = new ApplicantInformation();
        public DateTime DOB;
        protected Table<Document> TableRef { get; set; }
        protected List<Document> TableData = new List<Document>();
        public string fullDob { get; set; }
        public string currentRelativePath;

        protected override void OnInitialized()
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.applicant = this.MpdisService.GetApplicantByCdn(Cdn);
            DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(this.applicant.DateOfBith);
            DOB = offset.DateTime;
            fullDob = DOB.ToString("MMMM dd, yyyy");

        }
    }
}