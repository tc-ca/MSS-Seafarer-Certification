namespace CSF.Components.Buttons.Radio
{
    using Microsoft.AspNetCore.Components;

    public partial class CSFRadioToggleGroup : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
