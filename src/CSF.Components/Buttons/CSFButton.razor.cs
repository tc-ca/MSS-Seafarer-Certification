namespace CSF.Components.Buttons
{
    using CSF.Components.FontAwesome;
    using Microsoft.AspNetCore.Components;

    public partial class CSFButton : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public CSFFontAwesomeIconType? CSFFontAwesomeIconType { get; set; }
    }
}
