namespace CSF.Components.FontAwesome
{
    using Microsoft.AspNetCore.Components;

    public partial class CSFFontAwesomeIcon : ComponentBase
    {
        [Parameter]
        public CSFFontAwesomeIconType CSFFontAwesomeIconType { get; set; }
    }
}
