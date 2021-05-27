using GeneralUi.DropdownControl;
using Microsoft.AspNetCore.Components;
using Organize.Shared.Contracts;
using Organize.Shared.Enums;
using Organize.WASM.ItemEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public partial class ItemsOverview : ComponentBase
    {
        //[Inject]
        //private ItemEditService ItemEditService { get; set; }
        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Parameter]
        public string TypeString { get; set; }

        [Parameter]
        public int? Id { get; set; }

        private DropdownItem<ItemTypeEnum> SelectedDropDownType { get; set; }

        private IList<DropdownItem<ItemTypeEnum>> DropDownTypes { get; set; }

        private bool ShowEdit { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DropDownTypes = new List<DropdownItem<ItemTypeEnum>>();

            var item = new DropdownItem<ItemTypeEnum>();
            item.ItemObject = ItemTypeEnum.Text;
            item.DisplayText = "Text";
            DropDownTypes.Add(item);

            item = new DropdownItem<ItemTypeEnum>();
            item.ItemObject = ItemTypeEnum.Url;
            item.DisplayText = "Url";
            DropDownTypes.Add(item);

            item = new DropdownItem<ItemTypeEnum>();
            item.ItemObject = ItemTypeEnum.Parent;
            item.DisplayText = "Parent";
            DropDownTypes.Add(item);
            //ItemEditService.EditItemChanged += HandleEditItemChanged;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Id != null && Enum.TryParse(typeof(ItemTypeEnum), TypeString, out _))
            {
                ShowEdit = true;
            } else
            {
                ShowEdit = false;
            }
        }

        private void HandleEditItemChanged(object sender, ItemEditEventArgs e)
        {
            ShowEdit = e.Item != null;
            StateHasChanged();
        }

        private async void AddNewAsync()
        {
            if(SelectedDropDownType == null)
            {
                return;
            }

            await UserItemManager.CreateNewUserItemAndAddItToUserAsync(
                CurrentUserService.CurrentUser,
                SelectedDropDownType.ItemObject);
        }
    }
}
