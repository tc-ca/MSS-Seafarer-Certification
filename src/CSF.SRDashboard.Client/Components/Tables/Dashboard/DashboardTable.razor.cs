using System.Collections.Generic;
using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CSF.SRDashboard.Client.Components.Tables.Dashboard
{
    public partial class DashboardTable
    {
        [Parameter]
        public List<Document> TableData { get; set; } = new List<Document>();
        [Inject]
        public NavigationManager NavigationManager { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

    }
}
