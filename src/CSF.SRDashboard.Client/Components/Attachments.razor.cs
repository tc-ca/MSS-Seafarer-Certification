using CSF.Web.Client.Models.PageModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class Attachments
    {
        [Inject]
        private IWebHostEnvironment hostingEnv { get; set; }

        public List<UploadedFile> UploadedFiles { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var h = hostingEnv.WebRootPath;

                string FilePath = Path.Combine(hostingEnv.WebRootPath, "temp");

                string[] filePaths = Directory.GetFiles(FilePath);

                UploadedFiles = new List<UploadedFile>();

                foreach (string filePath in filePaths)
                {
                    UploadedFiles.Add(new UploadedFile
                    {
                        FilePath = filePath,
                        FileName = Path.GetFileName(filePath)
                    });
                }
            }
            StateHasChanged();
            base.OnAfterRender(firstRender);
        }

    }

    public class FileList
    {

        public int PicsID { get; set; }

        public string PicsName { get; set; }

    }
}
