using DSD.MSS.Blazor.Components.Core.Models;
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
        public const string Created = "Created";

        public static List<SelectListItem> RequestTypes = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "New certificate" },
            new SelectListItem { Id = "2", Text = "Renewal certificate" }
        };

        public static List<SelectListItem> CertificateTypes = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "Marine Medical Cerficate - 2 year validity" }
        };

        public static List<SelectListItem> DocumentTypeList = new List<SelectListItem> {
        new SelectListItem{ Id = "1",Text = "MME Exam Report" },
        new SelectListItem{ Id = "2",Text = "Medical Report" },
        new SelectListItem{ Id = "3",Text = "Letter" },
        new SelectListItem{ Id = "4",Text = "Certificate" },
        new SelectListItem{ Id = "5",Text = "Other" }
        };

        public static List<SelectListItem> SubmissionMethods = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "E-mail" },
            new SelectListItem { Id = "2", Text = "Mail"},
            new SelectListItem { Id = "3", Text = "Fax"},
            new SelectListItem { Id = "4", Text = "Online"},
            new SelectListItem { Id = "5", Text = "Phone"},
            new SelectListItem { Id = "6", Text = "Other"}
        };

        public static List<SelectListItem> RequestStatuses = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "New"},
            new SelectListItem { Id = "2", Text = "In Progress"},
            new SelectListItem { Id = "3", Text = "Pending"},
            new SelectListItem { Id = "4", Text = "Complete"},
            new SelectListItem { Id = "5", Text = "Cancelled"}
        };

        public static List<SelectListItem> Languages = new List<SelectListItem> {
            new SelectListItem { Id = "1", Text = "English" },
            new SelectListItem { Id = "2", Text = "French" }
        };

        public static string RequestStatusesDefaultValue = RequestStatuses[0].Text;

        public const string NoProfilePicturePath = "/img/no-profile-photo.png";

        public const string NotSelected = "-1";
    }
}
