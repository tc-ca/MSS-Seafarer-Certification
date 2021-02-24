using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class DashboardRow
    {
        public int ServiceRequestNumber { get; set; }
        public string FullName { get; set; }
        public string CDN { get; set; }
        public string RequestType { get; set; }
        public string AssignedTo { get; set; }
        public int ServiceStandard { get; set; }
        public string ProcessingPhase { get; set; }
        public string View { get; set; }
    }
}
