namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;
    using CSF.Common.Library.Extensions.String;
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
    using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;
    using CSF.SRDashboard.Client.DTO;

    public partial class WorkloadRequestTable
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public List<WorkloadRequestTableItem> TableData { get; set; } = new List<WorkloadRequestTableItem>();

        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        public void CreateNewRequest()
        {
            if (this.Applicant != null)
            {
                this.NavigationManager.NavigateTo("/SeafarerProfile/" + this.Applicant.Cdn + "/create-new-request");
            }
        }

        public void RowClicked(WorkloadRequestTableItem tableItem)
        {
            this.NavigationManager.NavigateTo($"SeafarerProfile/{tableItem.ApplicantCDN}/view-request-details/{tableItem.RequestId}");
        }
    }
}
