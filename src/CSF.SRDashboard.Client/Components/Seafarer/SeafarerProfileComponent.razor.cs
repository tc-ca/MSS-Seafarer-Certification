namespace CSF.SRDashboard.Client.Components.Seafarer
{
    using CSF.SRDashboard.Client.DTO;
    using Microsoft.AspNetCore.Components;
    using MPDIS.API.Wrapper.Services.MPDIS.Entities;

    public partial class SeafarerProfileComponent
    {
        [Parameter]
        public MpdisDto Applicant { get; set; }
    }
}
