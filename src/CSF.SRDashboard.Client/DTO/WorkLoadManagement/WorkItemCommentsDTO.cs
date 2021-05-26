using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemCommentsDTO
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public string Comment { get; set; }
        public System.DateTimeOffset? CreatedDateUTC { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTimeOffset? LastUpdatedDateUTC { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTimeOffset? DeletedDateUTC { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
