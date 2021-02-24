using CSF.Web.Client.Models.PageModels;
using Microsoft.AspNetCore.Components;
namespace CSF.SRDashboard.Client.Components
{
    using System.Collections.Generic;
    using System.IO;

    public partial class Attachments
    {
        public List<UploadedFile> UploadedFiles { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                UploadedFiles = new List<UploadedFile>();

                string[] filePaths = {"temp/image_1.jpg", "temp/image_2.jpg", "temp/image_3.jpg" };

                foreach (string filePath in filePaths)
                {
                    UploadedFiles.Add(new UploadedFile
                    {
                        FilePath = filePath,
                        FileName = filePath.Split('/')[1]
                    }); 
                }
            }
            StateHasChanged();
            base.OnAfterRender(firstRender);
        }

    }
}
