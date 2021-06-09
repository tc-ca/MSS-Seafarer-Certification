namespace CSF.SRDashboard.Client.Pages
{
    using CSF.API.Data.Entities;
    using CSF.API.Services.Repositories;
    using CSF.Common.Library.Azure;
    using CSF.SRDashboard.Client.DTO;
    using CSF.SRDashboard.Client.Models;
    using CSF.SRDashboard.Client.Services;
    using CSF.SRDashboard.Client.Services.Document;
    using CSF.SRDashboard.Client.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.WebUtilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
    using DSD.MSS.Blazor.Components.Core.Constants;
    using Microsoft.JSInterop;
    using Microsoft.Extensions.Localization;

    public partial class SeafarerProfile
    {
       
        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public int RequestId { get; set; }

        [Parameter]
        public AlertTypes AlertType { get; set; }

        [Parameter]
        public bool IsAlertEnabled { get; set; }

        [Parameter]
        public RenderFragment AlertMessageContent { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }
        [Inject]
        public IClientXrefDocumentRepository ClientXrefDocumentRepository { get; set; }
        [Inject]
        public IDocumentService DocumentService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        public IAzureBlobService AzureBlobService { get; set; }
        [Inject]
        public SessionState State { get; set; }

        [Inject]
        IStringLocalizer<SeafarerProfile> Localizer { get; set; }
        public MpdisApplicantDto Applicant { get; set; }
       
        public bool ShowToast { get; set; } = false;

        public List<DocumentInfo> Documents { get; set; }

        public DateTime DOB;
        public bool ShowFilterHeader { get; set; } = true;

        protected List<Document> TableData = new List<Document>();

        public List<Services.Document.Entities.DocumentInfo> DocumentInfos { get; set; }

        public string currentRelativePath;

        private int RequestID;

        [Parameter]
        public string Updated { get; set; }

        private string cratedOrUpdated { get; set; }
        private string message { get; set; }
        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        public List<WorkloadRequestTableItem> TableItems;

        protected async override Task OnInitializedAsync()
        {
            this.IsAlertEnabled = this.RequestId != 0;
            if (Updated != null)
            {
                message = Localizer["SuccessfullyUpdated"] + RequestId + Localizer["For"];
            }
            else
            {
                message = Localizer["SuccessfullyCreated"] + RequestId + Localizer["For"];
            }

            await base.OnInitializedAsync();

            this.LoadData();
            var Documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();
            
            var documentIds = Documents.Select(x => x.DocumentId).ToList();

            // Call document servie to get info for each document
            DocumentInfos = await DocumentService.GetDocumentsWithDocumentIds(documentIds);

            foreach (var documentInfo in DocumentInfos)
            {

                var link = await this.AzureBlobService.GetDownloadLinkAsync("documents", documentInfo.DocumentUrl, DateTime.UtcNow.AddHours(8));

                this.TableData.Add(new Document
                {
                    DocumentId = documentInfo.DocumentId,
                    FileName = documentInfo.FileName,
                    Language = documentInfo.Language,
                    Type = documentInfo.FileType,
                    DateUploaded = documentInfo.DateCreated.Value,
                    DocumentUrl = documentInfo.DocumentUrl,
                    DownloadLink = link
                });
            }

            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("requestId", out var requestId))
            {
                RequestID = Convert.ToInt32(requestId);
            }
        }

        protected void HandleHeaderFilterChanged()
        {
            StateHasChanged();
        }


        protected override async void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (IsAlertEnabled)
            {
                await JS.InvokeVoidAsync("SetTab");
            }
        }

        private void LoadData()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.TableItems = WorkLoadService.GetByCdnInRequestTableFormat(Cdn);
            this.AlertType = AlertTypes.Success;
        }
    }
}