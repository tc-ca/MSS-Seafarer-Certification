namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using System;
    using CSF.SRDashboard.Client.Services;
    using Microsoft.AspNetCore.Components;

    public partial class WorkloadRequestTableItemPriorityComponent
    {
        [Parameter]
        public string Priority { get; set; }

        public bool HasError {  
            get
            {
                return !string.IsNullOrEmpty(this.Priority) && this.Priority.Equals(Constants.Priorities[0].Text, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public string GetTextCssClass()
        {
            if (this.HasError)
                return "text-warning";
            return "";
        }
    }
}
