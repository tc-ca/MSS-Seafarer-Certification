using System.Collections.Generic;
using CSF.SRDashboard.Client.DTO;
using CSF.SRDashboard.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CSF.SRDashboard.Client.Components.Tables.Dashboard
{
    public partial class DashboardTable
    {
        [Parameter]
        public List<Document> TableData { get; set; } = new List<Document>();
        [Inject]
        public NavigationManager NavigationManager { get; private set; }
        [Parameter]
        public MpdisApplicantDto Applicant { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void AddDocumentRedirect()
        {
            NavigationManager.NavigateTo($"/SeafarerProfile/{this.Applicant.Cdn}/AddAttachment");
        }

        public void RowClicked(Document document)
        {
            this.NavigationManager.NavigateTo($"SeafarerProfile/{this.Applicant.Cdn}/view-attachment/{document.DocumentId}");
        }

    }
}
