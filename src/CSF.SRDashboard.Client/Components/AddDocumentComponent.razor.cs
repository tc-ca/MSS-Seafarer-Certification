﻿using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class AddDocumentComponent
    {
        [Parameter]
        public IBrowserFile File { get; set; }

        public IFormFile FileToUpload { get; set; }

        [Parameter]
        public string ProfileName { get; set; }
        public string FileName { get { if (File == null) return String.Empty; return File.Name; } }
       
        public List<string> DocumentTypes { get; set; }
        [Parameter]
        public AddDocumentModel DocumentForm { get; set; }
        [Parameter]
        public EditContext EditContext { get; set; }
        [Inject]
        public SessionState state { get; set; }

        [Inject]
        public IDocumentService DocumentServe { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DocumentForm = new AddDocumentModel();
        }

        private void OnInputChange(EventArgs e)
        {
         
        }
        private void HandleValidSubmit()
        {
            var isValid = EditContext.Validate();
            this.DocumentTypes = PopulateDocumentTypes(this.DocumentForm.DocumentTypeList);
            Console.WriteLine("Valid Submit");
            var result = this.DocumentServe.InsertDocument(0, "User", (IFormFile)File, "", DocumentForm.Description, "Dashboard", DocumentForm.Languages[DocumentForm.SelectValue], this.DocumentTypes, "");

        }
        private List<string> PopulateDocumentTypes(List<SelectListItem> list)
        {
            List<string> DocumentTypes = new List<string>();
            foreach (var i in list)
            {
                if (i.Value)
                {
                    DocumentTypes.Add(i.Text);
                }
            }
            return DocumentTypes;
        }
    }
  
}
