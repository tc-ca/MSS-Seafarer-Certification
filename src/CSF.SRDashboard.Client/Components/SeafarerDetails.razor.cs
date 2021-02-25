using Microsoft.AspNetCore.Components;
using CSF.SRDashboard.Client.Models;

namespace CSF.SRDashboard.Client.Components
{
    public partial class SeafarerDetails
    {
        [Parameter]
        public SeafarerDetailsModel Data { get; set; }
    }
}
