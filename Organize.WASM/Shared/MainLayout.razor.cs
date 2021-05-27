using GeneralUi.BusyOverlay;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Organize.Shared.Contracts;
using Organize.WASM.OrganizeAuthenticationStateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase, IAsyncDisposable
    {
        private DotNetObjectReference<MainLayout> _dotNetReference;
        private IJSObjectReference _module;

        [Inject]
        protected ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private IAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        private bool IsAuthenticated { get; set; } = false;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public bool UseShortNavText { get; set; }

        protected void SignOut()
        {
            AuthenticationStateProvider.UnsetUser();
        }

        protected override async Task OnParametersSetAsync()
        {
            
                
            await base.OnParametersSetAsync();
            var authState = await AuthenticationStateTask;
            IsAuthenticated = authState.User.Identity.IsAuthenticated;

            if (!IsAuthenticated || CurrentUserService.CurrentUser.IsUserItemsPropertyLoaded)
            {
                return;
            }
            try
            {
     
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                await UserItemManager.RetrieveAllUserItemsOfUserAndSetToUserAsync(CurrentUserService.CurrentUser);
                Console.WriteLine("Items retrieved");
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //var width = await JSRuntime.InvokeAsync<int>("blazorDimension.getWidth");

            _module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                 "import", "./js/jsIsolation.js");
            var width = await _module.InvokeAsync<int>("getWidth");

            CheckUseShortNavText(width);

            _dotNetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("blazorResize.registerReferenceForResizeEvent"
                ,nameof(MainLayout)
                , _dotNetReference);
        }

        [JSInvokable]
        public static void OnResize()
        {
     
        }

        [JSInvokable]
        public void HandleResize(int width, int height)
        {
            CheckUseShortNavText(width);
        }

        private void CheckUseShortNavText(int width)
        {
            var oldValue = UseShortNavText;
            UseShortNavText = width < 700;
            if (oldValue != UseShortNavText)
            {
                StateHasChanged();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await JSRuntime.InvokeVoidAsync("blazorResize.unRegister", nameof(MainLayout));
            _dotNetReference?.Dispose();
            await _module.DisposeAsync();
        }
    }
}
