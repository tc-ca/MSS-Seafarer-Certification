using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class Document
    {
        public Guid DocumentId { get; set; }

        public string FileName { get; set; }

        public string Type { get; set; }

        public string Language { get; set; }

        public string RequestID { get; set; }

        public DateTime DateUploaded { get; set; }

        public string DateUploadFormatted { get { return DateUploaded.ToString("MMM dd, yyyy"); } }

        public string DocumentUrl { get; set; }

        public string DownloadLink { get; set; }

    }
}
