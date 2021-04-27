namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using CSF.Common.Library.Extensions.Enum;
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
    using Microsoft.AspNetCore.Components;

    public partial class WorkloadRequestTableItemStatusComponent
    {
        [Parameter]
        public RequestStatus RequestStatus { get; set; }

        public string GetBadgeCssClass()
        {
            switch(RequestStatus)
            {
                case RequestStatus.IN_PROGRESS:
                    return "badge-info";
                case RequestStatus.PENDING:
                    return "badge-primary";
                case RequestStatus.COMPLETE:
                    return "badge-success";
                default:
                    return "badge-secondary";
            } 
        }

        public string GetRequestStatusDescriptionString()
        {
            return this.RequestStatus.GetDescription();
        }
    }
}
