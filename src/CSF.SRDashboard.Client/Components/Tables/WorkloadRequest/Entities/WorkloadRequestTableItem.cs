namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities
{
    using System;
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;

    public class WorkloadRequestTableItem
    {
            public string RequestId { get; set; }
            public string AssignedTo { get; set; }
            public string Certificate { get; set; }

            public string RequestType { get; set; }

            public DateTime RequestDate { get; set; }
            public string Status { get; set; }
            public RequestStatus RequestStatus
            {
            get
            {
                switch (this.Status)
                {
                    case "New": return RequestStatus.NEW;
                    case "Complete": return RequestStatus.COMPLETE;
                    case "In Progress": return RequestStatus.IN_PROGRESS;
                    case "Pending": return RequestStatus.PENDING;
                    case "Unknown": return RequestStatus.UNKNOWN;
                }
                return RequestStatus.UNKNOWN;
            }
            set { }
        }
        public string ProcessingPhase { get; set; }
        public string Priority { get; set; }
        public string ApplicantCDN { get; set; }

    }
}
