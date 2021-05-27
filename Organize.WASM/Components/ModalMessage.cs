using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ModalMessage : ComponentBase
    {
        [Parameter]
        public string Message { get; set; }
    }
}
