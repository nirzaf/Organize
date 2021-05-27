using BlazorExample.Components.DependencyInjection.Contracts;
using System;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class Scoped : IScoped, IDisposable
    {
        public int InstanceNumber { get; }
        private static volatile int _previousInstanceNumber;

        public Scoped()
        {
            Console.WriteLine("Scoped Created");
            InstanceNumber = System.Threading.Interlocked.Increment(ref _previousInstanceNumber);
        }

        public void Dispose()
        {
            Console.WriteLine("Transient Disposed");
        }
    }
}
