using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class ServiceRequestDTO : BaseServiceRequest
    {
        public List<KeyValuePair<string, string>> Metadata { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
