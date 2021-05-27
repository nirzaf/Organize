using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.DataAccess
{
    public class ItemDataAccess : IItemDataAccess
    {
        private readonly IPersistenceService _persistenceService;

        public ItemDataAccess(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        public async Task DeleteItemsAsync<TItem>(IEnumerable<TItem> items) where TItem : BaseItem
        {
            foreach(var item in items)
            {
                await _persistenceService.DeleteAsync<TItem>(item);
            }
        }

        public async Task<IEnumerable<TItem>> GetItemsOfUserAsync<TItem>(int parentId) where TItem : BaseItem
        {
            return await _persistenceService.GetAsync<TItem>(i => i.ParentId == parentId);
        }

        public async Task InsertItemAsync<TItem>(TItem item) where TItem : BaseItem
        {
            var id = await _persistenceService.InsertAsync<TItem>(item);
            item.Id = id;
        }

        public Task UpdateItemAsync<TItem>(TItem item) where TItem : BaseItem
        {
            return _persistenceService.UpdateAsync<TItem>(item);
        }
    }
}
