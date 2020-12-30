using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CSF.Web.Client.Data.DTO.MTAPI
{
    public class EmailAttachmentDTO
    {
        public EmailAttachmentDTO() { }

        public string Name { get; set; }
        public string Type { get; set; }
        public Stream Sream { get; set; }
        public byte[] AttachmentBytes { get; set; }
    }
}
