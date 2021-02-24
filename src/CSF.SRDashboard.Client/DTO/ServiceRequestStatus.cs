namespace CSF.SRDashboard.Client.DTO
{
    using System.ComponentModel;

    public enum ServiceRequestStatus
    {
        Unknown = 0,

        [Description("In Progress")]
        InProgress = 1,
        Submitted = 2,
        [Description("In Review")]
        InReview = 3,
        Completed = 4,

        [Description("Training Completed")]
        TrainingCompleted = 5,

        [Description("Information Required")]
        InformationRequired = 6,
        Accepted = 7,

        [Description("Accepted With Updates")]
        AcceptedWithUpdates = 8,
        Canceled = 9,
        Initiated = 10,
        Expired = 11,
        Rejected = 12,
        Failed = 13,

        [Description("Picked Up")]
        PickedUp = 14,

        [Description("Test Payment Pending")]
        TestPaymentPending = 15,

        [Description("Test Payment Sent")]
        TestPaymentSent = 16,
        Validated = 17,
        Verified = 18,
        Approved = 19,
        OnHold = 20,

        [Description("Pending Approval")]
        PendingApproval = 21,
        Issued = 22
    }
}
