

namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using CSF.SRDashboard.Client.Services.WorkloadRequest.Enums;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text;
    public partial class StatusBadge
    {

        [Parameter]
        public RequestStatus RequestStatus { get; set; }
        [Parameter]
        public RenderFragment StatusText { get; set; }

        public string GetBadgeCssClass()
        {
            switch (RequestStatus)
            {
                case RequestStatus.IN_PROGRESS:
                    return "badge-inprogress";
                case RequestStatus.PENDING:
                    return "badge-pending";
                case RequestStatus.COMPLETE:
                    return "badge-completed";
                case RequestStatus.NEW:
                    return "badge-new";
                case RequestStatus.CANCELLED:
                    return "badge-cancelled";
                default:
                    return "badge-secondary";
            }
        }

        public string GetRequestStatusDescriptionString()
        {
            return this.RequestStatus.ToString();
        }
    }
}
    