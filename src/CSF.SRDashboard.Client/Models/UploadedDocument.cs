﻿using CSF.API.Data.Entities;
using CSF.SRDashboard.Client.DTO.DocumentStorage;
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

        public List<DocumentTypeDTO> DocumentType { get; set; }

        public IFormFile FormFile { get; set; }

        public int SelectValue { get; set; }

        public string DownloadLink { get; set; }

        public UploadedDocument()
        {
            this.DocumentTypeList = new List<SelectListItem>
        {
            new SelectListItem { Id = "1", Text = "MME Exam Report", Value = false},
            new SelectListItem { Id = "2", Text = "Medical Report", Value = false},
            new SelectListItem { Id = "3", Text = "Letter", Value = false},
            new SelectListItem { Id = "4", Text = "Certificate", Value = false },
            new SelectListItem { Id = "5",Text = "Other", Value = false}
        };
        }
    }
}
