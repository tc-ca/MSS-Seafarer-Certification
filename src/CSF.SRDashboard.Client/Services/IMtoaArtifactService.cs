using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public interface IMtoaArtifactService
    {
        public List<ServiceRequest> GetAllRequestsForSeafarers();
        public SeafarersArtifactDTO GetArtifactByServiceRequestId(int serviceRequestId);
        public DashboardRow GetDashboardRowFromServiceRequest(ServiceRequest serviceRequest);
        public List<DashboardRow> GetDashboardRowsInParallel();
    }
}
