using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorExample.Entities;
using Microsoft.AspNetCore.Components;

namespace BlazorExample.Components
{
    public class LifeCycleBase : ComponentBase, IDisposable
    {
        [Parameter] public int SomeIntValue { get; set; }

        public User User { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            Console.WriteLine("SetParametersAsync");
            Console.WriteLine(JsonSerializer.Serialize(parameters));
            Console.WriteLine(parameters);
            return base.SetParametersAsync(parameters);
        }

        //--------Sync--------

        protected override void OnInitialized()
        {
            Console.WriteLine("OnInitialized");
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            Console.WriteLine("OnParametersSet");
            base.OnParametersSet();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("OnAfterRender");
            base.OnAfterRender(firstRender);
        }

        //--------Async--------
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(2000);
            User = new User {FirstName = "Benni"};
            Console.WriteLine("OnInitializedAsync");
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {

            Console.WriteLine("OnParametersSetAsync");
            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("OnAfterRenderAsync");
            Console.WriteLine("-------------");
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }
    }
}
