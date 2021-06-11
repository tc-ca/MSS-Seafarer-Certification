using CSF.SRDashboard.Client.Models;
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
        public const string Pending = "Pending";
        public const string Unknown = "Unknown";


        public const string MarineMedical = "002";

        public const string Updated = "Updated";

        public static List<Dropdown> RequestTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "New certificate" },
            new Dropdown { ID = "2", Text = "Renewal certificate" }
        };

        public static List<Dropdown> CertificateTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "Marine Medical Cerficate - 2 year validity" }
        };

        public static List<Dropdown> SubmissionMethods = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "E-mail" },
            new Dropdown { ID = "2", Text = "Mail"},
            new Dropdown { ID = "3", Text = "Fax"},
            new Dropdown { ID = "4", Text = "Online"},
            new Dropdown { ID = "5", Text = "Phone"},
            new Dropdown { ID = "6", Text = "Other"}
        };

    }
}
