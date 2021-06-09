using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class AddDocument
    {
        [Parameter]
        public string Cdn { get; set; }
        
        protected EditContext EditContext;
        
        public UploadedDocument DocumentForm { get; set; }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DocumentForm = new UploadedDocument();
            this.EditContext = new EditContext(this.DocumentForm);
        }

    }
}