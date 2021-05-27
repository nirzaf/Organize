using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.ItemEdit
{
    public class ItemEditService
    {
        public event EventHandler<ItemEditEventArgs> EditItemChanged;

        private BaseItem _editItem;

        public BaseItem EditItem
        {
            get { return _editItem; }
            set
            {
                if (_editItem == value)
                {
                    return;
                }

                _editItem = value;
                var args = new ItemEditEventArgs();
                args.Item = _editItem;
                OnEditItemChanged(args);
            }
        }

        protected virtual void OnEditItemChanged(ItemEditEventArgs e)
        {
            EventHandler<ItemEditEventArgs> handler = EditItemChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
