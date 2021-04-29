using CSF.SRDashboard.Client.PageValidators;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class SearchErrorComponent
    {
        [Parameter]
       public SearchErrorObject SearchError { get; set; }
    }
}
