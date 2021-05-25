using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO
{
    public class FileUploadDTO
    {
        public string Cdn { get; set; }
        public string FullName { get; set; }
        public bool FileUploadComplete { get; set; }
    }
}
