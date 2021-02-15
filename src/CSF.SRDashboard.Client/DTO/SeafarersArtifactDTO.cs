using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class SeafarersArtifactDTO
    {
        public string CdnNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmationNumber { get; set; }
        public string CertificateType { get; set; }
        public List<MtoaFileInfo> UploadedFiles { get; set; }
        public string SubmissionType { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string PersonAssignedTo { get; set; }
        public string Note { get; set; }
        public string SubmissionProgress { get; set; }
    }

}
