using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
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

        public bool IsCurrentSelectedStatus(Dropdown dropdown) => dropdown.Text.Equals(Constants.RequestStatusesDefaultValue) && (RequestModel.Status == null || RequestModel.Status == dropdown.ID);

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
