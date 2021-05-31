using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class WorkloadRequests
    {
        public List<WorkloadRequestTableItem> WorkloadData { get; set; }
        List<WorkItemDTO> workItems = new List<WorkItemDTO>();
        [Inject]

        public IWorkLoadManagementService WorkLoadService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //
            WorkloadData = WorkLoadService.GetAllInRequestTableFormat();
        }
    }
}
