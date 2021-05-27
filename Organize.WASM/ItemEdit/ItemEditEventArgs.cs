using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.ItemEdit
{
    public class ItemEditEventArgs : EventArgs
    {
        public BaseItem Item { get; set; }
    }
}
