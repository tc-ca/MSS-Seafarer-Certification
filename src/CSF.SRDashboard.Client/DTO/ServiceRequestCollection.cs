using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class ServiceRequestCollection
    {
        public ServiceRequestCollection()
        {
        }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public bool HasMore { get; set; }
    }
}
