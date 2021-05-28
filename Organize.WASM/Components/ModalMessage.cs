using Microsoft.AspNetCore.Components;

namespace Organize.WASM.Components
{
    public partial class ModalMessage : ComponentBase
    {
        [Parameter] public string Message { get; set; }
    }
}