using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class RequestDetail
    {

        [Parameter]
        public RequestDetailComponentModel Model { get; set; }

    }
}
