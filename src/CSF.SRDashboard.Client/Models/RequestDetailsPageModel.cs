using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class RequestDetailsPageModel
    {
        public RequestDetailComponentModel RequestDetails { get; set; }
        public SeafarerDetailsModel SeafarerDetails { get; set; }
        public MMEDetailComponentModel MMEDetail { get; set; }
    }
}
