using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.API.Data.Entities
{
    public class DocumentInfo
    {

        public string Cdn { get; set; }

        public Guid DocumentId { get; set; }

        public DateTime DateStartDte { get; set; }

        public DateTime? DateEndDte { get; set; }

    }
}
