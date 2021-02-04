namespace CSF.Components.Buttons
{
    using Microsoft.AspNetCore.Components;

    public partial class CSFButton : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string CssClass { get; set; } = string.Empty;
    }
}
