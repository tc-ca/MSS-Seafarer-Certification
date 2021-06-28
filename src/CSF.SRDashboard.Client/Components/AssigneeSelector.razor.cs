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
        private List<AzureMemberInfo> mme_members;

        [Parameter]
        public RequestModel RequestModel { get; set; }

        [Parameter]
        public bool IsReadOnly { get; set; }

        public string GetCurrentlySelectedMME => previouslySelectedMME != null ? this.previouslySelectedMME.Names : Localizer["Unassigned"];

        private AzureMemberInfo previouslySelectedMME;

        protected override void OnInitialized()
        {
            mme_members = graphService.GetMmeGroupMembers();

            previouslySelectedMME = mme_members.Where(x => x.id == RequestModel.AssigneeId).FirstOrDefault();

            if (RequestModel.AssigneeId != null)
            {
                previouslySelectedMME = graphService.GetUserByUserId(RequestModel.AssigneeId);
            }
        }
    }
}
