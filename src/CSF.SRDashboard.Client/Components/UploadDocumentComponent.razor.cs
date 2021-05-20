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
        public IFormFile FormFile
        {
            get => formFile;
            set
            {
                if (formFile == value) return;
                this.formFile = value;
                FormFileChanged.InvokeAsync(value);
            }
        }

        private IFormFile formFile;

        public string UploadClass { get; set; } = "file-drop-zone";

        [Parameter]
        public EventCallback<IBrowserFile> FileChanged { get; set; }

        [Parameter]
        public EventCallback<IFormFile> FormFileChanged { get; set; }

        public async void OnFileUpload(InputFileChangeEventArgs e)
        {
            if (e.File != null)
            {
                this.UploadClass = "file-drop-zone-disabled";

                MemoryStream ms = new MemoryStream();
                await e.File.OpenReadStream(e.File.Size).CopyToAsync(ms);
                var bytes = ms.ToArray();

                IFormFile NewFormFile = new FormFile(ms, 0, e.File.Size, e.File.Name, e.File.Name)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = e.File.ContentType
                };

                this.FormFile = NewFormFile;

            }
            else
            {
                this.UploadClass = "file-drop-zone";
            }
        }

    }
}
