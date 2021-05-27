using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Shared.Contracts
{
    public interface IItemDataAccess
    {
        Task<IEnumerable<TItem>> GetItemsOfUserAsync<TItem>(int parentId) where TItem : BaseItem;
        Task InsertItemAsync<TItem>(TItem item) where TItem : BaseItem;
        Task UpdateItemAsync<TItem>(TItem item) where TItem : BaseItem;
        Task DeleteItemsAsync<TItem>(IEnumerable<TItem> items) where TItem : BaseItem;
    }
}
