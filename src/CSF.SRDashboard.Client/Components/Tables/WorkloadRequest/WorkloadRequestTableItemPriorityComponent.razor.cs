namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using System;
    using Microsoft.AspNetCore.Components;

    public partial class WorkloadRequestTableItemPriorityComponent
    {
        [Parameter]
        public string Priority { get; set; }

        public bool HasError {  
            get
            {
                if (this.Priority != null)
                    return this.Priority.Equals("request more info", StringComparison.InvariantCultureIgnoreCase);
                else
                    return false;
            }
        }

        public bool HasWarning
        {
            get
            {
                if (this.Priority != null)
                    return this.Priority.Equals("missing info", StringComparison.InvariantCultureIgnoreCase);
                else
                    return false;
            }
        }

        public string GetTextCssClass()
        {
            if (this.HasError)
                return "text-danger";
            if (this.HasWarning)
                return "text-warning";
            return "";
        }
    }
}
