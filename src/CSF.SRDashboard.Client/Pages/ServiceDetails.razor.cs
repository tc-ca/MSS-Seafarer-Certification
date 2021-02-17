namespace CSF.SRDashboard.Client.Pages
{
    using Microsoft.AspNetCore.Components;

    public partial class ServiceDetails
    {
        /// <summary>
        /// Gets or sets the service request identification number.
        /// </summary>
        [Parameter]
        public int ServiceRequestId { get; set; }
    }
}