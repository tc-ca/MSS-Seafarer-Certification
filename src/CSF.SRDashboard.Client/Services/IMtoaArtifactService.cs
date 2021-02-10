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
        public int GetNumberOfRequestForSeafarers();
        public List<ServiceRequest> GetAllRequestsForSeafarers(int numberOfRequest);
        public SeafarersArtifactDTO GetArtifactByServiceRequestId(int serviceRequestId);
        public DashboardRow GetDashboarRowFromServiceRequest(ServiceRequest serviceRequest);
        public SeafarersArtifactDTO GetArtifactByArtifactId(int artifactId);
        public List<DashboardRow> GetDashboardRowsInParallel();

    }
}
