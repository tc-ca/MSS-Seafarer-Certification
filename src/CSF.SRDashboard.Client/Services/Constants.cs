﻿using CSF.SRDashboard.Client.Models;
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

        public static List<Dropdown> RequestTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "New certificate" },
            new Dropdown { ID = "2", Text = "Renewal certificate" }
        };

        public static List<Dropdown> CertificateTypes = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "Marine Medical Cerficate - 2 year validity" }
        };

        public static List<Dropdown> SubmissionMethods = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "FAX" },
            new Dropdown { ID = "2", Text = "MAIL"},
            new Dropdown { ID = "3", Text = "EMAIL"},
            new Dropdown { ID = "4", Text = "EMER"}
        };

        public static List<Dropdown> DocumentTypeList = new List<Dropdown> {
        new Dropdown{ ID = "1",Text = "MME Exam Report" },
        new Dropdown{ ID = "2",Text = "Medical Report" },
        new Dropdown{ ID = "3",Text = "Letter" },
        new Dropdown{ ID = "4",Text = "Certificate" },
        new Dropdown{ ID = "5",Text = "Other" }
        };

        public static List<Dropdown> Languages = new List<Dropdown> {
            new Dropdown { ID = "1", Text = "English" },
            new Dropdown { ID = "2", Text = "French" }
        };

    }
}
