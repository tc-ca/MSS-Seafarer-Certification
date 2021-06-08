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
        public bool Editable { get; set; }

        [Parameter]
        public bool ShowDefaultOption { get; set; }

        [Parameter]
        public bool ShowRequestID { get; set; }

        //[Parameter]

        [Parameter]
        public bool IsReadOnly { get; set; }

        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        public ValidationMessageStore ValidationMessageStore { get; private set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        public UploadedDocument DocumentForm { get; set; }

        public List<UploadedDocument> DocumentForm1 { get; set; }

        public List<IFormFile> FilesToUpload { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.DocumentForm1 = new List<UploadedDocument>();

            this.EditContext = new EditContext(this.DocumentForm1);
        }

        /// <summary>
        /// cancels and returns to the profile page
        /// </summary>
        public void HandleCancel()
        {
            //this.FileToUpload = null;
            //this.NavigationManager.NavigateTo($"/SeafarerProfile/{this.Cdn}");
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

            this.FilesToUpload.RemoveAt(index);

            //this.FileToUpload = null;
        }
    }
}
