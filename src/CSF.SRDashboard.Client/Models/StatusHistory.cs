
using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class StatusHistory
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string ProcessingPhase { get; set; }

        RequestStatus RequestStatus { get; set; }


        public System.DateTimeOffset? RequestStatusTime { get; set; }

        public string AssignedTo { get; set; }
    }
}
