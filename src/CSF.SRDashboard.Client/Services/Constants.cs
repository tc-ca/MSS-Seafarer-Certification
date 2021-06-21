using DSD.MSS.Blazor.Components.Core.Models;
using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
using System;
using System.Collections.Generic;

namespace CSF.SRDashboard.Client.Services
{
    public static class Constants
    {
        public const int DefaultPageSize = 10;

        //ProcessingPhase
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string OnHold = "On Hold";
        public const string Complete = "Complete";
        public const string NotSubmitted = "Not Completed";
        public const string Pending = "Pending";
        public const string Cancelled = "Cancelled";
        public const string Unknown = "Unknown";

        public const string MarineMedical = "002";

        public const string Updated = "Updated";

        public static List<SelectListItem> RequestTypes = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "New certificate" },
            new SelectListItem { Id = "2", Text = "Renewal certificate" }
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

        public static List<Dropdown> RequestStatuses = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "New"},
            new Dropdown { ID = "2", Text = "In Progress"},
            new Dropdown { ID = "3", Text = "Pending"},
            new Dropdown { ID = "4", Text = "Complete"},
            new Dropdown { ID = "5", Text = "Cancelled"}
        };

        public static List<SelectListItem> Languages = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "English" },
            new SelectListItem { Id = "2", Text = "French" }
        };
        public static List<SelectListItem> DocumentTypes = new List<SelectListItem>
        {
            new SelectListItem { Id = "1", Text = "MME Exam Report", Value = false},
            new SelectListItem { Id = "2", Text = "Medical Report", Value = false},
            new SelectListItem { Id = "3", Text = "Letter", Value = false},
            new SelectListItem { Id = "4", Text = "Certificate", Value = false },
            new SelectListItem { Id = "5",Text = "Other", Value = false}
        };

        public static string RequestStatusesDefaultValue = RequestStatuses[0].Text;
    }
}
