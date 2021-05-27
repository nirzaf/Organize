using Blazored.Modal;
using Blazored.Modal.Services;
using GeneralUi.BusyOverlay;
using GeneralUi.DropdownControl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Organize.Shared.Contracts;
using Organize.Shared.Enums;
using Organize.WASM.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public class SignUpBase : SignBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private IUserManager UserManager { get; set; }

        [Inject]
        private IModalService ModalService { get; set; }

        protected IList<DropdownItem<GenderTypeEnum>> GenderTypeDropDownItems { get; } = new List<DropdownItem<GenderTypeEnum>>();

        protected DropdownItem<GenderTypeEnum> SelectedGenderTypeDropDownItem { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var male = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Male,
                DisplayText = "Male"
            };

            var female = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Female,
                DisplayText = "Female"
            };

            var neutral = new DropdownItem<GenderTypeEnum>
            {
                ItemObject = GenderTypeEnum.Neutral,
                DisplayText = "others"
            };

            GenderTypeDropDownItems.Add(male);
            GenderTypeDropDownItems.Add(female);
            GenderTypeDropDownItems.Add(neutral);

            SelectedGenderTypeDropDownItem = female;

            TryGetUsernameFromUri();
        }

        private void TryGetUsernameFromUri()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            StringValues sv;
            if(QueryHelpers.ParseQuery(uri.Query).TryGetValue("userName", out sv))
            {
                User.UserName = sv;
            }
        }

        protected async void OnValidSubmit()
        {
            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                User.GenderType = SelectedGenderTypeDropDownItem.ItemObject;
                await UserManager.InsertUserAsync(User);
                NavigationManager.NavigateTo("signin");
            }
            catch(Exception e)
            {
                var parameters = new ModalParameters();
                parameters.Add(nameof(ModalMessage.Message), e.Message);
                ModalService.Show<ModalMessage>("Error", parameters);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }
    }
}
