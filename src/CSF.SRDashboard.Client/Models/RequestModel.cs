using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class RequestModel
    {
        public int RequestID { get; set; }

        public string RequestType { get; set; }

        public string CertificateType { get; set; }

        public string SubmissionMethod { get; set; }

        public string ApplicantFullName { get; set; }

        public string Cdn { get; set; }

        public string AssigneeId { get; set; }

        public string Status { get; set; }

        public string ProcessingPhase { get; set; }

        public DateTime? DueDate { get; set; }

        public List<RequestComment> Comments { get; set; }

        public List<UploadedDocument> UploadedDocuments { get; set; }

    }

}
