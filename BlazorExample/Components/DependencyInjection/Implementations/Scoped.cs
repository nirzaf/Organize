using System;
using System.Threading;
using BlazorExample.Components.DependencyInjection.Contracts;

namespace BlazorExample.Components.DependencyInjection.Implementations
{
    public class Scoped : IScoped, IDisposable
    {
        private static volatile int _previousInstanceNumber;

        public Scoped()
        {
            Console.WriteLine("Scoped Created");
            InstanceNumber = Interlocked.Increment(ref _previousInstanceNumber);
        }

        public void Dispose()
        {
            Console.WriteLine("Transient Disposed");
        }

        public int InstanceNumber { get; }
    }
}