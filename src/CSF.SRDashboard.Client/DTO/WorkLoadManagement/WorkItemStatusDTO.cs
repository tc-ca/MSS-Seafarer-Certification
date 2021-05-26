using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemStatusDTO
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public string WorkItemStatusCode { get; set; }
        public string WorkItemReasonCode { get; set; }
        public System.DateTimeOffset? StatusDateUTC { get; set; }
        public string StatusAdditionalDetails { get; set; }
        public string StatusChangeEmployeeId { get; set; }
    }
}
