using Microsoft.AspNetCore.Components;
using CSF.SRDashboard.Client.Services;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest;
using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
using System.Collections.Generic;
using DSD.MSS.Blazor.Components.Core.Constants;
using Microsoft.JSInterop;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class SeafarerProfile
    {
        [Parameter]
        public string Cdn { get; set; }

        [Parameter]
        public int requestId { get; set; }

        [Parameter]
        public AlertTypes alertType { get; set; }

        [Parameter]
        public bool IsAlertEnabled { get; set; }

        [Parameter]
        public RenderFragment AlertMessageContent { get; set; }

        [Inject]
        public IGatewayService GatewayService { get; set; }

        [Inject]
        public IWorkLoadManagementService WorkLoadService { get; set; }

        public MpdisApplicantDto Applicant { get; set; }

        public List<WorkloadRequestTableItem> tableItems;


        protected override void OnInitialized()
        {
            this.LoadData();
            if(requestId != 0)
            {
                IsAlertEnabled = true;
            }
            else
            {
                IsAlertEnabled = false;
            }    
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
            tableItems = WorkLoadService.GetByCdnInRequestTableFormat(Cdn);
            alertType = AlertTypes.Success;
        }
    }
}