using CSF.SRDashboard.Client.Models;
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
        public AddDocumentModel DocumentForm { get; set; }
        [Parameter]
        public EditContext EditContext { get; set; }

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
