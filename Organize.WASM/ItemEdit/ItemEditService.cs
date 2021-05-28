using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.ItemEdit
{
    public sealed class ItemEditService
    {
        public event EventHandler<ItemEditEventArgs> EditItemChanged;

        private BaseItem _editItem;

        public BaseItem EditItem
        {
            get => _editItem;
            set
            {
                if (_editItem == value)
                {
                    return;
                }

                _editItem = value;
                var args = new ItemEditEventArgs {Item = _editItem};
                OnEditItemChanged(args);
            }
        }

        private void OnEditItemChanged(ItemEditEventArgs e)
        {
            var handler = EditItemChanged;
            handler?.Invoke(this, e);
        }
    }
}
