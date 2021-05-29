namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities
{
    using System;
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;

    public class WorkloadRequestTableItem
    {
            public string RequestId { get; set; }

            public string Certificate { get; set; }

            public string RequestType { get; set; }

            public DateTime RequestDate { get; set; }

            public string Status { get; set; }

            public string Priority { get; set; }

            public string Language { get; set; }
    }
}
