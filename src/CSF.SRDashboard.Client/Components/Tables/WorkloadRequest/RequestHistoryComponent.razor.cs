﻿namespace CSF.SRDashboard.Client.Components.Tables.WorkloadRequest
{
    using DSD.MSS.Blazor.Components.Core.Models;
    using DSD.MSS.Blazor.Components.Core.Constants;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSF.SRDashboard.Client.Models;

    public partial class RequestHistoryComponent
    {
        [Parameter]
        public List<StatusHistoryItem> StatusHistories { get; set; }
        [Parameter]
        public string DateFormat { get; set; }
        public bool IsListEmpty() => this.StatusHistories == null || !this.StatusHistories.Any() ? false : true;
    }
}
