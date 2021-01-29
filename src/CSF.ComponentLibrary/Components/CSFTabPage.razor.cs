namespace CSF.ComponentLibrary.Components
{
    using System;
    using Microsoft.AspNetCore.Components;

    public class CSFTabPageBase : ComponentBase
    {
        [CascadingParameter]
        public CSFTabControl Parent { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        protected override void OnInitialized()
        {
            if (Parent == null)
                throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");
            base.OnInitialized();
            Parent.AddPage(this);
        }
    }
}
