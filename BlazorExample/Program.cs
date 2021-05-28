using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorExample.Components.DependencyInjection.Contracts;
using BlazorExample.Components.DependencyInjection.Implementations;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorExample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<IConstructorInjectionOne, ConstructorInjectionOne>();
            builder.Services.AddScoped<IConstructorInjectionTwo, ConstructorInjectionTwo>();


            builder.Services.AddScoped<IScoped, Scoped>();
            builder.Services.AddSingleton<ISingleton, Singleton>();
            builder.Services.AddTransient<ITransient, Transient>();

            //****other variants for service registration on singleton example****
            //builder.Services.AddSingleton<Scoped>();

            //var singleton = new Scoped();
            //builder.Services.AddSingleton<IScoped>(singleton);

            //****

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            await builder.Build().RunAsync();
        }
    }
}