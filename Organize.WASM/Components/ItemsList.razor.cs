using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organize.WASM.Components
{
    public partial class ItemsList : ComponentBase, IDisposable
    {
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IUserItemManager userItemManager { get; set; }


        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ItemEditService ItemEditService { get; set; }

        protected ObservableCollection<BaseItem> UserItems { get; set; } = new ObservableCollection<BaseItem>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Console.WriteLine("Initialize ItemsList");
            UserItems = CurrentUserService.CurrentUser.UserItems;
            UserItems.CollectionChanged += HandleUserItemsCollectionChanged;
            CurrentUserService.CurrentUser.PropertyChanged += HandleUserPropertyChanged;
        }

        private void HandleUserPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(User.UserItems)))
            {
                if(UserItems != null) { 
                    UserItems.CollectionChanged -= HandleUserItemsCollectionChanged;
                }

                UserItems = CurrentUserService.CurrentUser.UserItems;
                UserItems.CollectionChanged += HandleUserItemsCollectionChanged;
                StateHasChanged();
            }
        }

        private void HandleUserItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            StateHasChanged();
        }

        private void OnBackgroundClicked()
        {
            //ItemEditService.EditItem = null;
            NavigationManager.NavigateTo("/items");
        }

        public void Dispose()
        {
            UserItems.CollectionChanged -= HandleUserItemsCollectionChanged;
        }
    }
}
