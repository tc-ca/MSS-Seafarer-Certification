using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class MMEDetailComponentModel
    {

        public string Name { get; set; }

        public int MmeNumber { get; set; }

        public string DateOfExpiry { get { return ExpiryDate.ToString("MMMM dd, yyyy"); }  }

        public DateTime ExpiryDate { get; set; }

    }
}
