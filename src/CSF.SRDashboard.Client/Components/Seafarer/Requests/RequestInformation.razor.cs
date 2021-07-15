﻿using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace CSF.SRDashboard.Client.Components.Seafarer.Requests
{
    public partial class RequestInformation
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

        [Parameter]
        public RequestModel RequestModel { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        [Inject]
        public IStringLocalizer<Shared.Common> Localizer { get; set; }

        public string CssError { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
