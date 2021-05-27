using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Enitites; 
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public class ItemCheckBoxBase : ComponentBase
    {
        [Parameter]
        public BaseItem Item { get; set; }

        [CascadingParameter]
        public string ColorPrefix { get; set; }

        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        public async Task ChangeIsDone()
        {
            Item.IsDone = !Item.IsDone;
            await UserItemManager.UpdateAsync(Item);
        }
    }
}
