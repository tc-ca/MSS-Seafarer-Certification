namespace CSF.ComponentLibrary.Components
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;
    public class CSFTabControlBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public CSFTabPageBase ActivePage { get; set; }
        public List<CSFTabPageBase> Pages = new List<CSFTabPageBase>();

        public void AddPage(CSFTabPageBase tabPage)
        {
            Pages.Add(tabPage);
            if (Pages.Count == 1)
                ActivePage = tabPage;
            StateHasChanged();
        }
        public string GetButtonClass(CSFTabPageBase page)
        {
            return page == ActivePage ? "csf-tab-button-active" : "";
        }
        public void ActivatePage(CSFTabPageBase page)
        {
            ActivePage = page;
        }
    }
}
