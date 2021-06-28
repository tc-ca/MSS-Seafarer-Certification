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
    using CSF.SRDashboard.Client.Services.Document.Entities;
    using CSF.SRDashboard.Client.DTO.DocumentStorage;

    public partial class SeafarerProfile
    {

        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public int RequestId { get; set; }
        /// <summary>
        /// Tab to open if passed ?tab=summary
        /// </summary>
        [Parameter]
        public string Tab { get; set; }
        [Parameter]
        public string Created { get; set; }

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
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAzureBlobService AzureBlobService { get; set; }
        [Inject]
        public SessionState State { get; set; }

        [Inject]
        IStringLocalizer<SeafarerProfile> Localizer { get; set; }

        [Inject] 
        IUserGraphApiService graphApiService { get; set; }
        public MpdisApplicantDto Applicant { get; set; }

        public bool ShowToast { get; set; } = false;

        public DateTime DOB;
        public bool ShowFilterHeader { get; set; } = true;

        protected List<Document> TableData = new List<Document>();

        public DocumentDTO DocumentResult { get; set; }

        public string currentRelativePath;

        private int RequestID;

        [Parameter]
        public string Updated { get; set; }

        [Parameter]
        public string FileName { get; set; }

        private string message { get; set; }
        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        public List<WorkloadRequestTableItem> TableItems;

        protected async override Task OnInitializedAsync()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            this.IsAlertEnabled = this.RequestId != 0;

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("fileName", out var filename))
            {
                FileName = filename;
            }

            if (Updated != null)
            {
                message = Localizer["SuccessfullyUpdated"] + " " + RequestId + Localizer["For"];
            }
            else
            {
                message = Localizer["SuccessfullyCreated"] + " " + RequestId + Localizer["For"];
            }

            await base.OnInitializedAsync();

            this.LoadData();
            var Documents = ClientXrefDocumentRepository.GetDocumentsByCdn(Cdn).ToList();
            List<Services.Document.Entities.DocumentInfo> documentInfos = new List<Services.Document.Entities.DocumentInfo>();

            for (var i = 0; i < Math.Round(Math.Ceiling(Documents.Count / 30.0),0); i++)
            {
                var documentIds = Documents.Select(x => x.DocumentId).Skip(i * 30).Take(30).ToList();

                // Call document servie to get info for each document
                var docsToConcat = await DocumentService.GetDocumentsWithDocumentIds(documentIds);
                documentInfos.AddRange(docsToConcat);
            }

            foreach (var documentInfo in documentInfos)
            {

                var link = await this.AzureBlobService.GetDownloadLinkAsync("documents", documentInfo.DocumentUrl, DateTime.UtcNow.AddHours(8));

                this.TableData.Add(new Document
                {
                    DocumentId = documentInfo.DocumentId,
                    FileName = documentInfo.FileName,
                    Language = documentInfo.Language,
                    Type = documentInfo.FileType,
                    DateUploaded = documentInfo.DateLastUpdated.Value,
                    DocumentUrl = documentInfo.DocumentUrl,
                    DownloadLink = link
                });
            }

            this.TableData = this.TableData.OrderByDescending(x => x.DateUploaded).ToList();

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

            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (IsAlertEnabled)
            {
                await JS.InvokeVoidAsync("SetTab", "requestLink");
            }
            else if (!string.IsNullOrWhiteSpace(FileName))
            {
                await JS.InvokeVoidAsync("SetTab", "documents");
            }
            else if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("tab", out var tab))
            {
                Tab = tab;

                if (!string.IsNullOrWhiteSpace(Tab))
                {
                    await JS.InvokeVoidAsync("SetTab", Tab);
                }
            }
        }

        private void LoadData()
        {
            this.Applicant = this.GatewayService.GetApplicantInfoByCdn(Cdn);
            this.TableItems = WorkLoadService.GetByCdnInRequestTableFormat(Cdn);
            this.SetAssigneeNames(this.TableItems);
            this.AlertType = AlertTypes.Success;
        }

        //TODO:If an MME leaves his job and on Azure MME list does not have this person, when loading a work request
        //where this MME was assigned, following method can not show this person.
        //Discus this issue. Possible fix could be to store MME name on Work load management Assignee table togethe with
        //Or instead of the AssigneeId. Or we should not delete the person fromo the MME list when this person leaves his duty.
        private void SetAssigneeNames(List<WorkloadRequestTableItem> tableItems)
        {
            var mmeMembers = graphApiService.GetMmeGroupMembers();
            foreach(var item in this.TableItems)
            {
                if(item.AssigneeId != null)
                {
                    var mmeFound = mmeMembers.Where(x => x.id == item.AssigneeId).FirstOrDefault();
                    if(mmeFound != null)
                    {
                        item.AssignedTo = mmeFound.Names;
                    }
                }
            }
        }
    }
}