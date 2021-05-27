using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Organize.Business;
using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using Organize.Shared.Enums;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Organize.WASM.Components
{
    public partial class ItemEdit : ComponentBase, IDisposable
    {

        //[Inject]
        //private ItemEditService ItemEditService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        private BaseItem Item { get; set; } = new BaseItem();

        private int TotalNumber { get; set; }

        private Timer _debounceTimer;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _debounceTimer = new Timer();
            _debounceTimer.Interval = 500;
            _debounceTimer.AutoReset = false;
            _debounceTimer.Elapsed += HandleDebounceTimerElapsed;
            //ItemEditService.EditItemChanged += HandleEditItemChanged;
            //Item = ItemEditService.EditItem;
            SetDataFromUri();
        }

        private void HandleDebounceTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer elapsed");
            UserItemManager.UpdateAsync(Item);
        }

        private void SetDataFromUri()
        {
            if(Item != null)
            {
                Item.PropertyChanged -= HandleItemPropertyChanged;
            }
            
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            var segmentCount = uri.Segments.Length;
            if (segmentCount > 2 
                && Enum.TryParse(typeof(ItemTypeEnum), uri.Segments[segmentCount - 2].Trim('/'), out var typeEnum) 
                && int.TryParse(uri.Segments[segmentCount - 1], out var id))
            {
                var userItem = CurrentUserService.CurrentUser
                    .UserItems
                    .SingleOrDefault(item => item.ItemTypeEnum == (ItemTypeEnum)typeEnum && item.Id == id);

                //Not found? Redirect to items
                if (userItem == null)
                {
                    NavigationManager.LocationChanged -= HandleLocationChanged;
                    NavigationManager.NavigateTo("/items");
                }
                else
                {
                    Item = userItem;
                    Item.PropertyChanged += HandleItemPropertyChanged;
                    NavigationManager.LocationChanged += HandleLocationChanged;
                    TotalNumber = CurrentUserService.CurrentUser.UserItems.Count;
                    StateHasChanged();
                }
            }
        }

        private void HandleItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(_debounceTimer != null)
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
        }

        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            SetDataFromUri();
        }
         
        public void Dispose()
        {
            _debounceTimer?.Dispose();
            NavigationManager.LocationChanged -= HandleLocationChanged;
            Item.PropertyChanged -= HandleItemPropertyChanged;
        }

        //private void HandleEditItemChanged(object sender, ItemEditEventArgs e)
        //{
        //    if(Item != null)
        //    {
        //        Item.PropertyChanged -= HandleItemPropertyChanged;
        //    }
        //    Item = e.Item;
        //    Item.PropertyChanged += HandleItemPropertyChanged;
        //    StateHasChanged();
        //}
    }
}
