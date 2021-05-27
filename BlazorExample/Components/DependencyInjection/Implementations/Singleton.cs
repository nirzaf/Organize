using BlazorExample.Components.DependencyInjection.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class Singleton : ISingleton, IDisposable
    {
        public int InstanceNumber { get; }
        private static volatile int _previousInstanceNumber;


        public Singleton()
        {
            Console.WriteLine("Singleton Created");
            InstanceNumber = System.Threading.Interlocked.Increment(ref _previousInstanceNumber);
        }

        public void Dispose()
        {
            Console.WriteLine("Transient Disposed");
        }
    }
}
