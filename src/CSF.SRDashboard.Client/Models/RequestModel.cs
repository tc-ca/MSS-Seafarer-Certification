using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class RequestModel
    {
        public string RequestID { get; set; }

        public string RequestType { get; set; }

        public string CertificateType { get; set; }

        public string SubmissionMethod { get; set; }

        public string ApplicantFullName { get; set; }

        public string Cdn { get; set; }

        public string AssignedTo { get; set; }
        public List<Document> Documents { get; set; }

        public List<RequestComment> Comments { get; set; }

    }
}
