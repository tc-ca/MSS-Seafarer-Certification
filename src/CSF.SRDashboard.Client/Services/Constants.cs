using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Services
{
    public static class Constants
    {
        public const int DefaultPageSize = 10;

        //ProcessingPhase
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string OnHold = "On Hold";
        public const string Completed = "Completed";
        public const string NotSubmitted = "Not Completed";
    }
}
