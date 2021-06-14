using CSF.API.Data.Entities;
using CSF.API.Services.Repositories;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.Services.Document;
using CSF.SRDashboard.Client.Utilities;
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

      
        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }
        [Inject]
        public IGatewayService GatewayService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; private set; }
        [Inject]
        public SessionState State { get; set; }
        [Inject]
        public IDocumentService DocumentService { get; set; }
        public MpdisApplicantDto Applicant { get; set; }

        public List<UploadedDocument> DocumentForm { get; set; } = new List<UploadedDocument>();
       

        public IUploadDocumentService UploadService { get; set; }
        public DocumentInfo DocumentInfo { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.UploadService = new UploadDocumentService(this.DocumentService);
        }

        private async Task uploadToSeafarer()
        {
         

            foreach (var document in this.State.DocumentForm)
            {

                var addedDocumentIds = await this.UploadService.UploadDocument(document);
                if (addedDocumentIds.Count > 0)
                {
                    this.DocumentInfo = new DocumentInfo
                    {
                        Cdn = this.Cdn,
                        DateStartDte = DateTime.UtcNow,
                        DocumentId = addedDocumentIds[0]
                    };
                    ClientXrefDocumentRepository.Insert(this.DocumentInfo);
                }
               
            }

            this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Cdn}");
        }
        public void HandleCancel()
        {

        }
        public void HandleValidSubmit()
        {

        }

    }
}