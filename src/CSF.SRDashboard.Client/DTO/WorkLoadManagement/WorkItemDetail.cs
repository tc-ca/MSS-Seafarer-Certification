using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemDetail
    {
        [JsonPropertyName("Cdn")]
        public string Cdn { get; set; }
        [JsonPropertyName("ApplicantName")]
        public string ApplicantName { get; set; }
        [JsonPropertyName("RequestType")]
        public string RequestType { get; set; }
        [JsonPropertyName("CertificateType")]
        public string CertificateType { get; set; }
        [JsonPropertyName("SubmissionMethod")]
        public string SubmissionMethod { get; set; }
        [JsonPropertyName("DueDate")]
        public DateTime? DueDate { get; set; }
        [JsonPropertyName("Status")]
        public object Status { get; set; }
        [JsonPropertyName("ProcessingPhase")]
        public string ProcessingPhase { get; set; }
        [JsonPropertyName("HasAttachments")]
        public bool HasAttachments { get; set; }
        [JsonPropertyName("Comments")]
        public List<RequestComment> Comments { get; set; }

        public WorkItemAssignmentDTO Assignment { get; set; }
    }
}
