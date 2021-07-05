using CSF.SRDashboard.Client.DTO.Azure;
using CSF.SRDashboard.Client.Models;
using CSF.SRDashboard.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;

namespace CSF.SRDashboard.Client.Components
{
    public partial class AssigneeSelector
    {
        [Inject]
        protected IUserGraphApiService graphService { get; set; }

        [Inject]
        IStringLocalizer<Shared.Common> Localizer { get; set; }

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
        private List<AzureMemberInfo> staff_members;

        [Parameter]
        public RequestModel RequestModel { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        public string GetCurrentlySelectedMME => previouslySelectedStaff != null ? this.previouslySelectedStaff.Names : Localizer["Unassigned"];

        private AzureMemberInfo previouslySelectedStaff;

        protected override void OnInitialized()
        {
            staff_members = graphService.GetMarineMedicalStaffMembers();

            previouslySelectedStaff = staff_members.Where(x => x.id == RequestModel.AssigneeId).FirstOrDefault();

            if (RequestModel.AssigneeId != null)
            {
                previouslySelectedStaff = graphService.GetUserByUserId(RequestModel.AssigneeId);
            }
        }
    }
}
