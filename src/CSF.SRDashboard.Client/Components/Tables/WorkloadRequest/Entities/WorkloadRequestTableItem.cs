namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities
{
    using System;
    using CSF.SRDashboard.Client.Services;
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;

    public class WorkloadRequestTableItem
    {
            public string RequestId { get; set; }
            public string AssignedTo { get; set; }
            public string Certificate { get; set; }
            public string ApplicantCDN { get; set; }
            public string RequestType { get; set; }

            public DateTime RequestDate { get; set; }
            public string Status { get; set; }
            public RequestStatus RequestStatus
            {
            get
            {
                switch (this.Status)
                {
                    case Constants.New: return RequestStatus.NEW;
                    case Constants.Completed: return RequestStatus.COMPLETE;
                    case Constants.InProgress: return RequestStatus.IN_PROGRESS;
                    case Constants.Pending: return RequestStatus.PENDING;
                    case Constants.Unknown: return RequestStatus.UNKNOWN;
                    default: return RequestStatus.UNKNOWN;
                }
               
            }
            set { }
        }
        public string ProcessingPhase { get; set; }
        public string Priority { get; set; }
       

    }
}
