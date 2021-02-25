using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class RequestGridsModel
    {
        public RequestGridsModel()
        {

        }
        public List<DashboardRow> RequestsInProgress { get; set; }
        public List<DashboardRow> RequestsOnHold { get; set; }
        public List<DashboardRow> RequestsCompleted { get; set; }
        public List<DashboardRow> RequestsNotSubmitted { get; set; }
        public List<DashboardRow> NewRequests { get; set; }
        public bool HasData { get; set; }
    }
}
