using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class RequestCommentInfo
    {
        public int Id { get; set; }
        public int WorkItemId { get; set; }
        public string Comment { get; set; }
        public System.DateTimeOffset? CreatedDateUTC { get; set; }
        public string CreatedBy { get; set; }
    }
}
