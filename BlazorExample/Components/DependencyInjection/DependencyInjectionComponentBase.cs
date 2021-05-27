using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorExample.Components.DependencyInjection.Implementations;
using BlazorExample.Components.DependencyInjection.Contracts;
using Microsoft.AspNetCore.Components;

namespace BlazorExample.Components.DependencyInjection
{
    public class DependencyInjectionComponentBase : ComponentBase
    {
        [Inject]
        protected IScoped Scoped { get; set; }

        [Inject]
        protected ISingleton Singleton { get; set; }

        [Inject]
        protected ITransient Transient { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Console.WriteLine("Scoped " + Scoped.InstanceNumber);
            Console.WriteLine("Singleton " + Singleton.InstanceNumber);
            Console.WriteLine("Transient " + Transient.InstanceNumber);
        }
    }
}
