using BlazorExample.Components.DependencyInjection.Contracts;
using Microsoft.AspNetCore.Components;

namespace BlazorExample.Components.DependencyInjection
{
    public class DependencyInjectionParentComponentBase: ComponentBase
    {
        [Inject]
        private IConstructorInjectionOne ConstructorInjectionOne { get; set; }

        protected bool IsComponentVisible { get; set; } = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ConstructorInjectionOne.MethodOfOne();
        }
    }
}
