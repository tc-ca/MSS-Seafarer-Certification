namespace CSF.SRDashboard.Client.Services.WorkloadRequest.Enums
{
    using System.ComponentModel;
    /// <summary>
    /// Defines the status of the work request
    /// </summary>
    public enum RequestStatus
    {
        [Description("New")]
        NEW,

        [Description("Complete")]
        COMPLETE,

        [Description("In Progress")]
        IN_PROGRESS,

        [Description("Pending")]
        PENDING,

        [Description("Unknown")]
        UNKNOWN
    }
}
