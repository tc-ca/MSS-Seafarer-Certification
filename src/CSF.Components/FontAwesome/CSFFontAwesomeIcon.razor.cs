namespace CSF.Components.FontAwesome
{
    using Microsoft.AspNetCore.Components;

    public partial class CSFFontAwesomeIcon : ComponentBase
    {
        [Parameter]
        public CSFFontAwesomeIconType CSFFontAwesomeIconType { get; set; }

        [Parameter]
        public int Size { get; set; }

        public string CssClass => this.Size > 1 ? string.Format("fa-{0}x", this.Size) : string.Empty;
    }
}
