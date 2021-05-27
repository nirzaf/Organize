using BlazorExample.Components.DependencyInjection.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
