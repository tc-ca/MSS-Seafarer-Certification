
using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class StatusHistoryItem
    {
        public string Id { get; set; }

        public string StatusText { get; set; }

        public string ProcessingPhase { get; set; }

        public RequestStatus RequestStatus
        {
            get { switch (this.StatusText) {
                    case "New": return RequestStatus.NEW;
                    case "In Progress": return RequestStatus.IN_PROGRESS;
                    case "Pending": return RequestStatus.PENDING;
                    case "Complete": return RequestStatus.COMPLETE;
                    case "Cancelled": return RequestStatus.CANCELLED;
                    case "Unknown": return RequestStatus.UNKNOWN;
                    default: return RequestStatus.NEW;
                } 
            }
        }

        public DateTimeOffset? RequestStatusTime { get; set; }
        public string ChangedBy { get; set; }
    }
}
