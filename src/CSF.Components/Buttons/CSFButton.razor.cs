namespace CSF.Components.Buttons
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;

    public partial class CSFButton : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        public async Task OnClick(MouseEventArgs args)
        {
            await Click.InvokeAsync(args);
        }
    }
}
