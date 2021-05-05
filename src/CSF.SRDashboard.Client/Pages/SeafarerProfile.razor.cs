using Microsoft.AspNetCore.Components;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using CSF.SRDashboard.Client.Services;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public TrimmedApplicantInformation Applicant { get; set; }

        protected override void OnInitialized()
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
        }
    }
}