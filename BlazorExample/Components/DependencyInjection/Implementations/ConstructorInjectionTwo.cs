using System;
using BlazorExample.Components.DependencyInjection.Contracts;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class ConstructorInjectionTwo : IConstructorInjectionTwo
    {
        public void MethodOfTwo()
        {
            Console.WriteLine("Two");
        }
    }
}