using CSF.API.Data.Entities;
using CSF.SRDashboard.Client.Services;
using DSD.MSS.Blazor.Components.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Models
{
    public class UploadedDocument : DocumentInfo
    {
        public List<SelectListItem> DocumentTypeList { get; set; }

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

        public List<DocumentTypes> DocumentType { get; set; }

        public IFormFile FormFile { get; set; }

        public int SelectValue { get; set; }

        public string DownloadLink { get; set; }

        public UploadedDocument()
        {
            DocumentTypeList = Constants.DocumentTypeList;

            Languages = new List<SelectListItem>();

            

            Languages.Add(new SelectListItem { Id = "1", Text = "EN", Value = false });
            Languages.Add(new SelectListItem { Id = "2", Text = "FR", Value = false });

            this.SelectValue = -1;
        }
    }
}
