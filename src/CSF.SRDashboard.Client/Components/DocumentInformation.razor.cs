using CSF.Common.Library.Azure;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class DocumentInformation
    {
        [Parameter]
        public EditContext EditContext
        {
            get => editContext; set
            {
                if (editContext == value) return;
                this.editContext = value;
            }
        }

        private EditContext editContext;

        [Parameter]
        public List<UploadedDocument> UploadedDocuments { get; set; }

        [Parameter]
        public bool ShowDefaultOption { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        public ValidationMessageStore ValidationMessageStore { get; private set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAzureBlobService AzureBlobService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        /// <summary>
        /// cancels and returns to the profile page
        /// </summary>
        public void HandleCancel()
        {
            this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Applicant.Cdn}");
        }

        public void ViewDocument(UploadedDocument document)
        {
            this.NavigationManager.NavigateTo(document.DownloadLink);
        }

        /// <summary>
        /// Removes the attachment
        /// </summary>
        public void RemoveAttachment(int index)
        {
            if (index <= -1)
            {
                return;
            }

            if (index == 0)
            {
                this.ValidationMessageStore.Clear();
            }

            UploadedDocuments.RemoveAt(index);
        }
    }
}
