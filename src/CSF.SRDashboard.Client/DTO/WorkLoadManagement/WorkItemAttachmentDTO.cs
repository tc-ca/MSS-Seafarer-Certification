using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    public class WorkItemAttachmentDTO
    {
        public int WorkItemId { get; set; }

        public System.Guid DocumentId { get; set; }
    }
}
