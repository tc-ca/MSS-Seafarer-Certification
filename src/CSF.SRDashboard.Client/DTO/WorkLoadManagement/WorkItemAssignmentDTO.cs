using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemAssignmentDTO
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public string AssignedEmployeeId { get; set; }
        public System.DateTimeOffset? DateAssignedUTC { get; set; }
        public bool? DeletedInd { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTimeOffset? DeletedDateUTC { get; set; }
    }
}
