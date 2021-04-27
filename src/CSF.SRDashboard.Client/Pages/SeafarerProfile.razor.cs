using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using MPDIS.API.Wrapper.Services.MPDIS;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IMpdisService MpdisService { get; set; }

        public ApplicantInformation Applicant { get; set; }

        protected override void OnInitialized()
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.Applicant = this.MpdisService.GetApplicantByCdn(Cdn);
        }
    }
}