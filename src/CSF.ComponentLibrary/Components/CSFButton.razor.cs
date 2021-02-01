using Microsoft.AspNetCore.Components;

namespace CSF.ComponentLibrary.Components
{
    public partial class CSFButton : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public CSFFontAwesomeIconType? CSFFontAwesomeIconType { get; set; }
    }
}
