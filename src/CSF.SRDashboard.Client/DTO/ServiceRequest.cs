using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class ServiceRequest : BaseServiceRequest
    {
        public ServiceRequest()
        { }
        public ServiceRequest(BaseServiceRequest dto)
        {
        }
        public IServiceRequestMetadata Metadata { get; set; }
        public DateTime DateUpdated { get; set; }

        public ServiceRequestDTO ToDTO()
        {
            return null;
        }

    }
}
