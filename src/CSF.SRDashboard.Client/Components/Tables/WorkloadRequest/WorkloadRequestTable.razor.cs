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
        [Parameter]
        public List<WorkloadRequestTableItem> TableData { get; set; } = new List<WorkloadRequestTableItem>();

        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            this.TableData.Add(new WorkloadRequestTableItem()
            {
                RequestId = "123456D",
                Certificate = "Marine medical certificate - 2 years",
                RequestType = "Renewal",
                RequestDate = DateTime.Now,
                Status = "In Progress".ToEnum<RequestStatus>(RequestStatus.UNKNOWN),
                Priority = "Request more info",
                Language = "E"
            });
            this.TableData.Add(new WorkloadRequestTableItem()
            {
                RequestId = "456326H",
                Certificate = "Marine medical certificate - 2 years",
                RequestType = "Renewal",
                RequestDate = DateTime.Now,
                Status = "Pending".ToEnum<RequestStatus>(RequestStatus.UNKNOWN),
                Priority = "Missing info",
                Language = "E"
            });
            this.TableData.Add(new WorkloadRequestTableItem()
            {
                RequestId = "8721455C",
                Certificate = "Marine medical certificate - 2 years",
                RequestType = "New",
                RequestDate = DateTime.Now,
                Status = "Complete".ToEnum<RequestStatus>(RequestStatus.UNKNOWN),
                Priority = "Received",
                Language = "E"
            });
        }

        protected void OnAfterTableDataLoaded()
        {
        }

        public void CreateNewRequest()
        {
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + this.Applicant.Cdn + "/create-new-request");

        }
    }
}
