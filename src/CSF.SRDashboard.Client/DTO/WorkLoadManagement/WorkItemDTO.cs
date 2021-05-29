using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemDTO
    {
        public int Id { get; set; }

        public string ExternalServiceRequestId { get; set; }

        public System.DateTimeOffset? ReceivedDateUTC { get; set; }

        public string SubmissionMethodId { get; set; }

        public string ApplicantId { get; set; }

        public string SubmitterId { get; set; }

        public bool SameApplicantSubmitterInd { get; set; }

        public int? PaymentId { get; set; }

        public int? WorkItemGroupId { get; set; }

        public int? WorkItemStatusId { get; set; }

        public string LineOfBusinessId { get; set; }
        public string CreatedBy { get; set; }

        public System.DateTimeOffset? CreatedDateUTC { get; set; }

        public bool? DeletedInd { get; set; }

        public string DeletedBy { get; set; }

        public System.DateTimeOffset? DeletedDateUTC { get; set; }

        public string DeletionReason { get; set; }

        public string LastUpdatedBy { get; set; }

        public System.DateTimeOffset? LastUpdatedDateUTC { get; set; }

        public string InitialDetail { get; set; }

        public string Detail { get; set; }

        public ContactInformationDTO SubmitterContact { get; set; }

        public ContactInformationDTO ApplicantContact { get; set; }

        public WorkItemStatusDTO WorkItemStatus { get; set; }

        public WorkItemCommentsDTO WorkItemComment { get; set; }

        public System.Collections.Generic.ICollection<WorkItemAttachmentDTO> WorkItemAttachments { get; set; }

        public WorkItemAssignmentDTO WorkItemAssignment { get; set; }
    }
}
