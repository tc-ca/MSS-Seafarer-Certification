using Microsoft.AspNetCore.Components;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Utilities;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        [Inject]
        public SessionState state { get; set; }

        protected override void OnInitialized()
        {
            this.LoadData();
        }

        private void LoadData()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.state.mpdisApplicant = this.Applicant;

        }
    }
}