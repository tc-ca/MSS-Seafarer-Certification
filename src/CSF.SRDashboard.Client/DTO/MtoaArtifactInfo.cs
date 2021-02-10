using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class MtoaArtifactInfo
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public string ArtifactType { get; set; }
        public string RdimsReference { get; set; }
        public string DocumentVersion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
