﻿using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class MMEDetails
    {

        [Parameter]
        public MMEDetailComponentModel Model { get; set; }

    }
}