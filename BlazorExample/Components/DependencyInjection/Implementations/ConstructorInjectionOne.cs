using System;
using BlazorExample.Components.DependencyInjection.Contracts;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class ConstructorInjectionOne : IConstructorInjectionOne
    {
        private readonly IConstructorInjectionTwo _constructorInjectionTwo;

        public ConstructorInjectionOne(IConstructorInjectionTwo constructorInjectionTwo)
        {
            _constructorInjectionTwo = constructorInjectionTwo;
        }

        public void MethodOfOne()
        {
            Console.WriteLine("One");
            _constructorInjectionTwo.MethodOfTwo();
        }
    }
}