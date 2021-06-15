using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
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

        public static List<SelectListItem> SubmissionMethods = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "Fax" },
            new SelectListItem { Id = "2", Text = "Mail"},
            new SelectListItem { Id = "3", Text = "Email"},
            new SelectListItem { Id = "4", Text = "Emer"}
        };
        //public List<SelectListItem> DocumentTypeList { get; set; }

        //public List<SelectListItem> Languages { get; set; }
        public static List<SelectListItem> DocumentTypeList = new List<SelectListItem> {
        new SelectListItem{ Id = "1",Text = "MME Exam Report" },
        new SelectListItem{ Id = "2",Text = "Medical Report" },
        new SelectListItem{ Id = "3",Text = "Letter" },
        new SelectListItem{ Id = "4",Text = "Certificate" },
        new SelectListItem{ Id = "5",Text = "Other" }
        };

        public static List<Dropdown> Languages = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "English" },
            new Dropdown { ID = "2", Text = "French" }
        };

    }
}
