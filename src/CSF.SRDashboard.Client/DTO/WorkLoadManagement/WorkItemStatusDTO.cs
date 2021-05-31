using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemStatusDTO
    {
        public int Id { get; set; }

        public int WorkItemId { get; set; }

        public string WorkItemStatusCode { get; set; }

        public string WorkItemReasonCode { get; set; }

        public System.DateTimeOffset? StatusDateUTC { get; set; }

        public string StatusAdditionalDetails { get; set; }

        public string StatusChangeEmployeeId { get; set; }

        public RequestStatus RequestStatus { get {
                switch (this.StatusAdditionalDetails)
                {
                    case "New": return RequestStatus.NEW;
                    case "Complete": return RequestStatus.COMPLETE;
                    case "In Progress": return RequestStatus.IN_PROGRESS;
                    case "Pending": return RequestStatus.PENDING;
                    case "Unknown": return RequestStatus.UNKNOWN;
                }
                return RequestStatus.UNKNOWN;
            }
            set { } }
    }
}
