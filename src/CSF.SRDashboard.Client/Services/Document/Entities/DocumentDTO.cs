using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services.Document.Entities
{
    public class DocumentDTO
    {
        public Guid CorrelationId { get; set; }
        public List<DocumentInfo> Documents { get; set; }

        public DocumentDTO()
        {
            this.Documents = new List<DocumentInfo>();
        }
    }
}
