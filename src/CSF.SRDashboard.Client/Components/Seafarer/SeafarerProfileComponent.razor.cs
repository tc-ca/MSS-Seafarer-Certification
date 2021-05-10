namespace CSF.SRDashboard.Client.Components.Seafarer
{
    using CSF.SRDashboard.Client.DTO;
    using Microsoft.AspNetCore.Components;

    public partial class SeafarerProfileComponent
    {
        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }
    }
}
