using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public enum ServiceRequestStatus
    {
        Unknown = 0,
        InProgress = 1,
        Submitted = 2,
        InReview = 3,
        Completed = 4,
        TrainingCompleted = 5,
        InformationRequired = 6,
        Accepted = 7,
        AcceptedWithUpdates = 8,
        Canceled = 9,
        Initiated = 10,
        Expired = 11,
        Rejected = 12,
        Failed = 13,
        PickedUp = 14,
        TestPaymentPending = 15,
        TestPaymentSent = 16,
        Validated = 17,
        Verified = 18,
        Approved = 19,
        OnHold = 20,
        PendingApproval = 21,
        Issued = 22
    }
}
