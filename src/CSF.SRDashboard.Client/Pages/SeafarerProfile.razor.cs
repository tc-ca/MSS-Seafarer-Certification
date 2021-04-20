using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using MPDIS.API.Wrapper.Services.MPDIS;

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
        public string fullDob { get; set; }
        public SeafarerProfile()
        {

        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
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