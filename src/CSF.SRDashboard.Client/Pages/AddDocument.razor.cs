using CSF.SRDashboard.Client.Models;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Pages
{
    public partial class AddDocument
    {
        protected EditContext EditContext;
        public AddDocumentModel DocumentForm { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.DocumentForm = new AddDocumentModel();
            this.EditContext = new EditContext(this.DocumentForm);
          

        }
    }
}