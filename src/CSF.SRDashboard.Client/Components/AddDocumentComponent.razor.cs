using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.Components
{
    public partial class AddDocumentComponent
    {
        [Parameter]
        public IBrowserFile File { get; set; }

        [Parameter]
        public string ProfileName { get; set; }
        public string FileName { get { if (File == null) return String.Empty; return File.Name; } }

        [Parameter]
        public AddDocumentModel DocumentForm { get; set; }
        [Parameter]
        public EditContext EditContext { get; set; }
        [Inject]
        public SessionState state { get; set; }


    
        private void OnInputChange(EventArgs e)
        {

        }
        private void HandleValidSubmit()
        {
            var isValid = EditContext.Validate();
            Console.WriteLine("Valid Submit");
        }

    }
}
