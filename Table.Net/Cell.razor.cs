using Microsoft.AspNetCore.Components;

namespace Table.Net
{
    public partial class Cell : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Align Align { get; set; } = Align.Left;
    }
}
