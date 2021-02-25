using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class SeafarerDetailsModel
    {
        //tombstone
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CDN { get; set; }
        public DateTime DateOfBirth { get; set; }

        //medical summary
        public string FitnessStatus { get; set; }
        public List<string> Limitations { get; set; }
        public DateTime DateOfExpiry { get; set; }

        //certification summary
        public List<CertificateInfo> CurrentCertificates { get; set; }

        public SeafarerDetailsModel()
        {
            this.Limitations = new List<string>();
            this.CurrentCertificates = new List<CertificateInfo>();
        }
    }
}
