using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class AddDocument
    {
        [Parameter]
        public string Cdn { get; set; }
        
     
        
        public UploadedDocument DocumentForm { get; set; }
        [Inject]
        public IGatewayService GatewayService { get; set; }
        public MpdisApplicantDto Applicant { get; set; }
    protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DocumentForm = new UploadedDocument();
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
         
        }
        public void HandleCancel()
        {

        }
        public void HandleValidSubmit()
        {

        }

    }
}