namespace CSF.SRDashboard.Client.Components
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;
    public partial class CSFTabControl : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public CSFTabPage ActivePage { get; set; }
        public List<CSFTabPage> Pages = new List<CSFTabPage>();

        public void AddPage(CSFTabPage tabPage)
        {
            Pages.Add(tabPage);
            if (Pages.Count == 1)
                ActivePage = tabPage;
            StateHasChanged();
        }
        public string GetButtonClass(CSFTabPage page)
        {
            return page == ActivePage ? "csf-tab-button-active" : "";
        }
        public void ActivatePage(CSFTabPage page)
        {
            ActivePage = page;
        }
    }
}
