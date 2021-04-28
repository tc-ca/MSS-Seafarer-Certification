using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.PageValidators
{
    public enum errorType
    {
        CRITERIA,
        NO_RESULT
    }
    public class SearchErrorObj
    {

        public string message { get; set; }
        public errorType? error { get; set; }
    }
}
