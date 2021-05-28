namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;
    using CSF.Common.Library.Extensions.String;
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
    using CSF.SRDashboard.Client.Components.Tables.WorkloadRequest.Entities;

    public partial class WorkloadRequestTable
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public List<WorkloadRequestTableItem> TableData { get; set; } = new List<WorkloadRequestTableItem>();

        protected override void OnInitialized()
        {
            this.TableData.Add(new WorkloadRequestTableItem()
            {
                RequestId = "123456D",
                Certificate = "Marine medical certificate - 2 years",
                RequestType = "Renewal",
                RequestDate = DateTime.Now,
                Status = "In Progress".ToEnum<RequestStatus>(RequestStatus.UNKNOWN),
                Priority = "Urgent",
                AssignedTo = "John Medicalman",
                ProcessingPhase = "In limbo"
            });
            this.TableData.Add(new WorkloadRequestTableItem()
            {
                RequestId = "456326H",
                Certificate = "Marine medical certificate - 2 years",
                RequestType = "Renewal",
                RequestDate = DateTime.Now,
                Status = "Pending".ToEnum<RequestStatus>(RequestStatus.UNKNOWN),
                Priority = "High",
                AssignedTo = "John Medicalman",
                ProcessingPhase = "In limbo"
            });
            this.TableData.Add(new WorkloadRequestTableItem()
            {
                RequestId = "8721455C",
                Certificate = "Marine medical certificate - 2 years",
                RequestType = "New",
                RequestDate = DateTime.Now,
                Status = "Complete".ToEnum<RequestStatus>(RequestStatus.UNKNOWN),
                Priority = "Low",
                AssignedTo = "John Medicalman",
                ProcessingPhase = "In limbo"
            });
        }

        protected void OnAfterTableDataLoaded()
        {
        }
        public void RowClicked(WorkloadRequestTableItem tableItem)
        {
            // will change later. Need to ask about url of ViewRequestDetails.
            this.NavigationManager.NavigateTo("/SeafarerProfile/" + "345" +"/view-request-details/RequestId:" + tableItem.RequestId);
        }
    }
}
