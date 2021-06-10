﻿using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class UploadDocumentComponent
    {
       

        [Parameter]
        public List<UploadedDocument> DocumentForm
        {
            get => documentForm;
            set
            {
                if (documentForm == value) return;
                this.documentForm = value;
                DocumentFormChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<List<UploadedDocument>> DocumentFormChanged { get; set; }

        private List<UploadedDocument> documentForm;

        [Parameter]
        public bool AllowMultipleUploads { get; set; }
        public int MaxAllowedFiles => this.AllowMultipleUploads ? 5 : 1;

        public string UploadClass => this.DocumentForm.Count < this.MaxAllowedFiles ? "file-drop-zone" : "file-drop-zone-disabled";
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DocumentForm = new List<UploadedDocument>();

        }

        public async void OnFileUpload(InputFileChangeEventArgs e)
        {
            
            var files = e.GetMultipleFiles();
            
            foreach (var file in files)
            {
                if (file != null && !string.Equals(file.ContentType, "application/x-msdownload")){
                    MemoryStream ms = new MemoryStream();
                    await file.OpenReadStream(e.File.Size).CopyToAsync(ms);
                    IFormFile NewFormFile = new FormFile(ms, 0, e.File.Size, e.File.Name, e.File.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = e.File.ContentType
                    };
                    this.DocumentForm.Add(new UploadedDocument()
                    {
                        FormFile = NewFormFile
                    });

                }
              
            }
            StateHasChanged();
        }
    }
}
