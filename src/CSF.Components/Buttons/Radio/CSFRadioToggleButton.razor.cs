namespace CSF.Components.Buttons.Radio
{
    using Microsoft.AspNetCore.Components;

    public partial class CSFRadioToggleButton : ComponentBase
    { 
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public bool IsChecked { get; set; } = false;

        public string HtmlId => string.Format("toggle-{0}", this.Id);
        public string LabelFor => string.Format("toggle-{0}", this.Id);
    }
}
