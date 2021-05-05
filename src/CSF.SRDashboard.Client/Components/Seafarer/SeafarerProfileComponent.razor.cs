namespace CSF.SRDashboard.Client.Components.Seafarer
{
    using Microsoft.AspNetCore.Components;
    using MPDIS.API.Wrapper.Services.MPDIS.Entities;

    public partial class SeafarerProfileComponent
    {
        [Parameter]
        public TrimmedApplicantInformation Applicant { get; set; }
    }
}
