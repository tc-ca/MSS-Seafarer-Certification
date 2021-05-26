using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Utilities;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;



namespace CSF.SRDashboard.Client.Pages
{
    public partial class Index: ComponentBase
    {
        [Inject]
        public IWorkLoadManagementService WorkLoadManagementService { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            //var workItems = this.WorkLoadManagementService.GetByLineOfBusinessId(Constants.MarineMedical);
            var workItem = this.WorkLoadManagementService.GetByWorkItemById(1000098);
            var workItemDetails = JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);
            // var detail =JsonSerializer.Deserialize<WorkItemDetail>(workItem.Detail);
        }

    }
}
