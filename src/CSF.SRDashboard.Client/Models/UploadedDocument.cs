
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document.Entities;
using DSD.MSS.Blazor.Components.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class UploadedDocument
    {
        public Guid DocumentId { get; set; }

        public string Cdn { get; set; }
        /// <summary>
        /// Document list that is shown in the dropdown
        /// </summary>
        public List<SelectListItem> DocumentTypeList { get; set; }
        /// <summary>
        /// The types the user selected
        /// </summary>
        public List<DocumentTypes> DocumentTypes { get; set; }

        public List<SelectListItem> Languages { get; set; }

        private string fileName;

        public string FileName
        {
            get
            {
                if (FormFile == null && string.IsNullOrWhiteSpace(fileName))
                {
                    return string.Empty;
                }

                if (FormFile == null)
                    return fileName;
                return FormFile.FileName;
            }

            set { fileName = value; }
        }

        public string Description { get; set; }

        public string Language { get; set; }

        public IFormFile FormFile { get; set; }

        public int SelectValue { get; set; }

        public string DownloadLink { get; set; }

        public UploadedDocument()
        {
            DocumentTypeList = new List<SelectListItem> {
        new SelectListItem{ Id = "1",Text = "MME Exam Report" },
        new SelectListItem{ Id = "2",Text = "Medical Report" },
        new SelectListItem{ Id = "3",Text = "Letter" },
        new SelectListItem{ Id = "4",Text = "Certificate" },
        new SelectListItem{ Id = "5",Text = "Other" }
        };

            Languages = new List<SelectListItem>();



            Languages.Add(new SelectListItem { Id = "1", Text = "EN", Value = false });
            Languages.Add(new SelectListItem { Id = "2", Text = "FR", Value = false });

        }
    }
}
