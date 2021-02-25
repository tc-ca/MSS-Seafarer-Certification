using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class CertificateInfo
    {
        public string CertificateNameEnglish { get; set; }
        public string CertificateNameFrench { get; set; }
        public DateTime ExpiryDate { get; set; }

        public CertificateInfo(string certificateNameEnglish, DateTime expiryDate)
        {
            this.CertificateNameEnglish = certificateNameEnglish;
            this.ExpiryDate = expiryDate;
        }
    }
}
