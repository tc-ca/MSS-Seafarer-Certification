using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using DSD.MSS.Blazor.Components.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace CSF.SRDashboard.Client.Components
{
    public partial class RequestStatus
    {
        [Parameter]
        public EditContext EditContext
        {
            get => editContext; set
            {
                if (editContext == value) return;
                this.editContext = value;
            }
        }

        private EditContext editContext;

        [Parameter]
        public bool Editable { get; set; }

        [Parameter]
        public bool ShowDefaultOption { get; set; }

        [Parameter]
        public RequestModel RequestModel { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        public bool IsCurrentSelectedStatus(SelectListItem dropdown) => dropdown.Text.Equals(Constants.RequestStatusesDefaultValue) && (RequestModel.Status == null || RequestModel.Status == dropdown.Id);

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public void ResetProcessingPhase()
        {
            RequestModel.ProcessingPhase = null;
        }
    }
}
