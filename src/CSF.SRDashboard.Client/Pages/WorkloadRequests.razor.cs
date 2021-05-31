using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class WorkloadRequests
    {
        public List<WorkloadRequestTableItem> WorkloadData { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //
        }
    }
}
