using CSF.API.Data.Entities;
using CSF.API.Services.Repositories;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.DTO.WorkLoadManagement;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class AddDocumentComponent
    {


       

        [Inject]
        public IWorkLoadManagementService WorkLoadManagementService { get; set; }

        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public bool AllowMultipleUploads { get; set; }

        [Parameter]
        public int RequestId { get; set; } = -1;
        [Parameter]
        public List<UploadedDocument> DocumentForm { get; set; }


        public EditContext EditContext { get; set; }

       

        public string Language { get; set; }

        public ValidationMessageStore ValidationMessageStore { get; private set; }

        public void FileUploaded()
        {
            this.StateHasChanged();
        }
    }
}