using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemDetail
    {
        public string Cdn { get; set; }

        public string ApplicantName { get; set; }

        public string RequestType { get; set; }

        public string CertificateType { get; set; }

        public string SubmissionMethod { get; set; }

        public bool HasAttachments { get; set; }

        public List<RequestComment> Comments { get; set; }
    }
}
